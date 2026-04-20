# ADR 2: Travel Time Calculation via Google Routes API and Fallback

## Status
Accepted

## Context
Accurate travel time is essential to maximize clinic capacity. Overestimating travel time leads to wasted practitioner hours, while underestimating it leads to delayed appointments and poor customer service.

## Decision
We will implement an automated travel time calculation service within the **Infrastructure layer**.
* **Primary Method:** The system will call the **Google Routes API** to calculate the estimated travel time between the origin clinic and the destination clinic.
* **Fallback Strategy:** If the API is unreachable, the request times out, or the API key is invalid, the system will default to a **hard-coded 45-minute safety gap** for any intra-day clinic transfer.
* **Implementation:** This will be handled as an `async` I/O-bound task to ensure the Blazor UI remains responsive during the calculation.

## Consequences
* **Positive:** Provides high accuracy for the receptionist, reducing the manual mental load of calculating "is 30 minutes enough to get to Egtved?"
* **Positive:** The 45-minute fallback ensures the system is "fail-safe" and remains operational even without internet access or API credits.
* **Negative:** Introduces an external dependency on Google’s infrastructure.
* **Negative:** Requires handling API secrets and potential costs associated with the Google Maps Platform.
