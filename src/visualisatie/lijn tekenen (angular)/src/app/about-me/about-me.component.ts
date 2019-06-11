import {
  Component, Input, ElementRef, AfterViewInit, ViewChild
} from '@angular/core';
import { fromEvent } from 'rxjs';
import { switchMap, takeUntil, pairwise } from 'rxjs/operators'

  @Component({
    selector: 'app-about-me',
    templateUrl: './about-me.component.html',
    styleUrls: ['./about-me.component.css']
  })

export class AboutMeComponent implements AfterViewInit {

  @ViewChild('canvas') public canvas: ElementRef;

  @Input() public width = 400;
  @Input() public height = 400;

  private cx: CanvasRenderingContext2D;

  public ngAfterViewInit() {
    const canvasEl: HTMLCanvasElement = this.canvas.nativeElement as HTMLCanvasElement;
    this.cx = canvasEl.getContext('2d');

    canvasEl.width = this.width;
    canvasEl.height = this.height;

    this.cx.lineWidth = 3;
    this.cx.lineCap = 'round';
    this.cx.strokeStyle = '#000';

    this.captureEvents(canvasEl);
  }

  // onResize() {
  //   // Not a good thing to do but will get you going.
  //   // I need to look into the Renderer service instead.
  //   const canvasElement = this.canvas.nativeElement as HTMLCanvasElement;

  //   // These don't change (canvas intrinsic dimensions)
  //   const canvasWidth = canvasElement.width;
  //   const canvasHeight = canvasElement.height;

  //   // These will change (scaling applied by the style)
  //   const computedStyles = getComputedStyle(canvasElement);
  //   const computedWidth = computedStyles.width;
  //   const computedHeight = computedStyles.height;
  // }
  
  private captureEvents(canvasEl: HTMLCanvasElement) {
    // neemt alle mousedown events op van het canvas element
    fromEvent(canvasEl, 'mousedown')
      .pipe(
        switchMap((e) => {
          // na een muis down, nemen we alle muisbewegingen op
          return fromEvent(canvasEl, 'mousemove')
            .pipe(
              //we stoppen (en unsubscribe) zodra de gebruiker de muis loslaat
              // dit gaat 'mouseup' event triggeren   
              takeUntil(fromEvent(canvasEl, 'mouseup')),
              // we stoppen ook (en opzeggen) zodra de muis het canvas verlaat (mouseleave-event)
              takeUntil(fromEvent(canvasEl, 'mouseleave')),
              //in twee paren krijgen we de vorige waarde om een regel uit te tekenen
              // het vorige punt tot het huidige punt
              pairwise()
            )
        })
      )
      .subscribe((res: [MouseEvent, MouseEvent]) => {
        const rect = canvasEl.getBoundingClientRect();
  
        // vorige en huidige positie met de offset
        const prevPos = {
          x: res[0].clientX - rect.left,
          y: res[0].clientY - rect.top
        };
  
        const currentPos = {
          x: res[1].clientX - rect.left,
          y: res[1].clientY - rect.top
        };
        // deze methode zullen we binnenkort implementeren om de daadwerkelijke tekening te maken
        this.drawOnCanvas(prevPos, currentPos);
      });

      if(fromEvent(canvasEl, 'mousedown')){
        this.ClearCanvas();
      }

  }

  private drawOnCanvas(prevPos: { x: number, y: number }, currentPos: { x: number, y: number }) {
    if (!this.cx) { return; }

    this.cx.beginPath();

    if (prevPos) {
      this.cx.moveTo(prevPos.x, prevPos.y); // van
      this.cx.lineTo(currentPos.x, currentPos.y);
      this.cx.stroke();
    }
  }

  private ClearCanvas(){
    this.cx.clearRect(0, 0, this.width, this.height);
  }

  private Verzenden(){
    var fileText = "I am the first part of the info being emailed.\r\nI am the second part.\r\nI am the third part.";
    var fileName = "newfile001.txt"
    this.saveTextAsFile(fileText, fileName);
  }

  private saveTextAsFile (data, filename){

    if(!data) {
        console.error('Console.save: No data')
        return;
    }

    if(!filename) filename = 'console.json'

    var blob = new Blob([data], {type: 'text/plain'}),
        e    = document.createEvent('MouseEvents'),
        a    = document.createElement('a')
// Voor IE:

if (window.navigator && window.navigator.msSaveOrOpenBlob) {
  window.navigator.msSaveOrOpenBlob(blob, filename);
}
else{
  var e = document.createEvent('MouseEvents'),
      a = document.createElement('a');

  a.download = filename;
  a.href = window.URL.createObjectURL(blob);
  a.dataset.downloadurl = ['text/plain', a.download, a.href].join(':');
}
}



}