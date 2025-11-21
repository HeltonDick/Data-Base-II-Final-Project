using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Cinema_BD2.Controllers
{
    public class GenreController : Controller
    {
        private readonly IGenreRepository _genreRepository;
        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        // List's all genres
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var genres = await _genreRepository.GetAll();
            return View(genres);
        }

        // Create a genre (Shows the form for create)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create a new genre and redirect to index
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _genreRepository.Create(genre);
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // Edit a genre (Shows the form for edit)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var genre = await _genreRepository.GetById(id.Value);
            if (genre == null)
                return NotFound();

            return View(genre);
        }

        // Udates a genre validating if the id is valid
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Edit(int? id, Genre genre)
        {
            if (!id.HasValue)
                return BadRequest();

            if (id.Value != genre.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _genreRepository.Update(genre);
                return RedirectToAction(nameof(Index));
            }

            return View(genre);
        }

        // Delete a genre
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Delete(int id)
        {
            var genre = await _genreRepository.GetById(id);
            if (genre == null)
                return NotFound();

            await _genreRepository.Delete(genre);
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
                var genreFiltred = await _genreRepository.GetByName(search);
                return View(genreFiltred);
            }

            var genres = await _genreRepository.GetAll();
            return View(genres);
        }
    }
}
