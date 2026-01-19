using Microsoft.AspNetCore.Mvc;
using SR.HomeServices.Application.Interfaces;
using SR.HomeServices.Domain;
using System.Security.Claims;

namespace SR_HomeServices_Captain.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IProfileService _service;

        public ProfileController(IProfileService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var model = await _service.GetProfileAsync(userId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Index", model);

            await _service.UpsertProfileAsync(model, "System");
            TempData["Success"] = "Profile updated successfully";
            return RedirectToAction("Index");
        }
    }
}
