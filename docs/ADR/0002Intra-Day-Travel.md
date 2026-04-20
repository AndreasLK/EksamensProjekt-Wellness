# ADR 1: Support for Intra-day Practitioner Travel

## Status
Accepted

## Context
BookRight Klinik & Wellness ApS operates three clinics in the Vejle area and shares practitioners between locations. While the case description mentions practitioners staying at one clinic for a full day, it also highlights the current complexity of practitioners working in different cities (e.g., Vejle and Egtved) on the same day. To address the "worst-case scenario" for scheduling and prevent the frequent double bookings currently experienced by the clinic, the system must account for the physical time required for a practitioner to move between clinics.

## Decision
We will implement a scheduling model that allows practitioners to be assigned to different clinic locations within the same workday. 
* **Validation Logic:** Every booking request that involves a change in location for the practitioner must pass a "Travel Time Validation" check.
* **Clinic-Agnostic Scheduling:** Availability will not be tied to a single clinic per day; instead, it will be defined by time-slots and associated location IDs in the database.
* **Resource Constraint:** The system must verify that a practitioner is not only "free" but also "at the correct location" or has sufficient time to reach the next location.

## Consequences
* **Positive:** Directly solves the business problem of "lost appointments" and "scheduling complexity" mentioned in the case.
* **Positive:** Future-proofs the system for the planned opening of a fourth clinic.
* **Negative:** Increases the complexity of the "Create Booking" use case, as it now requires location-aware validation rather than simple time-slot checks.
