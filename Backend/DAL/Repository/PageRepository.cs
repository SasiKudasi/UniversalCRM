using DAL.Entity;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class PageRepository : IPageRepository
    {
        private readonly AppDbContext _context;
        public PageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PageEntity?> GetPageByPathAsync(string path)
        {
            return await _context.Pages
                .AsNoTracking()
                .Include(p => p.Children)
                .FirstOrDefaultAsync(p => p.Path == path && p.IsActive);
        }

        public async Task<PageEntity?> GetPageByIdAsync(Guid id)
        {
            return await _context.Pages
                .AsNoTracking()
                .Include(p => p.Children)
                .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);
        }


        public async Task<List<PageEntity>> GetAllActivePagesAsync()
        {
            return await _context.Pages
                .Where(p => p.IsActive)
                .Include(p => p.Children)
                .ToListAsync();
        }

        public async Task<List<PageEntity>> GetAllPages()
        {
            return await _context.Pages
                .Include(p => p.Children)
                .ToListAsync();
        }

        public async Task<PageEntity> CreatePageAsync(PageEntity page)
        {
            _context.Pages.Add(page);
            await _context.SaveChangesAsync();
            return page;
        }

        public async Task UpdatePageAsync(PageEntity page)
        {
            _context.Pages.Update(page);
            await _context.SaveChangesAsync();
        }


        public async Task DeletePageAsync(Guid id)
        {
            var page = await _context.Pages.FirstOrDefaultAsync(p => p.Id == id);
            if (page != null)
            {
                _context.Pages.Remove(page);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<PageEntity>> GetChildPagesAsync(Guid parentId)
        {
            return await _context.Pages
                .Where(p => p.ParentId == parentId && p.IsActive)
                .Include(p => p.Children)
                .ToListAsync();
        }
    }
}
