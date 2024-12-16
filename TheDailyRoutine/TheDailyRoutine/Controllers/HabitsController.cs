using Microsoft.AspNetCore.Mvc;
using TheDailyRoutine.Entities;
using TheDailyRoutine.Services.Interfaces;

namespace TheDailyRoutine.Controllers
{
    public class HabitsController : Controller
    {
        private readonly IHabitService _habitService;

        public HabitsController(IHabitService habitService)
        {
            _habitService = habitService;
        }

        // GET: /Habits
        public async Task<IActionResult> Index()
        {
            var habits = await _habitService.GetAllPredefinedHabitsAsync();
            return View(habits);
        }

        // GET: /Habits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Habits/Create
        [HttpPost]
        public async Task<IActionResult> Create(Habit habit)
        {
            if (ModelState.IsValid)
            {
                await _habitService.CreateCustomHabitAsync(habit, User.Identity.Name);
                return RedirectToAction(nameof(Index));
            }
            return View(habit);
        }
    }
}
