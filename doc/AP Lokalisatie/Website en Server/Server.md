# Documentatie van de server

**Topics die aan bod zullen komen**
- Algemene informatie
- Opstarten van de server
	- default connection string
	- SQL Server Object
- Doel van de controllers
- Belangrijke methode's

## Algemene informatie

Er is gebruik gemaakt van een ASP.NET Core REST API met versienummer 3.0. De server is helemaal klaar om online gehost te worden via Azure. De database kan zowel lokaal als online op Azure gehost worden. 

## Opstarten van de server

**Lokaal**
Bij het openen van de server is het aangeraden om gebruik te maken van Visual Studio 2019 of nieuwer. Het openen kan gemakkelijk door de solution Server.sln te openen, deze is te vinden in de map Server. Hierna moet je enkel nog op IIS Express klikken en de server zal opstarten, hierdoor zal database automatisch gegenereerd worden.

> **Note:** Zorg er voor dat de default connection string op de lokale database staat ingesteld. Meer informatie vindt je hieronder.

### Instellen van de default connection string

De default connection string beschrijft de locatie van de gebruikte database. Deze string vind je in de file 'appsettings.json'. Voor het gebruiken van een lokale database gebruik je volgende string:

**Server=(localdb)\\mssqllocaldb;Database=NaamDB**
NaamDB mag eender welke gekozen naam zijn voor de lokale database.

![ConnectionString](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/ConnectionString.JPG "ConnectionString")

Voor het gebruiken van een online database moeten er verschillende variabele meegeven worden. Om te weten welke variabele je moet meegeven, moet je kijken in de documentatie van het gebruikte hostplatform. 

> **Note:** De 2de connection string in de server is voor een database gehost op Azure. Bij het gebruiken van deze string moet je enkel het id, passwoord en de locatie van de server aanpassen.

### Locatie van de lokale database
Als je gekozen hebt om de database lokaal op te slagen, kan je deze als volgt terugvinden:
- Open de server
- Ga naar **View** => **SQL Server Object Explorer**

![SQL Server Object](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/SQLServerObject.png "SQL Server Object")

- Hierna open je de **SQL Server** => **Databases** => **{Gekozen DB naam}**

![DatabaseTablest](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/DatabaseTables.JPG "DatabaseTables")

Alle data die in de database zit zal je ook hier kunnen terugvinden bij het debuggen.

## Doel van de controllers

### TagController
Via deze controller kan je alle tags aanspreken die opgeslagen zijn in de database alsook nieuwe tags toevoegen en bestaande tags verwijderen. Doel van de methodes:
- **GetTagsParam({parameters}):** Elke tag die voldoet aan de meegeven parameters zal worden teruggestuurd. Dit zal een lijst zijn van geen of meerdere tags.
- **GetTag(id:long):** Opvragen van 1 tag die de meegeven id bevat. De methode zal antwoorden met een NotFound als de tag niet gevonden is.
- **Addtag(tag: Tag):** Toevoegen van 1 tag die meegeven wordt in de body van de Post. De tag zal hier opgeslagen worden in de database.
- **UpdateTag(tag: Tag):** Updaten van de meegegeven tag. Als de meegeven tag niet bestaat in de database zal een NotFound worden teruggestuurd.
- **DeleteTag(id: long):** Verwijderen van de meegeven tag. Wanner de tag succesvol is verwijderd zal een status 200 Ok worden teruggestuurd. Ook hier wordt een NotFound teruggestuurd bij een foute id.

### AnchorController
Via deze controller kan je alle anchors aanspreken die opgeslagen zijn in de database alsook nieuwe anchors toevoegen en bestaande anchors verwijderen. Doel van de methodes:
- **GetAnchorsParam({parameters}):** Elke anchor die voldoet aan de meegeven parameters zal worden teruggestuurd. Dit zal een lijst zijn van geen of meerdere anchors.
- **GetAnchor(id:long):** Opvragen van 1 anchor die de meegeven id bevat. De methode zal antwoorden met een NotFound als de anchor niet gevonden is.
- **AddAnchor(anchor: Anchor):** Toevoegen van 1 anchor die meegeven wordt in de body van de Post. De anchor zal hier opgeslagen worden in de database.
- **UpdateAnchor(anchor: Anchor):** Updaten van de meegegeven anchor. Als de meegeven anchor niet bestaat in de database zal een NotFound worden teruggestuurd.
- **DeleteAnchor(id: long):** Verwijderen van de meegeven anchor. Wanner de anchor succesvol is verwijderd zal een status 200 Ok worden teruggestuurd. Ook hier wordt een NotFound teruggestuurd bij een foute id.

### MeasurementController

- **CreateMeasurement(item: Measurement):** Toevoegen van 1 measurement die meegeven wordt in de body. Wanner zowel de tag als de anchor gevonden worden zal de measurement of worden geüpdate als deze al bestaat of worden toegevoegd. Als de tag of anchor ontbreekt zal er een NotFound worden teruggestuurd.
- **GetAllMeasurements():** Alle measurements zullen worden teruggestuurd in een lijst.
- **GetMeasurement(mac: string):** Opvragen van 1 specifieke measurement van een tag met als mac gelijk aan de meegegeven mac. De gevonden measurement zal dan worden teruggestuurd. Als er geen measurement gevonden is wordt er een NotFound teruggestuurd.

## Belangrijke methode's

In de startup.cs: 

- **client_MqttMsgPublishReceived(sender: object, e:MqttMsgPublishEventArgs):** Wordt getriggerd wanneer er een bericht gepubliceerd wordt op de MQTT topic
- **ConnectToBroker():** Connectie maken met de MQTT broker en subscriben op de ingestelde topic
- **SaveMeasurementInDB(payload: string):** Wanneer er een bericht op de MQTT wordt gepost, wordt er gecontroleerd of dit in het juiste formaat is. Wanneer het formaat overeenkomt zal de measurement worden geüpdate of aangepast wanneer deze nog niet bestond. Hierna wordt er gekeken of alle measurements van de tag in de meegestuurde measurement van hetzelfde tijdstip zijn. Als dit zo is zal de locatie van de tag opnieuw berekend worden.

In de Algorithm.cs:
- **Calculate(dataList: List<Data>):** Een data object bevat de afstand van een anchor tot een tag samen met zijn huidige locatie. Deze worden dan meegegeven naar de juiste methode. Voorlopig is dit slechts 1 methode.
- **calculateThreeCircleIntersection(x,y,r (3 keer)):** Deze methode berekend de locatie van de tag. Deze methode zal alleen maar weken bij 2 of 3 anchors. Bij het gebruik van meer anchors moet er een nieuwe methode worden aangemaakt.