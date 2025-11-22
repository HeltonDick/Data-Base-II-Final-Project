using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Cinema_BD2.Controllers
{
    public class LanguageController : Controller
    {
        private readonly ILanguageRepository _languageRepository;
        public LanguageController(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        // List's all languages
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var languages = await _languageRepository.GetAll();
            return View(languages);
        }

        // Create a language (Shows the form for create)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create a new language and redirect to index
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Create(Language language)
        {
            if (ModelState.IsValid)
            {
                await _languageRepository.Create(language);
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        // Edit a language (Shows the form for edit)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var language = await _languageRepository.GetById(id.Value);
            if (language == null)
                return NotFound();

            return View(language);
        }

        // Udates a language validating if the id is valid
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Edit(int? id, Language language)
        {
            if (!id.HasValue)
                return BadRequest();

            if (id.Value != language.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _languageRepository.Update(language);
                return RedirectToAction(nameof(Index));
            }

            return View(language);
        }

        // Delete a language
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Delete(int id)
        {
            var language = await _languageRepository.GetById(id);
            if (language == null)
                return NotFound();

            await _languageRepository.Delete(language);
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
                var lenguageFiltred = await _languageRepository.GetByName(search);
                return View(lenguageFiltred);
            }

            var languages = await _languageRepository.GetAll();
            return View(languages);
        }
    }
}
