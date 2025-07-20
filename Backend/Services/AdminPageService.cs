using Core.Models;
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
            return entities.Select(e => e.ToDomain()).ToList();
        }

        public async Task<List<Page>> GetAllPagesAsycn()
        {
            var entities = await _pageRepository.GetAllPages();
            return entities.Select(e => e.ToDomain()).ToList();
        }

        public async Task<Page> GetPageByIdAsync(int id)
        {
            var entity = await _pageRepository.GetPageByIdAsync(id);
            return entity?.ToDomain() ?? throw new Exception($"Page with ID {id} not found");
        }

        public async Task<Page> UpdatePageAsync(Page page)
        {
            var entity = page.ToDAL();
            if (entity == null) throw new ArgumentNullException(nameof(page), "Page cannot be null");

            var updatedEntity = await _pageRepository.UpdatePageAsync(entity);
            return updatedEntity.ToDomain();
        }

        public async Task<Page> CreatePageAsync(Page page)
        {
            var entity = page.ToDAL();
            if (entity == null) throw new ArgumentNullException(nameof(page), "Page cannot be null");

            var createdEntity = await _pageRepository.CreatePageAsync(entity);
            return createdEntity.ToDomain();
        }

        public async Task DeletePageAsync(int id)
        {
            await _pageRepository.DeletePageAsync(id);
        }
    }
}
