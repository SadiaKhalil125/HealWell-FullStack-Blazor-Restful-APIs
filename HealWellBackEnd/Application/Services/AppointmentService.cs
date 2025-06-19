using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _repo;


        public AppointmentService(IAppointmentRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _repo.GetAllAppointmentsAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _repo.GetAppointmentByIdAsync(id);
        }

        public async Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            return await _repo.GetAppointmentsByPatientIdAsync(doctorId);
        }

        public async Task<List<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            return await _repo.GetAppointmentsByPatientIdAsync(patientId);  
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            await _repo.AddAppointmentAsync(appointment);   
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            await _repo.UpdateAppointmentAsync(appointment);
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            await _repo.DeleteAppointmentAsync(id);
        }
        public async Task<List<Appointment>> TotalAppointments()
        {
            return await _repo.TotalAppointments();   
        }
    }
}
