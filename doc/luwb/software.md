# 1. Software
## 1.1 Geruikte software
•	Tag + anchors met Arduino (C/C++)
•	Opslagen data lokaal op Raspberry Pi (via PHP)
•	Data wordt doorgestuurd door de Raspberry Pi (python script)
•	Data wordt opgeslagen in een ASP.NET server
•	Data wordt visueel weergegeven in Angular
## 1.2 Software structuur
### 1.2.1 Tag en Anchor
Via de DWM1000 chips wordt de afstand tot de tag bepaald. De anchors die gebruik maken van een Arduino Pro Mini sturen hun data door over een Ethernet kabel naar het adres 192.168.3.3. De data wordt verstuurd in JSON formaat en ziet er als volgt uit:
{"MAC_TAG":"TAGx","MAC_ANCHOR":"ANCHORy","DISTANCE":z}
Met: x het tagnummer, y het anchornummer en z de afstand van de anchor tot aan de tag
Voorbeeld: {"MAC_TAG":"TAG5","MAC_ANCHOR":"ANCHOR1", "DISTANCE":5}
### 1.2.2 Raspberry Pi
De Raspberry Pi doet 2 dingen. Langs de ene kant slaagt deze de data via een php apache server op en langs de andere kant stuurt deze de data door via MQTT.
- Het index.php bestand slaat bij een POST van data de data op als volgt:
1. Maakt de map data aan in /var/www/html indien deze nog niet bestaat.
2. Maakt in de map data een map TAGx aan met x = het tagnummer in de data van de POST indien deze nog niet bestaat.
3. Maakt in de map TAGx een bestand ANCHORy aan met y = het anchornummer in de data van de POST indien deze nog niet bestaat.
4. Schrijft in het bestand ANCHORy de afstand samen met datum op. Deze data wordt opgeslagen in het volgende formaat: 
distance,unixtimestamp
Met: distance de afstand van anchor naar tag en unixtimestamp de datum
Voorbeeld: 5,1557920157
- Het postscriptMQTT.py stuurt de data door via MQTT om de 200 ms.
1. Leest de mappen uit die zich in de data map bevinden (/var/www/html/data).
2. Het script staat ingesteld om enkel de data uit de map TAG5 te lezen.
3. Alle bestanden in de TAG5 worden uitgelezen en de data wordt doorgestuurt via MQTT in het volgende formaat: 
TAGx;ANCHORy;distance;unixtimestamp
Met: x het tagnummer, y het anchornummer, distance de afstand van anchor naar tag en unixtimestamp de datum
Voorbeeld: TAG5;ANCHOR1;-4;1557475973
### 1.2.3 ASP.NET server
De ASP.NET server haalt de data op van MQTT. Deze data wordt dan opgeslagen in de database.
### 1.2.4 Webapplicatie met Angular framework
Op de website wordt visueel weergegeven waar de tag zich bevindt t.o.v. de anchors.