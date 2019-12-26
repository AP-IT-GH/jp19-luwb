# Test Plan
## Getting started
### Setup
1. Download de laatste firmware en Pozyx software: http://tray.cloud.pozyxlabs.com/  
2. Run de software  
3. Op het einde van de installatie wordt er gevraagdt om in te loggen of een account te maken  
4. Zorg ervoor dat de firmware minstens v1.3 is anders update je deze via deze link: https://www.pozyx.io/product-info/developer-tag/firmware  
5. Connecteer de tag of anchor met de computer met behulp van een USB-tag. Als het systeem het device detecteerdt dan wordt het icoontje blauw.
6. Nu kan je naar de web applicatie surfen: https://bapp.cloud.pozyxlabs.com

### Web application
Als het systeem correct is aangesloten open je de web applicatie en log je in.

## Setup and autocalibrate
### Setup screen
In het setup scherm is het mogelijk de anchors en het grondplan in te stellen.

### Discovering the anchors
Het eerste wat je moet doen is het detecteren van alle toestellen, dit doe je door op de knop "discover anchors and tags" te duwen. Deze worden nog niet direct weergegeven op de map want ze hebben nog geen coördinaten. Dit kan manueel of automatisch gebeuren.

### Uploading a floor plan
De applicatie laat he ttoe om een grondplan van de opstelling te uploaden. Momenteel kan je JPEG, PNG of GIF bestanden uploaden.

### Automatically determining the anchor coordinates
Het automatisch calibreren van de anchors laat snelle set-ups toe. Dit kan je doen in de calibratie tab. Momenteel werkt autocalibratie alleen als de hoogte is geweten is.  
Het process kan je starten door op de "start calibration" knop te drukken. Dit kan enige tijd duren. Als de calibratie klaar is dan zie je de opllossing op de kaart.

## Positioning
### Introduction
Als alle coördinaten ingesteld zijn, zou het positioneren automatisch moeten starten. Dit kan je zien in het visualisatie zien.

## Connect with MQTT
### Introduction
Als alles up en running is, kunnen we data uit het systeem halen en er iets mee doen. Dit kan gemakkelijk met het populaire MQTT protocol. MQTT is een IoT protocol dat de sensor data geeft met behulp van publish en subscribe methode. Door te subscribe op de posities, krijg je de nieuwste posities vanaf dat ze beschikbaar zijn.

### Connect with the local stream
Wanneer je de data nodig hebt op je lokale toestel, kan je direct verbinden met de software. Lokaal verbinden wordt aangeraden voor real-time applicaties door de zeer lage latency. Veel programeer talen hebben MQTT libraries(bv. Python).

### Connect through the cloud stream
Door te connecteren met de cloud, kan je de date overal ontvangen.

## Testen
### Up and Running
Eerst kijken we of alle anchors verbonden zijn met de PC en er connectie is.

### Tag Finding
Hier gaan we kijken of de Tag kan gevonden worden in het midden van de locatie.

### Moving
Dan kijken we of hij goed vindbaar is als deze zich verplaatst door de ruimte.

### Out of Bound
Hierbij testen we tot hoever de tag buiten de zone detecteerbaar is.