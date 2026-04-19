using Domain.Entities;
using Domain.Entities.Clinics;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Practitioner> _practitionerRepository;
        private readonly IRepository<Clinic> _clinicRepository;
        private readonly ITravelTimeService _travelService;
        private readonly IDateTimeProvider _dateTimeProvider;


        public BookingService(
            IRepository<Booking> bookingRepository,
            IRepository<Practitioner> practitionerRepository,
            IRepository<Clinic> clinicRepository,
            ITravelTimeService travelService,
            IDateTimeProvider dateTimeProvider)
        {
            this._bookingRepository = bookingRepository;
            this._practitionerRepository = practitionerRepository;
            this._clinicRepository = clinicRepository;
            this._travelService = travelService;
            this._dateTimeProvider = dateTimeProvider;
        }


        

    }
}
