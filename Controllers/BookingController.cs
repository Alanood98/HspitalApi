﻿using Microsoft.AspNetCore.Mvc;
using HospitalApi.Services;
using HospitalApi.Models;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: api/Booking/GetAllAppointments
        [HttpGet("GetAllAppointments")]
        public ActionResult<List<Booking>> GetAllAppointments()
        {
            try
            {
                var appointments = _bookingService.GetAllAppointments();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Booking/GetAppointmentByClinic/{clinicID}
        [HttpGet("GetAppointmentByClinic/{clinicID}")]
        public ActionResult<Booking> GetAppointmentByClinic(int clinicID)
        {
            try
            {
                var appointment = _bookingService.ViewAppointmentByClinic(clinicID);
                if (appointment == null)
                {
                    return NotFound($"No appointment found for clinic ID {clinicID}.");
                }
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Booking/GetAppointmentByPatient/{patientID}
        [HttpGet("GetAppointmentByPatient/{patientID}")]
        public ActionResult<Booking> GetAppointmentByPatient(int patientID)
        {
            try
            {
                var appointment = _bookingService.ViewAppointmentByPatient(patientID);
                if (appointment == null)
                {
                    return NotFound($"No appointment found for patient ID {patientID}.");
                }
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        //[HttpPost("AddAppointment")]
        //public IActionResult AddAppointment(string patientName, string ClinicName, DateTime Date)
        //{
        //    try
        //    {
        //        _bookingService.AddAppointment(patientName, ClinicName, Date);
        //        return Created();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        [HttpPost("BookAppointment")]
        public IActionResult AddBook(string namep, int clinicId, DateTime date, int slotNumber)
        {
            try
            {
                _bookingService.AddAppointment(namep, clinicId, date, slotNumber);
                return StatusCode(201); // HTTP 201 Created
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // HTTP 409 Conflict for slot unavailability
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }




    }
}
