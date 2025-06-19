using Application.Interfaces;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DoctorService
    {
        private readonly IDoctorRepository _repo;
        public DoctorService(IDoctorRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            List<Doctor> doctors = await _repo.GetAllAsync();
            foreach(var doctor in doctors) 
            {
                doctor.Education =await _repo.getEducation(doctor.Id);
                doctor.AvailableDateTimes =await _repo.getAvailableDates(doctor.Id);
                doctor.AvailableDays =await _repo.getAvailableDays(doctor.Id);

            }
            return doctors;
        }
        public IQueryable<Doctor> GetAll()
        {
            return _repo.GetAll().AsQueryable();
        }


        public async Task<Doctor?> GetByIdAsync(int id)
        {
            Doctor doctor = await _repo.GetByIdAsync(id);
            doctor.Education = await _repo.getEducation(doctor.Id);
            doctor.AvailableDateTimes = await _repo.getAvailableDates(doctor.Id);
            doctor.AvailableDays = await _repo.getAvailableDays(doctor.Id);

            return doctor;
        }

        public async Task<Doctor> AddAsync(Doctor doctor)
        {
            return await _repo.AddAsync(doctor);
        }

        public async Task<bool> UpdateAsync(Doctor doctor)
        {
            return await _repo.UpdateAsync(doctor);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repo.DeleteAsync(id); 
        }
        public async Task AddPrescription(Prescription prescription)
        {
            await _repo.AddPrescription(prescription);
        }
        public async Task AddHealthRecord(HealthRecord healthrecord)
        {
            await _repo.AddHealthRecord(healthrecord);
        }

        public async Task<int> GetDoctorId(string email)
        {
            return await _repo.GetDoctorId(email);  
        }
        public async Task<bool> CheckLoginAsync(string name, string email)
        {
            return await _repo.CheckLoginAsync(name, email);    
        }
        public async Task<int> GetTotalNoOfDoctors()
        {
            return await _repo.GetTotalNoOfDoctors();

        }
        public async Task<List<Service>> GetAllServices()
        {
            return await _repo.GetAllServices();
        }
        public async Task<Service> GetService(int id)
        {
            return await _repo.GetService(id);
        }
    }
}
