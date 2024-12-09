using HospitalApi.Models;

namespace HospitalApi.Repositories
{
    public interface IpatientRepo
    {
        int Add(Patient patient);
        IEnumerable<Patient> GetAll();
        Patient GetById(int id);
    }
}
