using HospitalApi.Models;
namespace HospitalApi.Repositories
{
    public class BookingRepo : IBookingRepo
    {
        private readonly ApplicationDbContext _context;

        public BookingRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Booking> GetAll()
        {
            return _context.Bookings.ToList();
        }

        public Booking GetById(int id)
        {
            return _context.Bookings.FirstOrDefault(a => a.BookingId == id);
        }

        public int Add(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
            return booking.PId;
        }


        public Booking ViewAppointmentByClinic(int clinicID)
        {
            return _context.Bookings.FirstOrDefault(b => b.CId == clinicID);
        }



        public Booking ViewAppointmentByPatient(int patientID)
        {
            return _context.Bookings.FirstOrDefault(p => p.PId == patientID);
        }

    }
}