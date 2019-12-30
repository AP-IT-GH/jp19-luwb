# Location Ultra Wide Band
## 1 Wat is UWB?
UWB (Ultra Wide Band) is een draadloze technologie die gebruikt wordt om met hoge snelheid grote hoeveelheid data door te sturen. Het UWB signaal is zeer robust en kan door dunne muren. Wij gebruiken deze techniek voor het localiseren van een beweegbaar object (drone/robot). 
## 2 Algemene begrippen/technologiën
### 2.1 MQTT
MQTT staat voor Message Queueing Telemetry Transport. Dit is een machine-tot-machine data transfer protocol. MQTT is gebasseerd op TCP/IP en heeft een kleine overhead (2-byte header). MQTT heeft een centrale bemiddelaar, de MQTT-broker. Deze interface is voor alle verzonden berichten. De betrokken apparaten communiceren dus niet met elkaar en kennen elkaar dus onderling niet. Het is de taak van de broker om berichten te accepteren en door te geven aan de juiste ontvanger(s).
### 2.2 DWM module
De DWM module, of specifieker de DWM1000 module is gebasseerd op Decawave's DW1000 IC. Deze module maakt het mogelijk om objecten real-time te lokaliseren (RTLS: real time location systems) tot op een nauwkeurigheid van 10cm binnen. De datacommunicatie kan gaan tot een snelheid van 6,8 Mbps met een bereik tot 300 meter. 
### 2.3 SQL
SQL staat voor Structured Query Language. Dit is een ANSI/ISO-standaardtaal voor databasemanagement systemen (DBMS). Dit is dus een gestandaliseerde taal die gebruikt wordt voor het opslagen, opvragen, aanpassen en verwijderen van gegevens in een relationele database.
### 2.4 Relationele Database
Een relationele database is een database die is opgebouwd volgens het relationeel model. Alle gegevens worden opgeslagen in tabellen waarbij rijen de soortgelijke informatie (records) vormen en de kolommen zijn de informatie die voor elk record moet worden opgeslagen. Verschillende tabbellen kunnen met elkaar verbonden worden door een extra kolom waarin een verwijzing staat naar een record van een andere tabel.
## 3 De 2 projecten
### 3.1 Project 1: LUWB self-made
Dit project is een eigen ontworpen systeem dat gebruik maakt van de UWB technologie. De bedoeling is dat we met zelfgemaakte anchors een robot kunnen localiseren met een tag aan boord. De tag communiceerd dan met de anchors en er wordt berekend hoe ver deze zich van elkaar bevinden. Op basis hiervan wordt dan berekend waar de robot zich op dat moment bevindt. Dit wordt dan visueel weergegeven.
#### 3.1.1 Hoe werkt het?
##### 1. Huidig concept:
De tag (robot) die werkt op een Arduino met een DWM module communiceerd met de anchors die werken op 2 Arduino's met een DWM module. Deze 2 delen dan informatie met elkaar uit via UWB en zo wordt er bepaald wat de afstand is tussen de tag en de anchors. Deze data wordt doorgestuurd over een ethernetkabel naar een switch en komt terecht bij een Raspberry Pi. Op deze Raspberry draait dan een script die deze data op zijn beurt doorstuurt via MQTT. Een server die lokaal of op een cloud draait lijst dan deze data uit en slaagt deze op in een SQL database. Met deze data wordt er berekend waar de tag (robot) zich exact bevindt.  Dan wordt vanuit een webapplicatie die gemaakt is met Angular de data opgevraagd. Dit wordt dan visueel weergegeven op de website.
#### 2. Veranderingen die zijn gebeurd:
De data werd eerst rechtstreeks doorgestuurd naar de server die in de cloud stond via threading in het python script. Dit zorgde voor verschillende problemen en werkte vrij traag. Ook was het luisteren naar de socket via een apache server niet de beste manier.
Dit is vervangen door 1 enkel pythonscript die luistert naar de socket en de data doorstuurd via MQTT. De server die op het moment lokaal draait op een laptop wacht op zijn beurt af tot er nieuwe MQTT data beschikbaar is.
#### 3. Veranderingen die nog moeten gebeuren:
1. Op het moment werken de anchors op 2 arduino's. Deze worden vervangen door 1 enkele chip genaamd atsamd21g18.
2. Het voorzien van spanning via een adapter wordt vervangen door PoE (Power over Ethernet).
3. Het visueel weergeven moet zowel op een website als op een app kunnen weergegeven worden.
4. De drone of robot moet kunnen worden bestuurd via de app/website door een lijn te tekenen en deze dan te laten volgen. Voorlopig kan de robot via een aparte app wel verplaatst worden door middel van 4 knoppen (voorruit/achterruit/links/rechts).

### 3.2 Project 2: Pozyx
---Hier komt nog tekst---




# Bronnen:
- MQTT: [at-automation](https://www.at-automation.nl/knowledge-base/mqtt-message-queueing-telemetry-transport-wat-is-het/) [ct](https://www.ct.nl/achtergrond/iot-protocol-mqtt-betrouwbaar-data/)
- DWM: [decawave](https://www.decawave.com/product/dwm1000-module/) 
- SQL: [wikipedia](https://nl.wikipedia.org/wiki/SQL) 
- Relationele Database: [wikipedia](https://nl.wikipedia.org/wiki/Relationele_database) 