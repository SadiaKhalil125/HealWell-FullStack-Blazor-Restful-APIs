using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
namespace Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly HealWellDbContext _context;

        public DepartmentRepository(HealWellDbContext context)
        {
            _context = context;
        }
        public async Task<Department?> GetById(int id)
        {
            return await _context.Departments.Where(s => s.Id == id).FirstOrDefaultAsync(); ;
        }
        public async Task AddDepartment(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteDepartment(int id)
        {
            Department department = _context.Departments.Where(d => d.Id == id).FirstOrDefault();
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateDepartment(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Department>> GetAll()
        {
            return _context.Departments.ToList();
        }
    }
}
