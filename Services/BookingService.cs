﻿using HospitalApi.Models;
using HospitalApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace HospitalApi.Services
{
    public class BookingService : IBookingService
    {

        private readonly IBookingRepo _bookingRepo;
        private readonly IpatientRepo _patientRepo;
        private readonly IClinicReop _clinicReop;

        public BookingService(IBookingRepo bookingRepo , IpatientRepo patientRepo, IClinicReop clinicReop)
        {
            _bookingRepo = bookingRepo;
            _patientRepo = patientRepo;
            _clinicReop = clinicReop;

        }

        public List<Booking> GetAllAppointments()
        {
            var appointment = _bookingRepo.GetAll()
                .OrderBy(p => p.BookingId)
                .ToList();
            if (appointment == null || appointment.Count == 0)
            {
                throw new InvalidOperationException("No appointment found.");
            }
            return appointment;
        }

        public Booking ViewAppointmentByClinic(int clinicID)
        {
            return _bookingRepo.ViewAppointmentByClinic(clinicID);
        }


        public Booking ViewAppointmentByPatient(int patientID)
        {
            return _bookingRepo.ViewAppointmentByPatient(patientID); 
        }


        public int AddAppointment(string patientName, int clinicId, DateTime date, int slotNumber)
        {
            // Validate patient
            var patient = _patientRepo.GetAll()
                .FirstOrDefault(p => p.PName.Equals(patientName, StringComparison.OrdinalIgnoreCase));
            if (patient == null)
            {
                throw new ArgumentException($"Patient with name '{patientName}' does not exist.");
            }

            // Validate clinic
            var clinic = _clinicReop.GetById(clinicId);
            if (clinic == null)
            {
                throw new ArgumentException($"Clinic with ID '{clinicId}' does not exist.");
            }

            // Check slot availability
            var existingBookings = _bookingRepo.GetAll()
                .Where(b => b.CId == clinicId && b.Date.HasValue && b.Date.Value.Date == date.Date)
                .ToList();

            if (slotNumber < 1 || slotNumber > clinic.NumberOfSlots)
            {
                throw new ArgumentException($"Slot number must be between 1 and {clinic.NumberOfSlots}.");
            }

            if (existingBookings.Any(b => b.SlotNumber == slotNumber))
            {
                throw new InvalidOperationException($"Slot {slotNumber} is already booked for the selected date.");
            }

            // Add booking
            var booking = new Booking
            {
                PId = patient.PId,
                CId = clinic.CId,
                Date = date,
                SlotNumber = slotNumber
            };

            return _bookingRepo.Add(booking);
        }


    }
}
