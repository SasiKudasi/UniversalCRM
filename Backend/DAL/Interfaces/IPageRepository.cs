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
        Task<PageEntity?> GetPageBySlugAsync(string slug);
        Task<PageEntity?> GetPageByIdAsync(int id);
        Task<List<PageEntity>> GetAllActivePagesAsync();
        Task<PageEntity> CreatePageAsync(PageEntity page);
        Task<PageEntity> UpdatePageAsync(PageEntity page);
        Task DeletePageAsync(int id);
        Task<List<PageEntity>> GetChildPagesAsync(int parentId);
        Task<PageEntity?> GetPageBySlugPathAsync(string[] slugPath);
        public Task<List<PageEntity>> GetAllPages();

    }
}
