using DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPageRepository
    {
        Task<PageEntity?> GetPageByPathAsync(string slug);
        Task<PageEntity?> GetPageByIdAsync(Guid id);
        Task<List<PageEntity>> GetAllActivePagesAsync();
        Task<PageEntity> CreatePageAsync(PageEntity page);
        Task UpdatePageAsync(PageEntity page);
        Task DeletePageAsync(Guid id);
        Task<List<PageEntity>> GetChildPagesAsync(Guid parentId);
        public Task<List<PageEntity>> GetAllPages();

    }
}
