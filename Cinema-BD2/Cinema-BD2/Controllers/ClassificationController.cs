using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Cinema_BD2.Controllers
{
    public class ClassificationController : Controller
    {
        private readonly IClassificationRepository _classificationRepository;
        public ClassificationController(IClassificationRepository classificationRepository)
        {
            _classificationRepository = classificationRepository;
        }

        // List's all classifications
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var classifications = await _classificationRepository.GetAll();
            return View(classifications);
        }

        // Create a classification (Shows the form for create)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create a new classification and redirect to index
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Create(Classification classification)
        {
            if (ModelState.IsValid)
            {
                await _classificationRepository.Create(classification);
                return RedirectToAction(nameof(Index));
            }
            return View(classification);
        }

        // Edit a classification (Shows the form for edit)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var classification = await _classificationRepository.GetById(id.Value);
            if (classification == null)
                return NotFound();

            return View(classification);
        }

        // Udates a classification validating if the id is valid
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Edit(int? id, Classification classification)
        {
            if (!id.HasValue)
                return BadRequest();

            if (id.Value != classification.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _classificationRepository.Update(classification);
                return RedirectToAction(nameof(Index));
            }

            return View(classification);
        }

        // Delete a classification
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Delete(int id)
        {
            var classification = await _classificationRepository.GetById(id);
            if (classification == null)
                return NotFound();

            await _classificationRepository.Delete(classification);
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
