using Microsoft.AspNetCore.Mvc;
using HospitalApi.Services;
using HospitalApi.Models;
using static HospitalApi.Models.Patient;
using static HospitalApi.Models.Clinic;


namespace HospitalApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClinicController : ControllerBase
    {
        private readonly IClinicService _clinicServices;

        public ClinicController(IClinicService clinicServices)
        {
            _clinicServices = clinicServices;
        }

        // GET: api/Clinic/GetAllClinics
        [HttpGet("GetAllClinics")]
        public ActionResult<List<Clinic>> GetAll()
        {
            try
            {
                var clinics = _clinicServices.GetAllClinics();
                if(clinics==null)
                return Ok(clinics);
                else return Ok(clinics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Clinic/GetClinicById/{id}
        [HttpGet("GetClinicById/{id}")]
        public ActionResult<Clinic> GetClinicById(int id)
        {
            try
            {
                var clinic = _clinicServices.GetClinicById(id);
                return Ok(clinic);
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

        //// POST: api/Clinic/AddClinic
        //[HttpPost("AddClinic")]
        //public IActionResult AddClinic( specialization specialization,int NumberOfSlots)
        //{
        //    try
        //    {
        //        if (specialization == null)
        //        {
        //            return BadRequest("Clinic data is null");
        //        }
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        var clinic = new Clinic
        //        {
        //            Specialization = specialization,
        //            NumberOfSlots = NumberOfSlots,

        //        };
        //        _clinicServices.AddClinic(clinic);
        //        return CreatedAtAction(nameof(AddClinic), new { id = clinic.CId }, clinic);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        return StatusCode(500, "Error ");
        //    }
        //}

        [HttpPost]
        public IActionResult AddClinic(string specialization, int NumberOfSlots)
        {
            try
            {
                string newClinicId = _clinicServices.AddClinic(new Clinic
                {
                    Specialization = specialization,
                    NumberOfSlots = NumberOfSlots
                });
                return Created(string.Empty, newClinicId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
