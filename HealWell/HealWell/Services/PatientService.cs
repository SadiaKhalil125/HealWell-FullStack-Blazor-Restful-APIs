using HealWell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
using System.Net.Http.Json;
using static HealWell.Pages.ResetPassword;
//using Microsoft.AspNetCore.Components;
//using Microsoft.Extensions.Logging;

namespace HealWell.Services
{
    public class PatientService
    {
        private readonly HttpClient _httpClient;
        private readonly ITokenService _service;

        public PatientService(HttpClient httpClient, ITokenService service)
        {
            _httpClient = httpClient;
            _service = service;
        }
        public async Task<bool> register(Patient patient)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7047/api/Patients/addPatient", patient);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Registration failed. Please try again.");
                return false;
            }
          
        }
        public async Task<int> loginandReturnId(LoginModel model)
        {
            try
            {
                // Simulate API call
                //await Task.Delay(1500);
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7047/api/Patients/check-login", model);
                int Id;
                if (response.IsSuccessStatusCode)
                {

                    var responsemsg = await _httpClient.PostAsJsonAsync($"https://localhost:7047/api/Patients/getId", model);
                    Id = await responsemsg.Content.ReadFromJsonAsync<int>();

                    return Id;
                    // Navigate or save token, etc.
                }
                else
                {
                    return -1;
                }
                // Successful login - redirect to dashboard

            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid email or password. Please try again.");
                return -1;
            }

        }
        public async Task SetToken(LoginModel model)
        {
            try
            {
                // Simulate API call
                //await Task.Delay(1500);
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7047/api/Auth/login", model);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadFromJsonAsync<JwtTokenResponse>();
                    await _service.SetTokenAsync(json.token);

                }
                // Successful login - redirect to dashboard

            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid email or password. Please try again.");
                // return null;
            }

        }
        public async Task<Patient> GetById(int id)
        {
            try
            {
                Patient patient;
                var response = await _httpClient.GetAsync($"https://localhost:7047/api/Patients/{id}");
                patient = await response.Content.ReadFromJsonAsync<Patient>();
                return patient;
            }
            catch
            {
                return null;
            }

        }
        public async Task<List<Prescription>> GetAllPrescriptions(int patientId)
        {
            List<Prescription> prescriptions = await _httpClient.GetFromJsonAsync<List<Prescription>>($"https://localhost:7047/api/Patients/GetPrescriptions/{patientId}");
            return prescriptions;
        }
        public async Task<List<HealthRecord>> GetAllHealthRecords(int patientId)
        {
            List<HealthRecord> healthRecords = await _httpClient.GetFromJsonAsync<List<HealthRecord>>($"https://localhost:7047/api/Patients/GetHealthRecords/{patientId}");
            return healthRecords;
        }
        public async Task<bool> EditPatient(Patient patient)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7047/api/Patients/{patient.Id}", patient);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> AddPrescription(Prescription prescription)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7047/api/Doctors/AddPrescription", prescription);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<bool> ResetPassword(ResetPasswordModel model)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7047/api/Patients/ResetPassword", model);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Patient>>("https://localhost:7047/api/Patients");
        }
        public async Task<int> GetTotalPresc()
        {
            return await _httpClient.GetFromJsonAsync<int>("https://localhost:7047/api/Patients/TotalPrescriptions");
        }
        public async Task<int> GetTotalHealthRecords()
        {
            return await _httpClient.GetFromJsonAsync<int>("https://localhost:7047/api/Patients/TotalHealthRecords");
        }
        public async Task<List<Testimonial>> GetTestimonials()
        {
            return await _httpClient.GetFromJsonAsync<List<Testimonial>>("https://localhost:7047/api/Patients/AllTestimonials");
        }

        //public async Task<Patient> GetByIdAsync(int id)
        //{
        //    return await _repo.GetByIdAsync(id);
        //}

        //public async Task<Patient> AddAsync(Patient patient)
        //{
        //   return await _repo.AddAsync(patient);
        //}

        //public async Task<bool> UpdateAsync(Patient patient)
        //{
        //    return await _repo.UpdateAsync(patient);
        //}

        //public async Task<bool> DeleteAsync(int id)
        //{
        //   return await _repo.DeleteAsync(id);
        //}
        //public async Task<bool> CheckLoginAsync(string email, string password)
        //{
        //    return await _repo.CheckLoginAsync(email, password);    
        //}
        //public async Task<int> GetPatientId(LoginModel model)
        //{
        //   return await _repo.GetPatientId(model);  

        //}
        //public async Task<Patient> GetByEmailAsync(string email)
        //{
        //    return await _repo.GetByEmailAsync(email);
        //}
        //public async Task<List<Prescription>> GetAllPrescriptionListAsync(int patientId)
        //{
        //    return await _repo.GetAllPrescriptionListAsync(patientId);
        //}
        //public async Task<List<HealthRecord>> GetAllHealthRecordListAsync(int patientId)
        //{
        //    return await _repo.GetAllHealthRecordListAsync(patientId);
        //}
    }
}
