using Core.Models;
using DAL.Interfaces;
using Services.Interfaces;
using Services.Mappers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserPageService : IUserPageService

    {
        private readonly IPageRepository _pageRepository;

        public UserPageService(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }


        public async Task<Page> GetPageBySlagAsync(string slug)
        {
            var slugPath = slug.Split('/', StringSplitOptions.RemoveEmptyEntries);

            if (slugPath.Length < 1)
            {
                var homePage = await _pageRepository.GetPageBySlugAsync("");
                return homePage.ToDomain();
            }

            var pageEntity = await _pageRepository.GetPageBySlugPathAsync(slugPath);
            if (pageEntity == null)
            {
                throw new FileNotFoundException($"Page with slug '{slug}' not found.");
            }

            return pageEntity.ToDomain();
        }
    }
}
