using HospitalApi.Models;

namespace HospitalApi.Services
{
    public interface IPatientServices
    {
        List<Patient> GetAllPatients();
        Patient GetPatientById(int id);
        int AddPatient(Patient patient);

    }
}
