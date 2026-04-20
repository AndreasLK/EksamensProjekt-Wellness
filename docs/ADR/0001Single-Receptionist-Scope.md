# ADR 0001: System Scope Limited to Single Receptionist User

## Status
Accepted

## Context
The case description for BookRight Klinik & Wellness ApS mentions that the system is an internal administration tool for "the receptionists". With three current locations in the Vejle area and a fourth opening soon , a real-world scenario would involve multiple users at different desks. However, the technical requirements also specify that the project is a "prototype" to be completed in four weeks. The ambiguity between "multiple users" and "one user role" creates a risk of over-engineering the state management.

## Decision
We have decided to design and implement the prototype under the assumption of a **single active receptionist user.** * **User Identity:** The system will not implement a login or multi-tenant session management; it will boot directly into the receptionist dashboard.
* **Concurrency:** Database-level integrity will still be maintained via EF Core concurrency tokens (as a "fail-safe"), but the application logic will not be optimized for high-frequency simultaneous edits from multiple physical locations.

## Consequences
* **Positive:** Reduces complexity, allowing the team to focus on the Clean Architecture layers.
* **Positive:** Focus on Business Logic: More time can be allocated to the complex "best price" calculation and the loyalty level evaluations.
* **Negative:** Limited Realism: The prototype will not demonstrate how the system behaves if two receptionists at different clinics try to book the same room at the same time.
