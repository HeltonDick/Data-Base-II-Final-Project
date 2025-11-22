using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cinema_BD2.Controllers
{
    public class DimensionController : Controller
    {
        private readonly IDimensionRepository _dimensionRepository;
        public DimensionController(IDimensionRepository dimensionRepoistory)
        {
            _dimensionRepository = dimensionRepoistory;
        }

        // List's all dimensions
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var dimensions = await _dimensionRepository.GetAll();
            return View(dimensions);
        }

        // Create a dimension (Shows the form for create)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create a new dimension and redirect to index
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Create(Dimension dimension)
        {
            if (ModelState.IsValid)
            {
                await _dimensionRepository.Create(dimension);
                return RedirectToAction(nameof(Index));
            }
            return View(dimension);
        }

        // Edit a dimension (Shows the form for edit)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var dimension = await _dimensionRepository.GetById(id.Value);
            if (dimension == null)
                return NotFound();

            return View(dimension);
        }

        // Udates a dimension validating if the id is valid
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Edit(int? id, Dimension dimension)
        {
            if (!id.HasValue)
                return BadRequest();

            if (id.Value != dimension.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _dimensionRepository.Update(dimension);
                return RedirectToAction(nameof(Index));
            }

            return View(dimension);
        }

        // Delete a dimension
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Delete(int id)
        {
            var dimension = await _dimensionRepository.GetById(id);
            if (dimension == null)
                return NotFound();

            await _dimensionRepository.Delete(dimension);
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
        public async Task<IActionResult> Search(string? search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var dimensionFiltered = await _dimensionRepository.GetByName(search);
                return View(dimensionFiltered);
            }

            var dimensions = await _dimensionRepository.GetAll();
            return View(dimensions);
        }
    }
}
