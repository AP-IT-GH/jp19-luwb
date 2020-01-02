# Pozyx opzetten met eigen software
## 1 Hardware
De anchors dienen op verschillende hoogtes gehangen te worden om een goede Z-positie te kunnen meten. Ook is het best om deze niet in een mooi vierkant en recht over elkaar te hangen om de accuratie te verbeteren.
Als alle anchors opgehangen zijn moeten de coördinaten van alle anchors genoteerd worden ten opzichte van een nul-punt.
Vervolgens moet er 1 anchor met USB-kabel verbonden worden met een Raspberry Pi. Al de andere anchors moeten enkel van stroom voorzien worden.

## 2 Software
Alle scripts die nodig zijn zijn te vinden in src/Pozyx/PozyxStandalone en dienen uitgevoerd te worden vanop de Raspberry Pi.

### 2.1 Config-files
#### 2.1.1 config.ini
Als config.ini niet bestaat of er zijn wijzigingen gebeurt, moet deze aangemaakt of gewijzigd worden met de genoteerde waarden. Hierin worden alle instellingen in verband met de ID's en coördinaten opgeslagen.

1. python3 createConfigFile.py
2. Vervolgens zal het script je door de verschillende vragen nemen en vragen of je deze wilt aanpassen. 
3. Of je al dan niet het geconnecteerde toestel aan de Raspberry Pi wilt aanpassen, en wat de ID dan is van dat achor.
4. Of je al dan niet het remote toestel (meestal de tag) wilt aanpassen, en wat de ID dan is van dat toestel.
5. En of je al dan niet de verschillende anchors wilt aanpassen, en wat de ID dan is van dat anchor alsook de X,Y en Z-coördinaten van dat anchor.

#### 2.1.2 export.ini
Als export.ini niet bestaat of er zijn wijzigingen gebeurt, moet deze aangemaakt of gewijzigd worden. Hierin worden alle instellingen in verband met het exporteren van de positie opgeslagen.

1. python3 createExportFile.py
2. Vervolgens zal het script je door de verschillende vragen nemen en de waarden vragen.
3. Of je al dan niet wilt exporteren naar een file en wat de file name dan is.
4. Of je al dan niet wilt exporteren via MQTT en wat de host, topic en clientID dan zijn.
5. Of je al dan niet wilt exporteren naar de API en wat de URL van de API dan is en wat de Mac naam van de tag dan is.

### 2.2 Discover Devices
Je kan nakijken of alle toestellen van stroom voorzien zijn, binnen bereik zijn en dezelfde UWB-settings hebben door dit script uit te voeren.

1. python3 discoverDevices.py

Als alle toestellen worden weergegeven zijn alle toestellen bereikbaar.

### 2.3 Setup Anchors
Door dit script uit te voeren worden de coördinaten van de anchors opgeslagen in het geheugen van de tag, die deze dan kan gebruiken voor het lokaliseren.

1. python3 setupAnchors.py
2. Eerst wordt de tag zijn lijst van gekende toestellen leeggemaakt.
3. Vervolgens worden de anchors en hun coördinaten die uit de config-file komen, aan de tag zijn lijst toegevoegd.
4. Dan is de tag gereed om gelokaliseerd te worden.

### 2.4 Positioneren
Dit script voert de uiteindelijke lokalisatie uit.

1. python3 positioningTag.py
2. De tag berekend zijn positie aan de hand van de anchors.
3. De positie wordt in de terminal geprint.
4. De positie wordt in de file opgeslagen (indien aan in de instellingen).
5. De positie wordt verstuurd via MQTT (indien aan in de instellingen).
6. De positie wordt met een call naar de API aangepast (indien aan in instellingen).

Dit script is gelooped en dient zelf gestopt te worden.
