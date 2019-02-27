# Lokalisatie technieken

## Two-way ranging

Deze techniek zendt pakketjes heen-en-weer tussen anchor en tag. Door de tijd tussen de signalen te bepalen kunnen we de afstand bepalen tussen anchor en tag. Deze tijd wordt de Time of Flight (ToF) genoemd. De afstand wordt bepaalt tussen de anchor en de tag, dus niet meteen het hele systeem maar de afstand tussen 1 van de anchors in het systeem. Zo kunnen we de afstand in een radius rond elke tag bepalen en zo de cirkels laten snijden om zo de locatie van de tag bepalen.

![Visuele voorstelling tag-anchor](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/Communicatie_tag_&_anchor.png "Visuele voorstelling van tag-anchor")

distance = ToF x (speed of light)  
ToF = ( (TRR-TSP) - (TSR-TRP) + (TRF-TSR) - (TSF-TRR) ) /4  
  
## Time Difference of Arrival

Bij deze techniek zijn de anchors met elkaar verbonden. De tag zendt een signaal uit dat de anchors ontvangen. Vanaf het eerst signaal is ontvangen wordt die tijd opgeslagen. als de andere tags het signaal ontvangen kijken ze hoeveel langer het had geduurt t.o.v. de tijd van het eerste ontvangen signaal.
  
## TWR vs TDoA
![TWR vs TDoA](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/TWR_vs_TDoA.png "Voorstelling tussen TWR & TDoA")