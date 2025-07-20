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


        public async Task<PageEntity?> GetPageBySlugAsync(string slug)
        {
            return await _context.Pages
                .Include(p => p.Children)
                .FirstOrDefaultAsync(p => p.Slug == slug && p.IsActive);
        }

        public async Task<PageEntity?> GetPageByIdAsync(int id)
        {
            return await _context.Pages
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

        public async Task<PageEntity> UpdatePageAsync(PageEntity page)
        {
            _context.Pages.Update(page);
            await _context.SaveChangesAsync();
            return page;
        }


        public async Task DeletePageAsync(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page != null)
            {
                _context.Pages.Remove(page);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<PageEntity>> GetChildPagesAsync(int parentId)
        {
            return await _context.Pages
                .Where(p => p.ParentId == parentId && p.IsActive)
                .Include(p => p.Children)
                .ToListAsync();
        }

        public async Task<PageEntity?> GetPageBySlugPathAsync(string[] slugPath)
        {
            PageEntity? current = null;

            foreach (var slug in slugPath)
            {
                current = await _context.Pages
                    .Include(p => p.Children)
                    .FirstOrDefaultAsync(p =>
                        p.Slug == slug &&
                        p.ParentId == (current == null ? null : current.Id) &&
                        p.IsActive);

                if (current == null)
                    return null;
            }

            return current;
        }

    }


}
