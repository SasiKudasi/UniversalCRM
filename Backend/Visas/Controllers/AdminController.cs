using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Core.Models;
using Microsoft.Extensions.Options;
using Services.Interfaces;
using Visas.Contracts;
using Visas.Contracts.Mapper;

namespace Visas.Controllers
{
    [ApiController]
    [Route("admin")]
    [Authorize]
    public class AdminController
        : ControllerBase
    {
        private readonly User _admin;
        private readonly IAdminPageService _adminPageService;

        public AdminController(IOptions<User> options, IAdminPageService adminPageService)
        {
            _admin = options.Value;
            _adminPageService = adminPageService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromForm] string userName, [FromForm] string pwd)
        {

            if (_admin.Username != userName)
            {
                return Unauthorized("Invalid username or password");
            }

            var hasher = new PasswordHasher<string>();
            var result = hasher.VerifyHashedPassword(null, _admin.PasswordHash, pwd); // порядок: (user, HASH, input)
            if (result == PasswordVerificationResult.Failed)
            {
                return Unauthorized("Invalid username or password");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Ok("Success");
        }


        [HttpDelete("{id}")]

        public async Task<ActionResult<string>> DeletePage(int id)
        {
            await _adminPageService.DeletePageAsync(id);
            return Ok("Page deleted successfully");
        }

        [HttpPost]
        public async Task<ActionResult<Page>> CreatePage([FromForm] PageRequestDTO page)
        {
            if (page == null)
            {
                return BadRequest("Page data is required");
            }

            var createdPage = await _adminPageService.CreatePageAsync(PageMapper.ToDomain(page));
            return Ok(createdPage);
        }

        [HttpPost("with_parent")]
        public async Task<ActionResult<Page>> CreatePageWithParent([FromForm] PageRequestDTO page, int parentID)
        {
            if (page == null)
            {
                return BadRequest("Page data is required");
            }

            var createdPage = await _adminPageService.CreatePageAsync(PageMapper.ToDomain(page), parentID);
            return Ok(createdPage);
        }

        [HttpGet("all_active_page")]
        public async Task<ActionResult<IEnumerable<PageResponseDTO>>> GetAllActivePages()
        {
            var pages = await _adminPageService.GetAllActivePagesAsync();
            var pagesResponse = pages.Select(p => p.ToResponse());

            return Ok(pagesResponse);
        }

        [HttpGet("all_page")]
        public async Task<ActionResult<IEnumerable<Page>>> GetAllPages()
        {
            var pages = await _adminPageService.GetAllPagesAsycn();
            return Ok(pages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Page>> GetPageById(int id)
        {
            var page = await _adminPageService.GetPageByIdAsync(id);
            if (page == null)
            {
                return NotFound("Page not found");
            }
            return Ok(page);
        }

        [HttpPut]
        public async Task<ActionResult<Page>> UpdatePage([FromBody] Page page)
        {
            if (page == null || page.Id <= 0)
            {
                return BadRequest("Valid page data is required for update");
            }

            var updatedPage = await _adminPageService.UpdatePageAsync(page);
            return Ok(updatedPage);
        }
    }
}
