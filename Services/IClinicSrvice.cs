using HospitalApi.Models;

namespace HospitalApi.Services
{
    public interface IClinicService
    {
        Clinic GetClinicById(int id);
        List<Clinic> GetAllClinics();
        string AddClinic(Clinic clinic);

    }
}