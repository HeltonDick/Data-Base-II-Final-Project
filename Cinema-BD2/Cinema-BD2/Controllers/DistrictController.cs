using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Cinema_BD2.Controllers
{
    public class DistrictController : Controller
    {
        private readonly IDistrictRepository _districtRepository;
        public DistrictController(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        // List's all districts
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var districts = await _districtRepository.GetAll();
            return View(districts);
        }

        // Create a district (Shows the form for create)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create a new district and redirect to index
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Create(District district)
        {
            if (ModelState.IsValid)
            {
                await _districtRepository.Create(district);
                return RedirectToAction(nameof(Index));
            }
            return View(district);
        }

        // Edit a district (Shows the form for edit)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var district = await _districtRepository.GetById(id.Value);
            if (district == null)
                return NotFound();

            return View(district);
        }

        // Udates a disctrict validating if the id is valid
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Edit(int? id, District district)
        {
            if (!id.HasValue)
                return BadRequest();

            if (id.Value != district.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _districtRepository.Update(district);
                return RedirectToAction(nameof(Index));
            }

            return View(district);
        }

        // Delete a district
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Delete(int id)
        {
            var district = await _districtRepository.GetById(id);
            if (district == null)
                return NotFound();

            await _districtRepository.Delete(district);
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
                var districtsFiltered = await _districtRepository.GetByName(search);
                return View(districtsFiltered);
            }

            var districts = await _districtRepository.GetAll();
            return View(districts);
        }
    }
}
