# Analyse

## Probleemstelling

Het indoor tracken van assets (drones/robots) door middel van een Ultra Wide Band (UWB) systeem dat we van scratch gaan maken.

## Mindmap
![Github Logo](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/Mindmap.PNG "Mindmap")

## Beschrijving

Een transportbedrijf, dat ook goederen stockeert, wil analyseren of de workflow efficiënter kan. Dit zou kunnen door manueel te kijken of alle goederen efficiënt doorheen het bedrijf bewegen door bij elke verandering van plaats, dit op te schrijven en nadien dit te bekijken. Dit vergt natuurlijk een hele hoop administratie en manueel werk. We zouden een systeem kunnen opstellen dat alle goederen traceert binnen het bedrijf en dit logt naar een centrale database. We zouden in dit systeem gebruik kunnen maken van een drone, die vanuit een database alle locaties en dus ook routes kan ophalen en worden weergegeven op een plattegrond van het bedrijf via een mobiele app of website. De plaatsbepaling kan op enkele manieren gebeuren, maar omdat dit indoor is en toch wel een goede nauwkeurigheid vereist, is GPS geen optie. In dit geval zouden we gebruik kunnnen maken van UWB technologie. Ultra-wideband is een draadloze technologie waarmee een grote hoeveelheid gegevens bij een hoge snelheid kan worden verzonden. Dit is mogelijk door de gegevenspakketjes over een brede frequentieband te spreiden, waardoor ze parallel kunnen worden verzonden. Daardoor zijn gegevenssnelheden tot 1GB per seconde over een afstand van 10 meter mogelijk. De drone zal dan door het magazijn kunnen vliegen en gevisualiseerd worden via een website of een mobiele app en indien nodig, kunnen er bewegingen uitgevoerd worden door een beweging op het scherm van een smartphone. Om te beginnen gaan we testen met een kleine robot. Deze robot zal werken van op de grond.

## Hardware analyse

Tag
![Github Logo](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/Mindmap.PNG "Tag Blokschema")

- ATmega328P
Een microcontroller wordt veel gebruikt in veel projecten en autonome systemen waarbij een eenvoudige, low-powered, low-cost microcontroller nodig is

- DWM1000 (UWB modules)
De DWM1000 is een UWB module. Deze module maakt de locatie van objecten in realtime locatiesystemen (RTLS) tot een nauwkeurigheid van 10 cm binnenshuis mogelijk

- Stepdown
De DC-DC-stepdown-module wordt gebruikt om de ingangsspanning en de uitgangsstabiele 5 volt spanning te verlagen.

- UART module
Deze module is om via Mirco USB naar UART te gaan. 


### Hardware blokdiagram


### Specificatie tabel


### Argumentatie en alternatieven tabel

## Software analyse

Plaats hier een flow-chart van de software. Hierin moet de werking van de software duidelijk worden weergegeven. Voorzie ook de nodige uitleg.

### Data In -en Outputs


### State diagram

#### State diagram mobiele app

![Github Logo](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/state_diagram_app.png "State Diagram App")
#### State diagram drone

![Github Logo](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/StateDiagramUWB.PNG "State Diagram Drone")
![Github Logo](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/state_diagram_localisation.png "State Diagram Localisation")
### Flowchart
![Github Logo](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/Software%20Flowchart.PNG "Software Flowchart")

### Mockup
![Github Logo](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/Mockup_Controls.png "Mockup Controls")
![Github Logo](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/Mockup_Map.png "Mockup Map")

## User stories en Engineering Tasks

Geef hier de userstories en engineering tasks. De beschrijving moet conform zijn met de methode zoals gezien in de lessen  van projectmanagement vn dhr Peeters.

## systeemspecificaties

Geef hier de systeemspecificaties waaruit je de hardware en software kan ontwerpen
