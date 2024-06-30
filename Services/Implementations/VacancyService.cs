using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models;
using HRM_Project.Models.DB;
using HRM_Project.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class VacancyService(ApplicationDbContext context, IMapper mapper) : IVacancyService
{
    public async Task<List<VacancyViewDto>> GetAllAsync()
    {
        var vacancies = await context.Vacancies
            .Include(v => v.Company)
            .Include(v => v.Department)
            .Include(v => v.Division)
            .Where(v => !v.IsDeleted)
            .ToListAsync();

        return mapper.Map<List<VacancyViewDto>>(vacancies);
    }

    public async Task<VacancyViewDto> GetByIdAsync(int id)
    {
        var vacancy = await context.Vacancies
            .Include(v => v.Company)
            .Include(v => v.Department)
            .Include(v => v.Division)
            .FirstOrDefaultAsync(v => v.Id == id && !v.IsDeleted);

        return vacancy == null
            ? throw new ToException(ToErrors.VACANCY_WITH_THIS_ID_NOT_FOUND)
            : mapper.Map<VacancyViewDto>(vacancy);
    }

    public async Task<VacancyViewDto> AddAsync(VacancyCreateDto createDto)
    {
        // Check for existence of related entities
        if (!context.Companies.Any(c => c.Id == createDto.CompanyId && !c.IsDeleted))
            throw new ToException(ToErrors.COMPANY_WITH_THIS_ID_NOT_FOUND);

        if (!context.Departments.Any(d => d.Id == createDto.DepartmentId && !d.IsDeleted))
            throw new ToException(ToErrors.DEPARTMENT_WITH_THIS_ID_NOT_FOUND);

        if (!context.Divisions.Any(d => d.Id == createDto.DivisionId && !d.IsDeleted))
            throw new ToException(ToErrors.DIVISION_WITH_THIS_ID_NOT_FOUND);

        var vacancy = mapper.Map<Vacancy>(createDto);
        await context.Vacancies.AddAsync(vacancy);
        await context.SaveChangesAsync();
        return mapper.Map<VacancyViewDto>(vacancy);
    }

    public async Task<VacancyViewDto> UpdateAsync(VacancyUpdateDto updateDto)
    {
        var vacancy = await context.Vacancies.FindAsync(updateDto.Id);

        if (vacancy == null || vacancy.IsDeleted)
            throw new ToException(ToErrors.VACANCY_WITH_THIS_ID_NOT_FOUND);

        mapper.Map(updateDto, vacancy);
        context.Vacancies.Update(vacancy);
        await context.SaveChangesAsync();
        return mapper.Map<VacancyViewDto>(vacancy);
    }

    public async Task<VacancyViewDto> DeleteAsync(int id)
    {
        var vacancy = await context.Vacancies.FindAsync(id);

        if (vacancy == null || vacancy.IsDeleted)
            throw new ToException(ToErrors.VACANCY_WITH_THIS_ID_NOT_FOUND);

        vacancy.IsDeleted = true;
        await context.SaveChangesAsync();
        return mapper.Map<VacancyViewDto>(vacancy);
    }

    public IQueryable<Vacancy> Search(string title = "", int page = 1, int size = 10)
    {
        return context.Vacancies
            .Include(v => v.Company)
            .Include(v => v.Department)
            .Include(v => v.Division)
            .Where(v => !v.IsDeleted && (string.IsNullOrEmpty(title) || v.Title.Contains(title)))
            .Skip((page - 1) * size)
            .Take(size)
            .AsQueryable();
    }
}
