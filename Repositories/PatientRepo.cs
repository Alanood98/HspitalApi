using HospitalApi.Models;
using static HospitalApi.Repositories.PatientRepo;
namespace HospitalApi.Repositories
{
        public class PatientRepo : IpatientRepo
    {
            private readonly ApplicationDbContext _context;

            public PatientRepo(ApplicationDbContext context)
            {
                _context = context;
            }

            public IEnumerable<Patient> GetAll()
            {
                return _context.Patients.ToList();
            }

            public Patient GetById(int id)
            {
                return _context.Patients.FirstOrDefault(a => a.PId == id);
            }

            public int Add(Patient patient)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
                return patient.PId;
        }

           

         
        }
    }
