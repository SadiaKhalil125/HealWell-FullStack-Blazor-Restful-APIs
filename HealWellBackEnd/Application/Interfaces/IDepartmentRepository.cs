using Domain.Models;
namespace Application.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<Department> GetById(int id);
        Task AddDepartment(Department department);
        Task DeleteDepartment(int id);
        Task UpdateDepartment(Department department);
        Task<List<Department>> GetAll();
    }
}
