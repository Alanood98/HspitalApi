using Microsoft.AspNetCore.Mvc;
using HospitalApi.Services;
using HospitalApi.Models;
using System.Reflection;
using static HospitalApi.Models.Patient;

namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientServices _patientServices;

        public PatientController(IPatientServices patientServices)
        {
            _patientServices = patientServices;
        }

        // GET: api/Patient/GetAllPatient
        [HttpGet("GetAllPatient")]
        public ActionResult<List<Patient>> GetAll()
        {
            try
            {
                var patients = _patientServices.GetAllPatients();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Patient/GetPatientById/{id}
        [HttpGet("GetPatientById/{id}")]
        public ActionResult<Patient> GetPatientById(int id)
        {
            try
            {
                var patient = _patientServices.GetPatientById(id);
                return Ok(patient);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddPatient")]
        public IActionResult AddPatient(string patientName, int Age, Gen gender)
        {
            try
            {
                if (patientName == null)
                {
                    return BadRequest("Patient data is null");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var patient = new Patient
                {
                    PName = patientName,
                    Age = Age,
                    gender = gender
                };
                _patientServices.AddPatient(patient);
                return CreatedAtAction(nameof(AddPatient), new { id = patient.PId }, patient);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return StatusCode(500, "Error ");
            }



        }
    }
}
