using Microsoft.AspNetCore.Mvc;
using SR.HomeServices.Application.Interfaces;
using System.Security.Claims;

namespace SR_HomeServices_Captain.Controllers
{
    [Route("Bookings")]
    public class BookingsController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            int captainId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var model = await _bookingService.GetDashboardAsync(captainId);
            return View(model);
        }

        [HttpGet("pendingRequests")]
        public async Task<IActionResult> GetPendingRequests()
        {
            int captainId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var data = await _bookingService
                .GetDashboardAsync(captainId);
            return PartialView("_GetPendingRequests.cshtml", data.Pending);
        }

        [HttpGet("inProgressRequests")]
        public async Task<IActionResult> GetInProgressRequests()
        {
            int captainId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var data = await _bookingService
                .GetDashboardAsync(captainId);
            return PartialView("GetInProgressRequests.cshtml", data.InProgress);
        }

        [HttpGet("completedRequests")]
        public async Task<IActionResult> GetCompletedRequests()
        {
            int captainId = 1;
            var data = await _bookingService
                .GetDashboardAsync(captainId);
            return PartialView("GetCompletedRequests.cshtml", data.Completed);
        }

        // GET: BookingsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookingsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookingsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookingsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
