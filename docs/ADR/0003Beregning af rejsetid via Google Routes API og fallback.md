# ADR 0003: Beregning af rejsetid via Google Routes API og Fallback

## Status
Accepteret

## Kontekst
Præcis rejsetid er afgørende for at maksimere klinikkens kapacitet. Overvurdering af rejsetid fører til spildte timer for behandleren, mens undervurdering fører til forsinkede aftaler og dårlig kundeservice.

## Beslutning
Vi vil implementere en automatiseret service til beregning af rejsetid i **Infrastructure-laget**.
* **Primær metode:** Systemet vil kalde **Google Routes API** for at beregne den estimerede rejsetid mellem afgangsklinikken og destinationsklinikken.
* **Fallback-strategi:** Hvis API'et ikke kan nås, anmodningen timer ud, eller API-nøglen er ugyldig, vil systemet som standard bruge **hårdkodet værdier** ved enhver klinikoverførsel i løbet af dagen.
* **Implementering:** Dette vil blive håndteret som en `async` I/O-bundet opgave for at sikre, at Blazor-brugerfladen forbliver responsiv under beregningen.

## Konsekvenser
* **Positiv:** Giver høj nøjagtighed for receptionisten og reducerer den manuelle mentale belastning ved at beregne "er 30 minutter nok til at nå til Egtved?".
* **Positiv:** 45-minutters fallback sikrer, at systemet er "fejlsikkert" og forbliver operationelt selv uden internetadgang eller API-kreditter.
* **Negativ:** Introducerer en ekstern afhængighed af Googles infrastruktur.
* **Negativ:** Kræver håndtering af API-hemmeligheder og potentielle omkostninger forbundet med Google Maps-platformen.