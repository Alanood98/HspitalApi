﻿using HospitalApi.Models;

namespace HospitalApi.Services
{
    public interface IBookingService
    {
        List<Booking> GetAllAppointments();
        Booking ViewAppointmentByClinic(int clinicID);
        Booking ViewAppointmentByPatient(int patientID);
        int AddAppointment(string patientName, int clinicId, DateTime date, int slotNumber);
    }
}
