using Core.Models;
using CSharpFunctionalExtensions;
using DAL.Interfaces;
using Services.Interfaces;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AdminPageService : IAdminPageService
    {
        private readonly IPageRepository _pageRepository;

        public AdminPageService(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }


        public async Task<List<Page>> GetAllActivePagesAsync()
        {
            var entities = await _pageRepository.GetAllActivePagesAsync();
            return entities.Select(e => e.ToDomain().Value).ToList();
        }

        public async Task<List<Page>> GetAllPagesAsycn()
        {
            var entities = await _pageRepository.GetAllPages();
            return entities.Select(e => e.ToDomain().Value).ToList();
        }

        public async Task<Result<Page>> GetPageByIdAsync(Guid id)
        {
            var entity = await _pageRepository.GetPageByIdAsync(id);
            if (entity is null)
            {
                return Result.Failure<Page>($"Page with ID {id} not found");
            }
            return entity.ToDomain();
        }

        public async Task<Result> UpdatePageAsync(Page page)
        {
            var entity = page.ToDAL();
            if (entity == null)
            {
                return Result.Failure("Page cannot be null");
            }
            await _pageRepository.UpdatePageAsync(entity);
            return Result.Success();
        }

        public async Task<Result<Page>> CreatePageAsync(Page page)
        {
            var entity = page.ToDAL();
            if (entity == null)
            {
                return Result.Failure<Page>("Page cannot be null");
            }
            var createdEntity = await _pageRepository.CreatePageAsync(entity);
            return createdEntity.ToDomain();
        }

        public async Task<Result<Page>> CreatePageAsync(Page page, Guid parentId)
        {
            var parent = await _pageRepository.GetPageByIdAsync(parentId);
            if (parent is null)
            {
                return Result.Failure<Page>($"Parent page with ID {parentId} not found");
            }
            var entity = page.ToDAL();
            if (entity == null)
            {
                return Result.Failure<Page>("Page cannot be null");
            }
            entity.Path = parent.Path + $"/{page.Path}";
            entity.ParentId = parent.Id;

            var createdEntity = await _pageRepository.CreatePageAsync(entity);
            return createdEntity.ToDomain();
        }


        public async Task DeletePageAsync(Guid id)
        {
            await _pageRepository.DeletePageAsync(id);
        }

        public async Task<Result<Page>> GetPageByPathAsync(string path)
        {
            var entity = await _pageRepository.GetPageByPathAsync(path);
            if (entity == null)
            {
                return Result.Failure<Page>("Page not found");
            }

            return entity.ToDomain();
        }
    }
}
