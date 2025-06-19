using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
namespace Application.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<List<Appointment>> GetAllAppointmentsAsync();
        Task<Appointment> GetAppointmentByIdAsync(int id);
        Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId);
        Task<List<Appointment>> GetAppointmentsByPatientIdAsync(int patientId);
        Task AddAppointmentAsync(Appointment appointment);
        Task UpdateAppointmentAsync(Appointment appointment);
        Task DeleteAppointmentAsync(int id);
        Task<List<Appointment>> TotalAppointments();
    }
}
