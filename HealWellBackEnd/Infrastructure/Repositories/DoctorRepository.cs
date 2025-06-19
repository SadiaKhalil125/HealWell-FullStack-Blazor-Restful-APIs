// DoctorRepository.cs

using Application.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repositories
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HealWellDbContext _context;

        public DoctorRepository(HealWellDbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            var doctors = await _context.Doctors.Include(d => d.Department).ToListAsync();

            for (int i = 0; i < doctors.Count; i++)
            {
                var email = doctors[i].Email;

                // Query each property individually using FirstOrDefault or SingleOrDefault
                var edu = _context.Doctors
                                  .Where(d => d.Email == email)
                                  .Select(d => d.Education)
                                  .FirstOrDefault();

                var availableDates = _context.Doctors
                                             .Where(d => d.Email == email)
                                             .Select(d => d.AvailableDateTimes)
                                             .FirstOrDefault();

                var availableDays = _context.Doctors
                                            .Where(d => d.Email == email)
                                            .Select(d => d.AvailableDays)
                                            .FirstOrDefault();

                Console.WriteLine(doctors[i].Education[0]);
                Console.WriteLine(doctors[i].AvailableDateTimes[0]);
                Console.WriteLine(doctors[i].AvailableDays[0]);
                // Assign the properties
                doctors[i].Education = edu ?? new List<string>();
                doctors[i].AvailableDateTimes = availableDates ?? new List<DateTime>();
                doctors[i].AvailableDays = availableDays ?? new List<string>();

            }

            return doctors;
            //return await _context.Doctors.Include(d => d.Department).ToListAsync();
        }
        public IEnumerable<Doctor> GetAll()
        {
            var doctors = _context.Doctors.Include(d => d.Department).ToList();

            for (int i = 0; i < doctors.Count; i++)
            {
                var email = doctors[i].Email;

                // Query each property individually using FirstOrDefault or SingleOrDefault
                var edu = _context.Doctors
                                  .Where(d => d.Email == email)
                                  .Select(d => d.Education)
                                  .FirstOrDefault();

                var availableDates = _context.Doctors
                                             .Where(d => d.Email == email)
                                             .Select(d => d.AvailableDateTimes)
                                             .FirstOrDefault();

                var availableDays = _context.Doctors
                                            .Where(d => d.Email == email)
                                            .Select(d => d.AvailableDays)
                                            .FirstOrDefault();

                // Assign the properties
                doctors[i].Education = edu ?? new List<string>();
                doctors[i].AvailableDateTimes = availableDates ?? new List<DateTime>();
                doctors[i].AvailableDays = availableDays ?? new List<string>();

            }

            return doctors;
        }
        public async Task<List<string>>getEducation(int docId)
        {
            return await _context.Doctors.Where(d => d.Id == docId).Select(d => d.Education).FirstOrDefaultAsync();
        }
        public async Task<List<DateTime>> getAvailableDates(int docId)
        {
            return await _context.Doctors.Where(d => d.Id == docId).Select(d => d.AvailableDateTimes).FirstOrDefaultAsync();
        }
        public async Task<List<string>> getAvailableDays(int docId)
        {
            return await _context.Doctors.Where(d => d.Id == docId).Select(d => d.AvailableDays).FirstOrDefaultAsync();
        }
        public async Task<Doctor?> GetByIdAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            var email = doctor.Email;

            // Query each property individually using FirstOrDefault or SingleOrDefault
            var edu = _context.Doctors
                              .Where(d => d.Email == email)
                              .Select(d => d.Education)
                              .FirstOrDefault();

            var availableDates = _context.Doctors
                                         .Where(d => d.Email == email)
                                         .Select(d => d.AvailableDateTimes)
                                         .FirstOrDefault();

            var availableDays = _context.Doctors
                                        .Where(d => d.Email == email)
                                        .Select(d => d.AvailableDays)
                                        .FirstOrDefault();

            // Assign the properties
            doctor.Education = edu ?? new List<string>();
            doctor.AvailableDateTimes = availableDates ?? new List<DateTime>();
            doctor.AvailableDays = availableDays ?? new List<string>();
            return doctor;
        }

        public async Task<Doctor> AddAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task<bool> UpdateAsync(Doctor doctor)
        {
            var existing = await _context.Doctors.FindAsync(doctor.Id);
            if (existing == null) return false;

            _context.Entry(existing).CurrentValues.SetValues(doctor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return false;

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task AddPrescription(Prescription prescription)
        {
            await _context.Prescriptions.AddAsync(prescription);
        }
        public async Task AddHealthRecord(HealthRecord healthrecord)
        {
            await _context.HealthRecords.AddAsync(healthrecord);
        }
       
        public async Task<int> GetDoctorId(string email)
        {
            var doctor = await _context.Doctors.Where(s=>s.Email == email).FirstOrDefaultAsync();
            return doctor.Id;
        }
        public async Task<bool> CheckLoginAsync(string name, string email)
        {
            return await _context.Doctors.AnyAsync(p => p.Name == name && p.Email == email);
        }
        public async Task<int> GetTotalNoOfDoctors()
        {
            return _context.Doctors.Count();
        }
        public async Task<List<Service>> GetAllServices()
        {
            return await _context.Services.ToListAsync();
        }
        public async Task<Service> GetService(int id)
        {
            return await _context.Services.Where(s => s.Id == id).FirstAsync();
        }

    }
}
