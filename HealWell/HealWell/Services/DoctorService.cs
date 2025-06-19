using HealWell.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace HealWell.Services
{
    public class DoctorService
    {
        private readonly HttpClient _httpClient;
        public DoctorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<int> loginandReturnId(DoctorLoginModel model)
        {
            try
            {
                // Simulate API call
                //await Task.Delay(1500);
                var response = await _httpClient.PostAsJsonAsync("https://localhost:7047/api/Doctors/check-login", model);
                int Id;
                if (response.IsSuccessStatusCode)
                {

                    var responsemsg = await _httpClient.PostAsJsonAsync($"https://localhost:7047/api/Doctors/GetId", model);
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
        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Doctor>>("https://localhost:7047/api/Doctors/GetAll");
        }
        public async Task<Doctor> GetById(int id)
        {
            try
            {
                var doctor = await _httpClient.GetFromJsonAsync<Doctor>($"https://localhost:7047/api/Doctors/{id}");
                return doctor;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> UpdateDoctor(Doctor doctor)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7047/api/Doctors/{doctor.Id}", doctor);
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
       
        public async Task<string> GetImageUrl(string url)
        {
            return $"https://localhost:7047/images/{url}";
        }
        public async Task<int>GetDoctorId(string email)
        {
            return await _httpClient.GetFromJsonAsync<int>($"https://localhost:7047/api/Doctors/GetId/{email}");
        }
        public async Task<int> GetNumber()
        {
            return await _httpClient.GetFromJsonAsync<int>("https://localhost:7047/api/Doctors/GetCount");
        }
        public async Task<List<Service>> GetAllServices()
        {
            return await _httpClient.GetFromJsonAsync<List<Service>>("https://localhost:7047/api/Doctors/GetAllServices");
        }
        public async Task<Service> GetService(int id)
        {
            return await _httpClient.GetFromJsonAsync<Service>($"https://localhost:7047/api/Doctors/GetService/{id}");
        }


        //public async Task<List<Doctor>> GetDoctorsWithFilterAsync(string? search = null, string? specialty = null)
        //{
        //    var filters = new List<string>();

        //    if (!string.IsNullOrWhiteSpace(search))
        //    {
        //        filters.Add($"(contains(Name,'{search}') or contains(Specialty,'{search}') or contains(Hospital,'{search}'))");
        //    }

        //    if (!string.IsNullOrWhiteSpace(specialty))
        //    {
        //        filters.Add($"Specialty eq '{specialty}'");
        //    }

        //    var filterQuery = filters.Count > 0 ? $"?$filter={string.Join(" and ", filters)}" : "";

        //    // Ensure no double slashes
        //    var baseUrl = "https://localhost:7047/odata/ODataDoctor/getodata";
        //    var url = $"{baseUrl}{filterQuery}";
        //    //await IJSRuntime.InvokeVoidAsync("console.log", "Requesting URL: " + url);
        //    Console.WriteLine("Requesting URL: " + url);

        //    var response = await _httpClient.GetFromJsonAsync<List<Doctor>>(url);

        //    return response ?? new List<Doctor>();
        //}
        public async Task<List<Doctor>> GetDoctorsWithFilterAsync(string? search = null, string? specialty = null)
        {
            var filters = new List<string>();

            if (!string.IsNullOrWhiteSpace(search))
            {
                // Normalize to lowercase for case-insensitive search
                var lowerSearch = search.ToLower();
                filters.Add($"(contains(tolower(Name),'{lowerSearch}') or contains(tolower(Specialty),'{lowerSearch}') or contains(tolower(Hospital),'{lowerSearch}'))");
            }

            if (!string.IsNullOrWhiteSpace(specialty))
            {
                // Also normalize specialty comparison to lowercase
                var lowerSpecialty = specialty.ToLower();
                filters.Add($"tolower(Specialty) eq '{lowerSpecialty}'");
            }

            var filterQuery = filters.Count > 0 ? $"?$filter={string.Join(" and ", filters)}" : "";

            var baseUrl = "https://localhost:7047/odata/ODataDoctor/getodata";
            var url = $"{baseUrl}{filterQuery}";

            Console.WriteLine("Requesting URL: " + url);

            var response = await _httpClient.GetFromJsonAsync<List<Doctor>>(url);

            return response ?? new List<Doctor>();
        }



    }
}
