# Documentatie van de website

**Topics die aan bod zullen komen**
- Algemene informatie
- Opstarten van de website
- De verschillende componenten
- De verschillende services

## Algemene informatie

Deze server is gemaakt in Angular. Je kan deze files aanpassen en bekijken met verschillende programma's. Wij raden aan om het te openen met Visual Studio Code. De naam van de folder waar alle files zich bevinden is WebAppLUWB me als path \src\visualisatie\WebAppLUWB.

## Opstarten van de website
**Lokaal**
- Installeren van de dependencies: Voor het lokaal runnen van de website moet je de nodige dependencies en modules installeren. Hiervoor typ je 'npm install' in een terminal of Windows Powershell. 

> **Note:** Zorg er voor dat je bij dit en volgende commando's in de folder zelf zit of je in de terminal typt die je onderaan vindt bij Visual Studio Code.

- Het starten van de server kan zeer gemakkelijk door het commando 'npm start' in de terminal te typen. De webserver zal nu opstarten en je kan je website terugvinden onder het domein 'http://localhost:4200'. Je zal automatisch geredirected worden naar de homepage van de webserver (http://localhost:4200/home).

## De verschillende componenten

- **Home component:** Deze component bevat alle code voor het tonen van de home page. Op deze pagina vindt u een kleine samenvatting van de 2 projecten alsook een foto.
- **Map component:** Deze component bevat alle code voor het tonen van de map page. Hier worden alle anchors en tags getoond op een kaart.
- **PageNotFound component:** Deze component wordt weergegeven wanneer de gebruik een route ingeeft die niet bestaat.
- **TagsAndAnchors component:** Deze component bevat alle code voor het tonen van de anchors en tags in een lijst. Hier is het mogelijk anchors/tags te verwijderen/toevoegen/updaten.
- **Toolbar component:** Deze component bevat alle code voor het tonen van de toolbar. Dus wanneer er een nieuwe route nodig is of een bestaande wordt aangepast moet je de logica en de layout aanpassen in deze component. Via het [routerlink] attribuut kan je ingeven naar welke route er moet genavigeerd worden.

## De verschillende services

- **Callapi Service** Deze service wordt gebruikt voor alle gets/posts en puts naar de API. Hier is het dus belangrijk dat de variabele genaamd domain juist is ingesteld. Deze moet ingesteld zijn op de link waar de api zich bevindt.
> **Tip:** Als je de api lokaal runt zal dit de standaard link zijn en die is als volgt: https://localhost:44321/api/
> Na de laatste / komt de naam van de controller (bv.  https://localhost:44321/api/tags)
