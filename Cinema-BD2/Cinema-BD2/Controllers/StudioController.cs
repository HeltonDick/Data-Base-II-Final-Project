using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Cinema_BD2.Controllers
{
    public class StudioController : Controller
    {
        private readonly IStudioRepository _studioRepository;
        public StudioController(IStudioRepository studioRepository)
        {
            _studioRepository = studioRepository;
        }

        // List's all studios
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var studios = await _studioRepository.GetAll();
            return View(studios);
        }

        // Create a studio (Shows the form for create)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create a new studio and redirect to index
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Create(Studio studio)
        {
            if (ModelState.IsValid)
            {
                await _studioRepository.Create(studio);
                return RedirectToAction(nameof(Index));
            }
            return View(studio);
        }

        // Edit a studio (Shows the form for edit)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var studio = await _studioRepository.GetById(id.Value);
            if (studio == null)
                return NotFound();

            return View(studio);
        }

        // Udates a studio validating if the id is valid
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Edit(int? id, Studio studio)
        {
            if (!id.HasValue)
                return BadRequest();

            if (id.Value != studio.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _studioRepository.Update(studio);
                return RedirectToAction(nameof(Index));
            }

            return View(studio);
        }

        // Delete a studio
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Delete(int id)
        {
            var studio = await _studioRepository.GetById(id);
            if (studio == null)
                return NotFound();

            await _studioRepository.Delete(studio);
            return RedirectToAction(nameof(Index));
        }

        // Default MVC methods for privacy and error handling
        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
