# ADR 0002: Understøttelse af behandleres transport i løbet af dagen

## Status
Accepteret

## Kontekst
BookRight Klinik & Wellness ApS driver tre klinikker i Vejle-området og deler behandlere mellem lokationer. Selvom casebeskrivelsen nævner, at behandlere opholder sig på én klinik en hel dag, fremhæver den også den nuværende kompleksitet ved, at behandlere arbejder i forskellige byer (f.eks. Vejle og Egtved) på den samme dag. For at håndtere "worst-case scenariet" for planlægning og forhindre de hyppige dobbeltbookinger, som klinikken oplever i øjeblikket, skal systemet tage højde for den fysiske tid, det kræver for en behandler at flytte sig mellem klinikkerne.

## Beslutning
Vi vil implementere en planlægningsmodel, der gør det muligt at tildele behandlere til forskellige kliniklokationer inden for den samme arbejdsdag.
* **Valideringslogik:** Enhver bookinganmodning, der involverer et skift af lokation for behandleren, skal bestå et tjek for "Validering af rejsetid".
* **Klinik-agnostisk planlægning:** Tilgængelighed vil ikke være bundet til en enkelt klinik per dag; i stedet vil den blive defineret af tidsintervaller og tilhørende lokations-ID'er i databasen.
* **Ressourcebegrænsning:** Systemet skal verificere, at en behandler ikke blot er "ledig", men også "på den korrekte lokation" eller har tilstrækkelig tid til at nå den næste lokation.

## Konsekvenser
* **Positiv:** Løser direkte forretningsproblemet med "mistede aftaler" og "planlægningskompleksitet", som er nævnt i casen.
* **Positiv:** Fremtidssikrer systemet i forhold til den planlagte åbning af en fjerde klinik.
* **Negativ:** Øger kompleksiteten af use casen "Opret booking", da den nu kræver lokationsbevidst validering frem for simple tjek af tidsintervaller.