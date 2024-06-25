using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Models;
using System.Linq;
using System.Threading.Tasks;

public interface INewsService
{
    IQueryable<News> Search(string title = "", string content = "", string tags = "", int page = 1, int size = 10);
    Task<List<NewsViewDto>> GetNews();
    Task<NewsViewDto> GetByIdAsync(int id);
    Task<NewsViewDto> AddAsync(NewsCreateDto createDto);
    Task<NewsViewDto> UpdateAsync(NewsUpdateDto updateDto);
    Task<NewsViewDto> DeleteAsync(int id);
}
