    var canvas, ctx;
    var mouseX, mouseY, mouseDown = 0;
    var touchX, touchY;
    var array;
    function drawDot(ctx, x, y, size) {
      r = 0;
      g = 0;
      b = 0;
      a = 255;
      ctx.fillStyle = "rgba(" + r + "," + g + "," + b + "," + (a / 255) + ")";

// Teken een gevulde cirkel
      ctx.beginPath();
      ctx.arc(x, y, size, 0, Math.PI * 2, true);
      ctx.closePath();
      ctx.fill();
    }

    // Wis de canvascontext met behulp van de breedte en hoogte van het canvas
    function clearCanvas(canvas, ctx) {
      ctx.clearRect(0, 0, canvas.width, canvas.height);
    }

    // Houd de muisknop in de gaten die wordt ingedrukt en teken een punt op de huidige locatie
    function canvas_mouseDown() {
      mouseDown = 1;
      drawDot(ctx, mouseX, mouseY, 6);
    }

    // Blijf op de hoogte van de muisknop die wordt vrijgegeven
    function canvas_mouseUp() {
      mouseDown = 0;
    }

    // Houd de positie van de muis bij en teken een punt als de muisknop op dit moment wordt ingedrukt
    function canvas_mouseMove(e) {
      // Update de muiscoördinaten bij verplaatsing
      getMousePos(e);

      // Teken een punt als de muisknop wordt ingedrukt
      if (mouseDown == 1) {
        drawDot(ctx, mouseX, mouseY, 6);
      }
    }

    // Haal de huidige positie van de muis op ten opzichte van de linkerbovenhoek van het canvas
    function getMousePos(e) {
      if (!e)
        var e = event;

      if (e.offsetX) {
        mouseX = e.offsetX;
        mouseY = e.offsetY;
      } else if (e.layerX) {
        mouseX = e.layerX;
        mouseY = e.layerY;
      }
    }

    // Teken iets wanneer een aanraakstart wordt gedetecteerd
    function canvas_touchStart() {
      // Werk de aanraakcoördinaten bij
      getTouchPos();

      drawDot(ctx, touchX, touchY, 6);

      // Voorkomt dat een extra gebeurtenis in de muis wordt geactiveerd
      event.preventDefault();
    }

    // Teken iets en voorkom het standaard scrollen wanneer aanraakbeweging wordt gedetecteerd
    function canvas_touchMove(e) {
      getTouchPos(e);

// Tijdens een aanraakgebeurtenis hoeven we, in tegenstelling tot een mousemove-gebeurtenis, niet te controleren of de aanraaktoets is ingeschakeld, omdat er per definitie altijd contact met het scherm is.
      drawDot(ctx, touchX, touchY, 6);
      array = [touchX, touchY];
      console.log(array);


      // Voorkom een schuivende actie als gevolg van deze triggering met aanraakbediening.
      event.preventDefault();
    }

// Kies de aanraakpositie ten opzichte van de linkerbovenhoek van het canvas
     // Wanneer we de onbewerkte waarden van paginaX en pageY hieronder krijgen, houden ze rekening met het scrollen op de pagina
     // maar niet de positie ten opzichte van onze doel-div. We zullen ze aanpassen met behulp van "target.offsetLeft" en
     // "target.offsetTop" om de juiste waarden te krijgen in relatie tot de linkerbovenhoek van het canvas.
    function getTouchPos(e) {
      if (!e)
        clearCanvas(canvas, ctx);
      var e = event;

      if (e.touches) {
        if (e.touches.length == 1) { // 1 vinger
          var touch = e.touches[0]; // info van vinger #1
          touchX = touch.pageX - touch.target.offsetLeft;
          touchY = touch.pageY - touch.target.offsetTop;
        }
      }
    }


    function init() {
      canvas = document.getElementById('canvas');

// Als de browser de canvastag ondersteunt, krijgt u de 2D-tekencontext voor dit canvas
      if (canvas.getContext)
        ctx = canvas.getContext('2d');

// Controleer of we een geldige context hebben om op / te tekenen voordat event-handlers worden toegevoegd
      if (ctx) {
// Reageer op muisgebeurtenissen op het canvas en mouseup op het hele document
        canvas.addEventListener('mousedown', canvas_mouseDown, false);
        canvas.addEventListener('mousemove', canvas_mouseMove, false);
        window.addEventListener('mouseup', canvas_mouseUp, false);

        // Reageren op aanraakgebeurtenissen op het canvas
        canvas.addEventListener('touchstart', canvas_touchStart, false);
        canvas.addEventListener('touchmove', canvas_touchMove, false);
      }
    }
