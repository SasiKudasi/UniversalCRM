using Core.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Visas.Contracts.Mapper;

namespace Visas.Controllers
{
    [ApiController]
    [Route("api/")]
    public class UserController : ControllerBase
    {
        private readonly IAdminPageService _adminPageService;

        public UserController(IAdminPageService adminPageService)
        {
            _adminPageService = adminPageService;
        }

        [HttpGet("page_by_path")]
        public async Task<IActionResult> GetPageByPath(string path)
        {
            var pagesResult = await _adminPageService.GetPageByPathAsync(path);
            if (pagesResult.IsFailure)
            {
                return BadRequest(pagesResult.Error);
            }
            var page = pagesResult.Value;
            return Ok(page.ToResponseWhithChildren());
        }

    }
}
