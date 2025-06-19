using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealWell.Models;
using static System.Net.WebRequestMethods;
using System.Net.Http.Json;
namespace HealWell.Services
{
    public class AppointmentService
    {
        private  readonly HttpClient _httpClient;


        public AppointmentService(HttpClient http)
        {
            _httpClient= http;
        }
        public async Task<Appointment> GetById(int id)
        {
            try
            {
                var result = await _httpClient.GetFromJsonAsync<Appointment>($"https://localhost:7047/api/Appointments/{id}");
                return result;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Appointment>> GetPatientAppointments(int patientId)
        {
            try
            {
                List<Appointment> appointments = await _httpClient.GetFromJsonAsync<List<Appointment>>($"https://localhost:7047/api/Appointments/patient/{patientId}");
                return appointments;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Appointment>> GetDoctorAppointments(int doctorId)
        {
            try
            {
                List<Appointment> appointments = await _httpClient.GetFromJsonAsync<List<Appointment>>(
                $"https://localhost:7047/api/Appointments/doctor/{doctorId}");
                return appointments;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<Appointment>> GetAllAppointments()
        {
            try
            {
                List<Appointment> appointments = await _httpClient.GetFromJsonAsync<List<Appointment>>(
                $"https://localhost:7047/api/Appointments/GetAll");
                return appointments;
            }
            catch
            {
                return null;
            }
        }
        //public async Task<List<Appointment>> GetAllAppointmentsAsync()
        //{
        //    return await _repo.GetAllAppointmentsAsync();
        //}

        //public async Task<Appointment> GetAppointmentByIdAsync(int id)
        //{
        //    return await _repo.GetAppointmentByIdAsync(id);
        //}

        //public async Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
        //{
        //    return await _repo.GetAppointmentsByPatientIdAsync(doctorId);
        //}

        //public async Task<List<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
        //{
        //    return await _repo.GetAppointmentsByPatientIdAsync(patientId);  
        //}

        //public async Task AddAppointmentAsync(Appointment appointment)
        //{
        //    await _repo.AddAppointmentAsync(appointment);   
        //}

        //public async Task UpdateAppointmentAsync(Appointment appointment)
        //{
        //    await _repo.UpdateAppointmentAsync(appointment);
        //}

        //public async Task DeleteAppointmentAsync(int id)
        //{
        //    await _repo.DeleteAppointmentAsync(id);
        //}
    }
}
