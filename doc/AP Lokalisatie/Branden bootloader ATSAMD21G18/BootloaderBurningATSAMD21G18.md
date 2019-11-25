# Stappenplan branden bootloader ATSAMD21G18

## 1. Installeren software
Voor het branden van de bootloader hebben we een bepaald programma nodig genaamd 'Atmel Studio'. 
De URL waar u dit programma kan downloaden is het volgende : [https://www.microchip.com/mplab/avr-support/atmel-studio-7](https://www.microchip.com/mplab/avr-support/atmel-studio-7)
## 2. Downloaden bootloader github
U zal de volgende repository moeten downloaden zodat u de bootloader voor de ATSAMD21G18 ter beschikking heeft.
[https://github.com/sparkfun/SAMD21_Dev_Breakout](https://github.com/sparkfun/SAMD21_Dev_Breakout)
## 3. Aansluitingen hardware
U sluit de J-link EDU aan via usb op uw computer. U sluit de 20 pin naar 10 pin adapter (J-TAG) aan op de J-link EDU. Vervolgens sluit je de ribbon cable aan op de adapter. De volgende signalen zijn de belangrijkste bij het aansluiten van de header:

 - SWDIO ( Grijze female jumper )
 - SWCLK ( Witte female jumper ) 
 - GND ( Paarse en groene female jumper ) 
 - VCC ( Gele female jumper ) 
 - RESET ( Blauwe female jumper ) 
 
![Aansluitingen hardware](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/AansluitingHardware.PNG "Aansluitingen hardware")
![Aansluitingen hardware 1](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/AansluitingHeader1.jpg "Aansluitingen hardware 1")
![Aansluitingen hardware 2](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/AansluitingHeader2.jpg "Aansluitingen hardware 2")
![Aansluitingen hardware 3](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/AansluitingHeader3.jpg "Aansluitingen hardware 3")

## 4. Verwijderen ATSAMD21G18 chip
Zodra het programma geïnstalleerd is, zal u indien u het programma opstart het volgende scherm bekomen:
![Atmel Studio start scherm](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/AtmelStudioStartScherm.PNG "Atmel Studio start scherm")

Vanuit dit scherm gaat u naar **'Tools => Device programming'**. 
![Atmel Studio tools](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/DeviceProgramming.jpg "Atmel Studio tools")

Een nieuw venster zal verschijnen. Nu kan u uw J-LINK EDU connecteren met uw laptop via een USB-poort. Het resultaat zou moeten zijn dat u bij het nieuwe scherm onder **'Tool'** een drop-down heeft, in deze drop-down zou dan de J-LINK moeten staan. Deze kan u dan ook selecteren.
![Atmel Studio programming](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/ProgrammingScreen.jpg "Atmel Studio programming")

Bij de term **'Interface'** moeten we ook specificeren welke interface we zullen gebruiken. Deze moet ingesteld staan op **'SWD'**. Hierna kan u op **'Apply'** drukken.

Indien alles correct aangesloten zou zijn, moet u de ATSAMD21G18 zelf van spanning voorzien ( 3.3V ). Nadat u dit gedaan hebt, kan u onder de term **'Target Voltage'** de knop **'Read'** indrukken. Als alles correct aangesloten zou zijn moet u hier 3.3V uitlezen. Indien er wat onduidelijk is, verwijs ik naar de foto hierboven vermeld.

Vervolgens gaat u naar **'memories'** aan de linkerkant van uw scherm en zal u de chip verwijderen. U doet dit door **'erase chip'** te selecteren uit de drop-down en vervolgens op de knop **'erase'** te drukken. Op het output scherm zou u het volgende moeten bekomen : "Erase...Ok".
![Atmel Studio erase](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/FlashScreen.jpg "Atmel Studio erase")

## 5. Branden bootloader
Nu kan u de zekeringen of **'fuses'** programmeren bij de **'interface settings'** aan de linkerkant van het scherm. Dit doet u door naar het 'fuses' scherm te gaan en op de knop **'program'** te drukken. Dit verifieert de interne registers van de ATSAMD21-chip.
![Atmel Studio fuses](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/Fuses.jpg "Atmel Studio fuses")

Uiteidenlijk gaan we terug naar het menu **'memories'**. Klik onder de balk met het label **'flash'** op het pictogram **'Bladeren naar bestand'** en zoek de map waarin u het zip bestand hebt opgehaald dat u van github hebt gekregen met het label "SAMD21_Dev_Breakout-master" onder Firmware, zorg ervoor dat u naar alle bestandstypes zoekt, want we zullen het samd21_sam_ba_sparkfun.bin bestand gebruiken. Klik op het bestand en selecteer openen. Klik ten slotte op de knop **'Program'** en u krijgt een antwoord in het output scherm met de melding: "Verifying Flash...OK". Gefeliciteerd, je hebt de SAMD21-chip succesvol opgestart!
