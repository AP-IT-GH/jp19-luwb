
var demo = document.getElementById('demo');
var mydata = JSON.parse(data1);
function load() {
  var x = mydata[0].data.coordinates.x;
  var y = mydata[0].data.coordinates.y;
  var z = mydata[0].data.coordinates.z;

    demo.innerHTML = '<svg width="510px" height="282px" viewBox="0 0 1055 581" version="1.1" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink"<g><circle id="0" class="handle" cx="'+x+'" cy="'+y+'" r="5"></circle><text x="'+(x-50)+'" y="'+(y-10)+'">(x: '+x+', y: '+y+', z: '+z+')</text>'+
  /*x cor */
  '<text x="'+(x -50 )+'" y="100%">'+(x-50)+'</text>'+
  '<text x="'+x+'" y="100%">'+x+'</text>'+
  '<text x="'+(x +50 )+'" y="100%">'+(x+50)+'</text>'+
  
  /*y cor */
  '<text y="'+(y -50 )+'" x="0%">'+(y-50)+'</text>'+
  '<text y="'+y+'" x="0%">'+y+'</text>'+
  '<text y="'+(y +50 )+'" x="0%">'+(y+50)+'</text>'+
  
  '</g>.</svg>';
    
}
