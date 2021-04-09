using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ReversiMvcApp.Controllers
{
    [Route("[controller]")]
    public class ManagementController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly PlayerController _playerController;

        public ManagementController(UserManager<IdentityUser> userManager, PlayerController playerController)
        {
            _userManager = userManager;
            _playerController = playerController;
        }

        [Authorize(Policy = "RolePage")]
        public async Task<ActionResult> Index()
        {
            return View(await _playerController.GetPlayers());

        }

        [Authorize(Policy = "Edit")]
        [HttpGet("edit/{guid}")]
        public async Task<IActionResult> Edit(string guid)
        {
            var user = _playerController.GetPlayer(guid);

            return View(user);
        }

        [Authorize(Policy = "Edit")]
        [HttpPost("edit/{guid}")]
        public async Task<IActionResult> Edit(string guid, string name, string email)
        {
            var user = _playerController.GetPlayer(guid);

            user.Email = email;
            user.Name = name;

            await _playerController.SavePlayer(user);

            return RedirectToAction("Index");
        }

        [Authorize(Policy = "Ban")]
        [HttpGet("view/{guid}")]
        public async Task<IActionResult> View(string guid)
        {
            var user = _playerController.GetPlayer(guid);

            return View(user);
        }

        [Authorize(Policy = "Ban")]
        [HttpGet("ban/{guid}")]
        public async Task<IActionResult> Ban(string guid)
        {
            await _playerController.DeletePlayer(guid);
            return RedirectToAction("Index");
        }

        [Authorize(Policy = "Edit")]
        [HttpGet("promote/{guid}")]
        public async Task<IActionResult> Promote(string guid)
        {
            var user = await _userManager.FindByIdAsync(guid);

            await _userManager.AddToRoleAsync(user, "Mediator");

            return RedirectToAction("Index");
        }

        [Authorize(Policy = "Edit")]
        [HttpGet("demote/{guid}")]
        public async Task<IActionResult> Demote(string guid)
        {
            var user = await _userManager.FindByIdAsync(guid);

            await _userManager.RemoveFromRoleAsync(user, "Mediator");

            return RedirectToAction("Index");
        }
    }
}
