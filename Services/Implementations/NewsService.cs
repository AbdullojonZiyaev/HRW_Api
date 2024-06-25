using AutoMapper;
using HRM_Project.DTOs.Request;
using HRM_Project.DTOs.Response;
using HRM_Project.Exceptions;
using HRM_Project.Models;
using HRM_Project.Models.DB;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM_Project.Services.Implementations
{
    public class NewsService(ApplicationDbContext context, IMapper mapper) : INewsService
    {
        public IQueryable<News> Search(string title = "", string content = "", string tags = "", int page = 1, int size = 10)
        {
            return context.News
                .Where(n => !n.IsDeleted &&
                            (string.IsNullOrEmpty(title) || n.Title.Contains(title)) &&
                            (string.IsNullOrEmpty(content) || n.Content.Contains(content)) &&
                            (string.IsNullOrEmpty(tags) || n.Tags.Contains(tags)))
                .OrderByDescending(n => n.Id)
                .Skip((page - 1) * size)
                .Take(size)
                .AsQueryable();
        }

        public async Task<List<NewsViewDto>> GetNews()
        {
            return mapper.Map<List<NewsViewDto>>(
                await context.News.Where(n => !n.IsDeleted).ToListAsync()
            );
        }

        public async Task<NewsViewDto> GetByIdAsync(int id)
        {
            var news = await context.News.FirstOrDefaultAsync(n => n.Id == id && !n.IsDeleted);

            if (news == null) throw new ToException(ToErrors.NEWS_WITH_THIS_ID_NOT_FOUND);
            return mapper.Map<NewsViewDto>(news);
        }

        public async Task<NewsViewDto> AddAsync(NewsCreateDto createDto)
        {
            var news = mapper.Map<News>(createDto);
            await context.News.AddAsync(news);
            await context.SaveChangesAsync();
            return mapper.Map<NewsViewDto>(news);
        }

        public async Task<NewsViewDto> UpdateAsync(NewsUpdateDto updateDto)
        {
            var news = await context.News.AsNoTracking().FirstOrDefaultAsync(n => n.Id == updateDto.Id && !n.IsDeleted);

            if (news == null) throw new ToException(ToErrors.NEWS_WITH_THIS_ID_NOT_FOUND_FOR_UPDATE);

            news = mapper.Map<News>(updateDto);
            context.News.Update(news);
            await context.SaveChangesAsync();
            return mapper.Map<NewsViewDto>(news);
        }

        public async Task<NewsViewDto> DeleteAsync(int id)
        {
            var news = await context.News.FirstOrDefaultAsync(n => n.Id == id && !n.IsDeleted);

            if (news == null) throw new ToException(ToErrors.NEWS_WITH_THIS_ID_NOT_FOUND);

            news.IsDeleted = true;
            await context.SaveChangesAsync();
            return mapper.Map<NewsViewDto>(news);
        }
    }
}