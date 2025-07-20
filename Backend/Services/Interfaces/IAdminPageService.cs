using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAdminPageService
    {
        public Task DeletePageAsync(int id);
        public Task<Page> CreatePageAsync(Page page);
        public Task<Page> CreatePageAsync(Page page, int parentID);
        public Task<Page> UpdatePageAsync(Page page);
        public Task<Page> GetPageByIdAsync(int id);
        public Task<List<Page>> GetAllActivePagesAsync();
        public Task<List<Page>> GetAllPagesAsycn();
    }
}
