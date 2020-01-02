# Script Overview
## Dit is de documentatie van de POZYX Scripts
Onder src/Pozyx vind je 3 mappen met scripts het gebruik ervan wordt hieronder uitgelegd.

## 1 PozyxSoftware
Om deze scripts te gebruiken moet de Pozyx software aan het runnen zijn op een computer en moet er een Pozyx toestel met USB-kabel verbonden zijn.

### 1.1 connectLocal.py
Dit script verbind met de Pozyx software op een computer in het lokale netwerk.
Het print de coördinaten in de terminal en kan deze exporteren naar een txt-file.
Het IP-adres van de host waar de software op draait moet ingevuld worden onder host.

### 1.2 connectCloud.py
Dit script verbind met de software via de cloud gebruik makend van een topic, username en password.
Daarna worden de coördinaten geprint in de terminal.
Het topic, username en password moeten in het script ingevuld worden, en kan men vinden in de Pozyx software.

## 2 PozyxRPi
Dit zijn vooral test scripts en worden niet meer gebruikt.

### 2.1 pyTest.py
Dit script kijkt of er een Pozyx toestel is aangesloten.
Als er geen error wordt gegeven is alles in orde en is het toestel juist verbonden.

### 2.2 readingData.py
Kijkt na of het succesvol data uit het toestel kan halen.
Als "0x43" wordt geprint is alles in orde.

### 2.3 UWBsettings.py
Dit script haalt de UWB instellingen uit het toestel en print deze.

### 2.4 positioning.py
Dit script haalt de positie uit de direct verbonden tag en print dit af in de terminal.
De coördinaten kunnen ook geëxporteerd worden naar een txt-file

## 3 PozyxStandalone
Dit zijn de scripts waar we gebruik van maken en die niet afhankelijk zijn van de Pozyx software.
Bij de meeste scripts moeten er dingen worden ingesteld, hiervoor maken we gebruik van config-files.

### 3.1 Config-files
Omdat de meeste scripts dezelfde instellingen nodig hebben, zijn er config-files.

Er zijn 2 soorten files:

1. scripts die de vragen stellen over de instellingen
2. ini-files waarin de instellingen worden opgeslagen

We hebben 2 aparte config-files gemaakt:

1. Coördinaten en de ID's 
2. Exporteren

#### 3.1.1 createConfigFile.py
Dit script stelt de vragen voor de coördinaten en ID's:

1. Geconnecteerde toestel
2. Remote toestel(Meestal de tag)
3. ID's en coördinaten van de anchors

Deze slaagt deze waarden dan op in config.ini.

#### 3.1.2 config.ini
Hierin worden de ingestelde waarden van createConfigFile.py opgeslagen.
En deze file wordt gelezen door al de scripts die de coördinaten en ID's nodig hebben.

#### 3.1.3 createExportFile.py
Dit script stelt de vragen over het exporteren:

1. Exporteren naar een file?
2. Naam van de file
3. Exporteren via MQTT?
4. MQTT Host
5. MQTT Topic
6. MQTT ClientID
7. Exporteren via API?
8. API URL
9. API MAC Name

Deze slaagt al deze waarden dan op in export.ini

#### 3.1.4 export.ini
Hierin worden de ingesltelde waarden van createExportFile.py opgeslagen.
Deze file wordt dan ingelezen door de scripts die de export-settings nodig hebben.

### 3.2 Scripts
Hier staan de scripts die iets uitvoeren op het Pozyx toestel.

#### 3.2.1 discoverDevices.py
Het zoekt naar al de Pozyx toestellen binnen bereik met dezelfde UWB-instellingen vanuit het standpunt van het geconnecteerde toestel.
En print deze naar de terminal.

#### 3.2.2 setCoordinates.py
Dit script zet de ingestelde coördinaten in het geheugen van die toestellen.
Daarna worden al de coördinaten nog geprint ter controle.
Hierna kan getCoordinates.py uitgevoerd worden.
Dit is niet nodig voor positioning.

#### 3.2.3 getCoordinates.py
Deze haalt de coördinaten van de toestellen uit het geheugen van die toestellen en print deze naar de terminal.
Dit kan gebruikt worden na setCoordinates.py.

#### 3.2.4 setupAnchors.py
Dit wordt uitgevoerd op het remote device (tag).
Eerst wordt de device list leeg gemaakt. Daarna worden de ID's en coördinaten van de anchors opgeslagen in het geheugen van het device.
Optioneel kan dit worden opgeslagen in het flash-geheugen, maar dit staat in commentaar voor test doeleinden.
Hierna kan getDeviceCoordinates.py worden uitgevoerd.
Dit is nodig om positioning te kunnen uitvoeren.

#### 3.2.5 getDeviceCoordinates.py
Dit script wordt uitgevoerd op het remote device (tag).
Deze haalt de coördinaten van de anchors uit het geheugen van het device en print deze in de terminal.
Dit kan worden uitgevoerd na setupAnchors.py

#### 3.2.6 positioningTag.py
Dit is het belangrijkste script.
Dit wordt uitgevoerd op het remote device (tag).
Om dit script te kunnen uitvoeren moet setupAnchors.py eerst uitgevoerd zijn.
Eerst wordt de positie van de tag berekend en wordt die geprint naar de terminal.
Vervolgens wordt afhankelijk van de export-settings de positie geëxporteerd naar een file, via MQTT of naar de API.
Dit script zit in een loop en dient manueel gestopt te worden.
