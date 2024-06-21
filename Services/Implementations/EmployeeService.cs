namespace HRM_Project.Services.Implementations
{
    using AutoMapper;
    using global::HRM_Project.DTOs.Request;
    using global::HRM_Project.Exceptions;
    using global::HRM_Project.Models.DB;
    using global::HRM_Project.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using global::HRM_Project.DTOs.Response;

    public class EmployeeService(ApplicationDbContext context, IMapper mapper) : IEmployeeService
    {
        public IQueryable<Employee> Search(string firstName = "", int page = 1, int size = 10, string lastName = "", string position = "", string division = "", string department = "")
        {
            var query = context.Employees
                .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.Division)
                .Include(e => e.Position)
                .Where(e => !e.IsDeleted &&
                            (string.IsNullOrEmpty(firstName) || e.FirstName.Contains(firstName)) &&
                            (string.IsNullOrEmpty(lastName) || e.LastName.Contains(lastName)) &&
                            (string.IsNullOrEmpty(position) || e.Position.Title.Contains(position)) &&
                            (string.IsNullOrEmpty(division) || e.Division.Fullname.Contains(division)) &&
                            (string.IsNullOrEmpty(department) || e.Department.Fullname.Contains(department)))
                .OrderByDescending(e => e.Id)
                .Skip((page - 1) * size)
                .Take(size);

            return query;
        }

        public async Task<List<EmployeeViewDto>> GetAllEmployees()
        {
            var employees = await context.Employees
                .Where(e => !e.IsDeleted)
                .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.Division)
                .Include(e => e.Position)
                .ToListAsync();

            return mapper.Map<List<EmployeeViewDto>>(employees);
        }

        public async Task<EmployeeViewDto> GetByIdAsync(int id)
        {
            var employee = await context.Employees
                .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.Division)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

            if (employee == null)
            {
                throw new ToException(ToErrors.EMPLOYEE_WITH_THIS_ID_NOT_FOUND);
            }

            return mapper.Map<EmployeeViewDto>(employee);
        }

        public async Task<EmployeeViewDto> AddAsync(EmployeeCreateDto create)
        {
            var employee = mapper.Map<Employee>(create);

            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();

            await context.Entry(employee).Reference(e => e.Company).LoadAsync();
            await context.Entry(employee).Reference(e => e.Department).LoadAsync();
            await context.Entry(employee).Reference(e => e.Division).LoadAsync();
            await context.Entry(employee).Reference(e => e.Position).LoadAsync();

            return mapper.Map<EmployeeViewDto>(employee);
        }

        public async Task<EmployeeViewDto> UpdateAsync(EmployeeUpdateDto update)
        {
            var employee = await context.Employees.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == update.Id && !e.IsDeleted);

            if (employee == null)
            {
                throw new ToException(ToErrors.EMPLOYEE_WITH_THIS_ID_NOT_FOUND_FOR_UPDATE);
            }

            employee = mapper.Map<Employee>(update);
            context.Employees.Update(employee);
            await context.SaveChangesAsync();

            await context.Entry(employee).Reference(e => e.Company).LoadAsync();
            await context.Entry(employee).Reference(e => e.Department).LoadAsync();
            await context.Entry(employee).Reference(e => e.Division).LoadAsync();
            await context.Entry(employee).Reference(e => e.Position).LoadAsync();

            return mapper.Map<EmployeeViewDto>(employee);
        }

        public async Task<EmployeeViewDto> DeleteAsync(int id)
        {
            var employee = await context.Employees
                .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

            if (employee == null)
            {
                throw new ToException(ToErrors.EMPLOYEE_WITH_THIS_ID_NOT_FOUND_FOR_DELETE);
            }

            employee.IsDeleted = true;
            await context.SaveChangesAsync();

            return mapper.Map<EmployeeViewDto>(employee);
        }

        public async Task<List<EmployeeViewDto>> GetEmployeesByDepartment(int departmentId)
        {
            var employees = await context.Employees
                .Where(e => e.DepartmentId == departmentId && !e.IsDeleted)
                .Include(e => e.Company)
                .Include(e => e.Division)
                .Include(e => e.Position)
                .ToListAsync();

            return mapper.Map<List<EmployeeViewDto>>(employees);
        }

        public async Task<List<EmployeeViewDto>> GetEmployeesByDivision(int divisionId)
        {
            var employees = await context.Employees
                .Where(e => e.DivisionId == divisionId && !e.IsDeleted)
                .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.Position)
                .ToListAsync();

            return mapper.Map<List<EmployeeViewDto>>(employees);
        }

        public async Task<List<EmployeeViewDto>> GetEmployeesByPosition(int positionId)
        {
            var employees = await context.Employees
                .Where(e => e.PositionId == positionId && !e.IsDeleted)
                .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.Division)
                .ToListAsync();

            return mapper.Map<List<EmployeeViewDto>>(employees);
        }

        public async Task<List<EmployeeViewDto>> GetEmployeesInReserve()
        {
            var employees = await context.Employees
                .Where(e => e.IsInReserve && !e.IsDeleted)
                .Include(e => e.Company)
                .Include(e => e.Department)
                .Include(e => e.Division)
                .Include(e => e.Position)
                .ToListAsync();

            return mapper.Map<List<EmployeeViewDto>>(employees);
        }
    }

}
