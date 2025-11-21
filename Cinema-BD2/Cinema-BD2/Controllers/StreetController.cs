using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cinema_BD2.Controllers
{
    public class StreetController : Controller
    {
        private readonly IStreetRepository _streetRepository;
        public StreetController(IStreetRepository streetRepository)
        {
            _streetRepository = streetRepository;
        }

        // List's all Streets
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var streets = await _streetRepository.GetAll();
            return View(streets);
        }

        // Create a Street (Shows the form for create)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create a new Street and redirect to index
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Create(Street street)
        {
            if (ModelState.IsValid)
            {
                await _streetRepository.Create(street);
                return RedirectToAction(nameof(Index));
            }
            return View(street);
        }

        // Edit a Street (Shows the form for edit)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var street = await _streetRepository.GetById(id.Value);
            if (street == null)
                return NotFound();

            return View(street);
        }

        // Udates a Street validating if the id is valid
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Edit(int? id, Street street)
        {
            if (!id.HasValue)
                return BadRequest();

            if (id.Value != street.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _streetRepository.Update(street);
                return RedirectToAction(nameof(Index));
            }

            return View(street);
        }

        // Delete a Street
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Delete(int id)
        {
            var street = await _streetRepository.GetById(id);
            if (street == null)
                return NotFound();

            await _streetRepository.Delete(street);
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

        // Search Bar
        [HttpGet]
        public async Task<IActionResult> Index(string? search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var streetFiltred = await _streetRepository.GetByName(search);
                return View(streetFiltred);
            }

            var streets = await _streetRepository.GetAll();
            return View(streets);
        }
    }
}
