
namespace TheDailyRoutine.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TheDailyRoutine.Services.Interfaces;

    [Authorize]
    public class ProgressController : Controller
    {
        private readonly IHabitProgressService _habitProgressService;

        public ProgressController(IHabitProgressService habitProgressService)
        {
            _habitProgressService = habitProgressService;
        }

        // POST: /Progress/Record
        [HttpPost]
        public async Task<IActionResult> Record(int userHabitId, DateTime date, bool isCompleted)
        {
            if (ModelState.IsValid)
            {
                await _habitProgressService.RecordProgressAsync(userHabitId, date, isCompleted);
                return Ok("Progress recorded successfully.");
            }

            return BadRequest("Failed to record progress.");
        }

        // GET: /Progress/UserProgress
        public async Task<IActionResult> UserProgress(DateTime startDate, DateTime endDate)
        {
            var userId = User.Identity.Name;
            var progress = await _habitProgressService.GetProgressForUserAsync(userId, startDate, endDate);

            return View(progress);
        }
    }
}
