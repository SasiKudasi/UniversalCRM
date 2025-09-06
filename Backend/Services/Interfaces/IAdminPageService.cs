using Core.Models;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IAdminPageService
    {
        Task<List<Page>> GetAllActivePagesAsync();
        Task<List<Page>> GetAllPagesAsycn();
        Task<Result<Page>> GetPageByIdAsync(Guid id);
        Task<Result> UpdatePageAsync(Page page);
        Task<Result<Page>> CreatePageAsync(Page page);
        Task<Result<Page>> CreatePageAsync(Page page, Guid parentId);
        Task DeletePageAsync(Guid id);
        Task<Result<Page>> GetPageByPathAsync(string path);
    }
}
