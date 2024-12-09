using HospitalApi.Models;
namespace HospitalApi.Repositories
{
    public interface IClinicReop
    {
        string AddClinic(Clinic clinic);
        IEnumerable<Clinic> GetAll();
        Clinic GetById(int id);
    }
}
