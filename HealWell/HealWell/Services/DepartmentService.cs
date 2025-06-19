using HealWell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace HealWell.Services
{
    public  class DepartmentService
    {
        private readonly HttpClient _httpClient;

        public DepartmentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
      
        //public async Task<Department?> GetById(int id)
        //{
        //    return await _repo.GetById(id);
        //}
        //public async Task AddDepartment(Department department)
        //{
        //    await _repo.AddDepartment(department);  
        //}
        //public async Task DeleteDepartment(int id)
        //{
        //   await _repo.DeleteDepartment(id);
        //}
        //public async Task UpdateDepartment(Department department)
        //{
        //    await _repo.UpdateDepartment(department);
        //}
        //public async Task<List<Department>> GetAll()
        //{
        //    return await _repo.GetAll();    
        //}
    }
}
