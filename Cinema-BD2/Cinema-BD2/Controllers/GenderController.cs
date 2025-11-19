using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Cinema_BD2.Controllers
{
    public class GenderController : Controller
    {
        private readonly IGenderRepository _genderRepository;
        public GenderController(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        // List's all Genders
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var genders = await _genderRepository.GetAll();
            return View(genders);
        }

        // Create a Genders (Shows the form for create)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create a new Gender and redirect to index
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Create(Gender gender)
        {
            if (ModelState.IsValid)
            {
                await _genderRepository.Create(gender);
                return RedirectToAction(nameof(Index));
            }
            return View(gender);
        }

        // Edit a district (Shows the form for edit)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var gender = await _genderRepository.GetById(id.Value);
            if (gender == null)
                return NotFound();

            return View(gender);
        }

        // Udates a Gender validating if the id is valid
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Edit(int? id, Gender gender)
        {
            if (!id.HasValue)
                return BadRequest();

            if (id.Value != gender.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _genderRepository.Update(gender);
                return RedirectToAction(nameof(Index));
            }

            return View(gender);
        }

        // Delete a gender
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Delete(int id)
        {
            var gender = await _genderRepository.GetById(id);
            if (gender == null)
                return NotFound();

            await _genderRepository.Delete(gender);
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
