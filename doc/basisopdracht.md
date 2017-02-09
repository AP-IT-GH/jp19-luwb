# Basis opdracht

## Korte beschrijving

Voor het vak Smart Systems moet u een sturing maken voor een voertuig met enige
vorm van intelligentie en draadloze communicatie. Wat deze intelligentie
word, is beschreven in de uitbreiding.

Het voertuig bouw je op met een chassis dat de school ter beschikking stelt. Dit
chassis beweegt zich voor d.m.v. rupsbanden, deze worden aangedreven door 2 DC
motoren. Je zorgt er voor dat de wagen zich in alle richtingen kan begeven,
versnellen en afremmen. Voor het aansturen van de motor maak je een sturing
gebaseerd op h-brug.

Met behulp van draadloze communicatie stuur je commando's naar het voertuig
waardoor het zich kan bewegen en eventuele debug informatie ontvangt. Voor dit
schrijf je een kleine applicatie. Hiervoor mag je gebruik maken van je
Raspberry Pi. Het draadloze protocol is vrij te kiezen.

Al dit combineer je in een schakeling die je zowel op breadboard, protoboard
maakt. Als deze schakeling gevalideerd maak je hier een PCB van.

Het school stelt het materiaal ter beschikking. Je mag ook eigen materiaal
voorzien, op eigen verantwoordelijkheid.

## Uitwerking

### Algemene werking

We werken Agile door gebruik te maken van de scrum methodologie. De docenten
zijn de Product Owners en de studenten het development team. Het developmenteam
is zelfsturend en verantwoordelijk voor het eindresultaat. Samen bouwen we de
product backlog op.

We werken in sprints van 2 weken waarna we telkens een review en retrospective.

De tools die we hiervoor gebruiken zijn ZenHub en GitHub. Deze tools moeten het
duidelijk maken wie welke bijdrage heeft geleverd aan het project.

We maken gebruik van github voor het opslagen van alle project bestanden. Dit
geld ook voor PCB ontwerpen en alle relevante documentatie van het project.

Per groep maak je bij de start van de opdracht een verkennende analyse/design
van het voertuig. Hierin word er verwacht dat je een highlevel blok diagramma
maakt. Van elk blok beschrijf je de functionaliteit die er in huist. Deze kan
je al gebruiken voor epics te maken voor je backlog, die je daarna indeelt in
kleinere meetbare deeltjes.

Zorg ervoor dat je individueel kan werken aan het project. Hiervoor is een
goede taakbeschrijving / verdeling nodig. Een goede analyse maakt dit een
eenvoudige taak. Spreek dus goed af wat de input en de output is van de
verschillende blokken in het systeem. Eens dit vast gelegd is kan je de nodige
data simuleren en kan je beter individueel werken. Spreek bepaalde tijdstippen
af om verschillende stukken van het project te integreren.

Tijdens het labo leg de nadruk op de hardware, de nodige software kan je thuis
schrijven en integreren in het labo.

Het systeem moet ook gevalideerd worden. Hiervoor gebruiken we het commando
centrum. Dit zorgt ervoor dat we feedback krijgen van ons voertuig. Dit maakt
van ons commando centrum een testbank.

### Hardware 

Voor de hardware gedeelte van dit project, moet je een schema ontwerpen waarin
alle hardware staat beschreven. Je zal dit schema op 2 manieren realiseren. Op:
* Breadboard
* PCB

Een dat het schema klaar is, kan je de hardware op 2 manieren beginnen
opbouwen. Dit moet niet door dezelfde persoon. Valideer de verschillende
opstellingen.

Je bent volledig vrij in het ontwerp van het schema. Je mag een bestaand schema
als startpunt gebruiken. Geef de nodige credit.

### Software
Beschrijf de nodige input en out van het systeem en transleer dit naar een
specifiek ontwerp. 

Als voorbeeld een motorbrug is vraag meestal 6 inputs, 4 outputs heeft een
specifiek waarheidstabel. Je zult dus een motor klasse moeten maken met een
constructor met min. 6 inputs. Doe dit voor heel het project.


## Beschikbare materialen
* Robot chassis
  * Rupsbanden
  * 7,2V motoren
* P -en N-Mosfets voor hoog vermogen
* Teensy 3.2
* Arduino boards
