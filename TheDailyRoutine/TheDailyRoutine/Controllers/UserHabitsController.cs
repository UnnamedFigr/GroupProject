using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TheDailyRoutine.Entities.Enums;
using TheDailyRoutine.Services.Interfaces;

namespace TheDailyRoutine.Controllers
{
    [Authorize]
    public class UserHabitsController : Controller
    {
        private readonly IUserHabitService _userHabitService;

        public UserHabitsController(IUserHabitService userHabitService)
        {
            _userHabitService = userHabitService;
        }

        // GET: /UserHabits
        public async Task<IActionResult> Index()
        {
            var userId = User.Identity.Name; 
            var userHabits = await _userHabitService.GetUserHabitsAsync(userId);
            return View(userHabits);
        }

        // POST: /UserHabits/Add
        [HttpPost]
        public async Task<IActionResult> Add(int habitId, FrequencyType frequency)
        {
            var userId = User.Identity.Name;

            if (ModelState.IsValid)
            {
                await _userHabitService.AddHabitToUserAsync(habitId, userId, frequency);
                return RedirectToAction(nameof(Index));
            }

            return BadRequest("Unable to add habit");
        }

        // POST: /UserHabits/Remove
        [HttpPost]
        public async Task<IActionResult> Remove(int userHabitId)
        {
            var userId = User.Identity.Name;

            await _userHabitService.RemoveUserHabitAsync(userHabitId, userId);
            return RedirectToAction(nameof(Index));
        }
    }
}
