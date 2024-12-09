using HospitalApi.Models;

namespace HospitalApi.Repositories
{
    public class ClinicRepo : IClinicReop
    {
        private readonly ApplicationDbContext _context;

        public ClinicRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Clinic> GetAll()
        {
            return _context.Clinics.ToList();
        }

        public Clinic GetById(int id)
        {
            return _context.Clinics.FirstOrDefault(a => a.CId == id);
        }

        public string AddClinic(Clinic clinic)
        {
            _context.Clinics.Add(clinic);
            _context.SaveChanges();
            return clinic.Specialization.ToString();

        }
    }
}