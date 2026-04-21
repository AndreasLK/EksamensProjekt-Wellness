# ADR 0001: Afgrænsning til en enkelt receptionistbruger

## Status
Accepteret

## Kontekst
Casebeskrivelsen for BookRight Klinik & Wellness ApS nævner, at systemet er et internt administrationsværktøj til "receptionisterne". Med tre nuværende lokationer i Vejle-området og en fjerde, der snart åbner, ville et virkelighedsnært scenarie involvere flere brugere ved forskellige skriveborde. De tekniske krav specificerer dog også, at projektet er en "prototype", der skal færdiggøres på fire uger. Uklarheden mellem "flere brugere" og "én brugerrolle" skaber en risiko for over-engineering af tilstandshåndteringen (state management).

## Beslutning
Vi har besluttet at designe og implementere prototypen under forudsætning af en **enkelt aktiv receptionistbruger.**
* **Brugeridentitet:** Systemet vil ikke implementere login eller multi-tenant sessionshåndtering; det vil starte direkte op i receptionistens dashboard.
* **Samtidighed (Concurrency):** Integritet på databaseniveau vil stadig blive opretholdt via EF Core concurrency tokens (som en "sikkerhedsforanstaltning"), men applikationslogikken vil ikke blive optimeret til højfrekvente, samtidige redigeringer fra flere fysiske lokationer.

## Konsekvenser
* **Positiv:** Reducerer kompleksiteten, hvilket giver teamet mulighed for at fokusere på Clean Architecture-lagene.
* **Positiv:** Fokus på forretningslogik: Mere tid kan allokeres til den komplekse "bedste pris"-beregning og evaluering af loyalitetsniveauer.
* **Negativ:** Begrænset realisme: Prototypen vil ikke demonstrere, hvordan systemet opfører sig, hvis to receptionister på forskellige klinikker forsøger at booke det samme rum på samme tid.