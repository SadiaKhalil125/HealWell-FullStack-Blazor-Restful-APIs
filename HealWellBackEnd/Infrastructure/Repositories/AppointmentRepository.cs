using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly HealWellDbContext _context;


        public AppointmentRepository(HealWellDbContext context)
        {
            _context = context;
        }

        public async Task<List<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<List<Appointment>> GetAppointmentsByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a=>a.Patient)
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }

        public async Task<List<Appointment>> GetAppointmentsByPatientIdAsync(int patientId)
        {
            return await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Patient)
                .Where(a => a.PatientId == patientId)
                .ToListAsync();
        }

        public async Task AddAppointmentAsync(Appointment appointment)
        {
            appointment.Patient = _context.Patients.Where(p => p.Id == appointment.PatientId).FirstOrDefault();
            appointment.Doctor = _context.Doctors.Where(d => d.Id == appointment.DoctorId).FirstOrDefault();
            var doctor = _context.Doctors.Where(d=>d.Id ==  appointment.DoctorId).FirstOrDefault();
            doctor.AppointmentCount++;
            _context.Doctors.Update(doctor);
            _context.SaveChanges();
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppointmentAsync(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Appointment>> TotalAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }
    }
}


