using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM_Project.Services
{
    public interface IEmployeeService
    {
        IQueryable<Employee> Search(string firstName = "", int page = 1, int size = 10, string lastName = "", string position = "", string division = "", string department = "");

        Task<List<EmployeeViewDto>> GetAllEmployees();

        Task<EmployeeViewDto> GetByIdAsync(int id);

        Task<EmployeeViewDto> AddAsync(EmployeeCreateDto create);

        Task<EmployeeViewDto> UpdateAsync(EmployeeUpdateDto update);

        Task<EmployeeViewDto> DeleteAsync(int id);

        Task<List<EmployeeViewDto>> GetEmployeesByDepartment(int departmentId);

        Task<List<EmployeeViewDto>> GetEmployeesByDivision(int divisionId);

        Task<List<EmployeeViewDto>> GetEmployeesByPosition(int positionId);

        Task<List<EmployeeViewDto>> GetEmployeesInReserve();
    }
}