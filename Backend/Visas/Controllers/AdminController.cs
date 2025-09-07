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
    [Route("api/admin")]
    [Authorize]
    public class AdminController
        : ControllerBase
    {
        private readonly User _admin;
        private readonly IAdminPageService _adminPageService;
        private readonly ILogger<AdminController> _logger;
        public AdminController(IOptions<User> options, IAdminPageService adminPageService, ILogger<AdminController> logger)
        {
            _admin = options.Value;
            _adminPageService = adminPageService;
            _logger = logger;
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
                _logger.LogError($"{DateTime.Now} : {HttpContext.Connection.RemoteIpAddress} : Неудачная попытка авторизации");
                return Unauthorized("Invalid username or password");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            _logger.LogInformation($"{DateTime.Now} : {HttpContext.Connection.RemoteIpAddress}: Авторизация пользователя  : {User.Identity.Name}");
            return Ok("Success");
        }


        [HttpDelete("{id:guid}")]

        public async Task<ActionResult<string>> DeletePage(Guid id)
        {
            await _adminPageService.DeletePageAsync(id);
            _logger.LogInformation($"Удалена страница: {id}");
            return Ok("Page deleted successfully");
        }

        [HttpPost("root")]
        public async Task<IActionResult> CreatePage([FromForm] PageRequestDTO page)
        {
            if (page == null)
            {
                return BadRequest("Page data is required");
            }

            var pageToDomain = PageMapper.ToDomain(page, true);
            if (pageToDomain.IsFailure)
            {
                return BadRequest(pageToDomain.Error);
            }

            var createdPage = await _adminPageService.CreatePageAsync(pageToDomain.Value);

            if (createdPage.IsFailure)
            {
                return BadRequest(createdPage.Error);
            }

            _logger.LogInformation($"{DateTime.Now} : Создана страница {createdPage.Value.Title} | {createdPage.Value.Id}");
            return Created($"{createdPage.Value.Path}", createdPage.Value.Id);
        }

        [HttpPost("with_parent")]
        public async Task<IActionResult> CreatePageWithParent([FromForm] PageRequestDTO page, Guid parentID)
        {
            if (page == null)
            {
                return BadRequest("Page data is required");
            }
            var pageToDomain = PageMapper.ToDomain(page, false);
            if (pageToDomain.IsFailure)
            {
                return BadRequest(pageToDomain.Error);
            }
            var createdPage = await _adminPageService.CreatePageAsync(pageToDomain.Value, parentID);
            if (createdPage.IsFailure)
            {
                return BadRequest(createdPage.Error);
            }
            var createdPageResult = createdPage.Value;
            _logger.LogInformation($"{DateTime.Now} : Создана страница {createdPageResult.Title} | {createdPageResult.Id}");
            return Created($"{createdPageResult.Path}", createdPageResult.Id);
        }

        [HttpGet("all_active_page")]
        public async Task<IActionResult> GetAllActivePages()
        {
            var pages = await _adminPageService.GetAllActivePagesAsync();
            var pagesResponse = pages.Select(p => p.ToResponseWhithChildren());
            return Ok(pagesResponse);
        }

        [HttpGet("all_page")]
        public async Task<IActionResult> GetAllPages()
        {
            var pages = await _adminPageService.GetAllPagesAsycn();
            var pagesResponse = pages.Select(p => p.ToResponse());

            return Ok(pagesResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPageById(Guid id)
        {
            var page = await _adminPageService.GetPageByIdAsync(id);
            if (page.IsFailure)
            {
                return BadRequest(page.Error);
            }
            return Ok(page.Value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePage([FromBody] Page page)
        {
            if (page == null)
            {
                return BadRequest("Valid page data is required for update");
            }

            var updatedPage = await _adminPageService.UpdatePageAsync(page);
            if (updatedPage.IsFailure)
            {
                return BadRequest(updatedPage.Error);
            }

            _logger.LogInformation($"{DateTime.Now} : Обновлена страница {page.Title} | {page.Id}");
            return Ok();
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


        [HttpGet("me")]
        public ActionResult<string> GetMe()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
                return Unauthorized();

            return Ok(User.Identity.Name);
        }

    }
}
