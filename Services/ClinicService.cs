using HospitalApi.Models;
using HospitalApi.Repositories;
using System.Text.RegularExpressions;

namespace HospitalApi.Services
{
    public class ClinicService : IClinicService
    {

        private readonly IClinicReop _clinicRepo;

        public ClinicService(IClinicReop clinicRepo)
        {
            _clinicRepo = clinicRepo;
        }

        public List<Clinic> GetAllClinics()
        {
            var clinic = _clinicRepo.GetAll()
                .OrderBy(c => c.Specialization)
                .ToList();
            if (clinic == null || clinic.Count == 0)
            {
                throw new InvalidOperationException("No clinic found.");
            }
            return clinic;
        }

        public Clinic GetClinicById(int id)
        {
            var clinic = _clinicRepo.GetById(id);
            if (clinic == null)
            {
                throw new KeyNotFoundException("clinic not found.");
            }
            return clinic;
        }

        //public string AddClinic(Clinic clinic)
        //{
        //    _clinicRepo.AddClinic(clinic);



        //}

        public string AddClinic(Clinic clinic)
        {
            if (clinic == null )
            {
                throw new ArgumentException("Invalid clinic details.");
            }

            _clinicRepo.AddClinic(clinic);

            // Return a confirmation message or the specialization name.
            return $"Clinic '{clinic.Specialization}' has been added successfully.";
        }
    }

}

