using HospitalApi.Models;
using HospitalApi.Repositories;
using System.Text.RegularExpressions;

namespace HospitalApi.Services
{
    public class PatientService : IPatientServices
    {

        private readonly IpatientRepo _patientRepo;

        public PatientService(IpatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }

        public List<Patient> GetAllPatients()
        {
            var patient = _patientRepo.GetAll()
                .OrderBy(p => p.PName)
                .ToList();
            if (patient == null || patient.Count == 0)
            {
                throw new InvalidOperationException("No patient found.");
            }
            return patient;
        }

        public Patient GetPatientById(int id)
        {
            var patient = _patientRepo.GetById(id);
            if (patient == null)
            {
                throw new KeyNotFoundException("patient not found.");
            }
            return patient;
        }

        public int AddPatient(Patient patient)
        {
            if (string.IsNullOrWhiteSpace(patient.PName))
            {
                throw new ArgumentException("patient name is required.");
            }
            if (patient.Age <= 0)
            {
                throw new ArgumentException("Patient age must be a positive integer.");
            }
            if (!Enum.IsDefined(typeof(Patient.Gen), patient.gender))
            {
                throw new ArgumentException("Invalid gender. Gender must be Male or Female.");
            }
            _patientRepo.Add(patient);

            return patient.PId;
         

        }

    }
}
