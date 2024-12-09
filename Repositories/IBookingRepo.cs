using HospitalApi.Models;

namespace HospitalApi.Repositories
{
    public interface IBookingRepo
    {
        int Add(Booking booking);
        IEnumerable<Booking> GetAll();
        Booking GetById(int id);
        Booking ViewAppointmentByClinic(int clinicID);
        Booking ViewAppointmentByPatient(int patientID);

    }
}
