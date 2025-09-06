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
    }
}
