# Booking Reservation Flow (SSD)

This document describes the real-time reservation logic, including in-memory leasing, heartbeat synchronization, and SignalR broadcasts to prevent double-booking.

## Sequence Diagram

```mermaid
---
config:
  theme: redux-dark-color
  look: classic
  sequence:
    mirrorActors: true
---
sequenceDiagram
    participant CA as Blazor Client A
    participant CB as Blazor Client B
    participant S as Server (.NET 10)
    participant RAM as In-Memory Storage
    participant DB as Database

    Note over CA, DB: 1. CLIENT A RESERVES 10:15 - 11:45
    CA->>S: ReserveRange(10:15, 11:45)
    S->>RAM: Check Local Overlap
    S->>DB: Check Confirmed Overlap
    alt [Range is Free]
        S->>RAM: StoreLease(ClientA, 10:15-11:45)
        S-->>CA: Success (LeaseID: 555)
        par SignalR Broadcast
            S-->>CA: RangeReserved(10:15-11:45, ClientA)
            S-->>CB: RangeReserved(10:15-11:45, ClientA)
        end
    else [Overlap Detected]
        S-->>CA: Error: Range Taken
    end

    Note over CA, DB: 2. CLIENT B ATTEMPTS OVERLAPPING RANGE (11:00 - 12:30)
    CB->>S: ReserveRange(11:00, 12:30)
    S->>RAM: Check Local Overlap
    Note over RAM: Finds overlap with Lease 555 (11:00 - 11:45)
    alt [Overlap Detected]
        S-->>CB: Error: Range 11:00-11:45 blocked by Client A
        Note over CB: UI triggers Conflict Popup
    else [Range is Free]
        Note over S: (This path is not taken in this scenario)
    end

    Note over CA, DB: 3. THE HEARTBEAT (EVERY 30s)
    loop Heartbeat
        CA->>S: RenewLease(555)
        S->>RAM: Update Expiry Timestamp
        S-->>CA: Updated
    end

    Note over CA, DB: 4. FINAL CONFIRMATION
    CA->>S: ConfirmBooking(555, Details)
    S->>DB: Save Permanent Booking
    S->>RAM: RemoveLease(555)
    S-->>CA: Booking Complete
    par SignalR Broadcast
        S-->>CA: RangeConfirmed(10:15-11:45)
        S-->>CB: RangeConfirmed(10:15-11:45)
    end

    Note over CA, DB: 5. SAFETY CLEANUP (REAPER)
    loop Every 60s
        S->>RAM: Periodic Check
        RAM->>RAM: Find Expired Leases
        par SignalR Broadcast
            S-->>CA: RangeAvailable(Details)
            S-->>CB: RangeAvailable(Details)
        end
    end