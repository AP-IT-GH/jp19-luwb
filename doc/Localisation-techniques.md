# Lokalisatie technieken

## Two-way ranging

Deze techniek zendt pakketjes heen-en-weer tussen anchor en tag. Door de tijd tussen de signalen te bepalen kunnen we de afstand bepalen tussen anchor en tag. Deze tijd wordt de Time of Flight(ToF) genoemd.

![Visuele voorstelling tag-anchor](https://github.com/AP-Elektronica-ICT/jp19-luwb/blob/master/doc/img/Communicatie%20tag%20&%20anchor.png "Visuele voorstelling van tag-anchor")

distance = ToF x (speed of light)
ToF = ( (TRR-TSP) - (TSR-TRP) + (TRF-TSR) - (TSF-TRR) ) /4