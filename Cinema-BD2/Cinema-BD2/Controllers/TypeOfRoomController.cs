using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Cinema_BD2.Controllers
{
    public class TypeOfRoomController : Controller
    {
        private readonly ITypeOfRoomRepository _typeOfRoomRepository;
        public TypeOfRoomController(ITypeOfRoomRepository typeOfRoomRepository)
        {
            _typeOfRoomRepository = typeOfRoomRepository;
        }

        // List's all type Of Rooms
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var typeOfRooms = await _typeOfRoomRepository.GetAll();
            return View(typeOfRooms);
        }

        // Create a typeOfRoom (Shows the form for create)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create a new typeOfRoom and redirect to index
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Create(TypeOfRoom typeOfRoom)
        {
            if (ModelState.IsValid)
            {
                await _typeOfRoomRepository.Create(typeOfRoom);
                return RedirectToAction(nameof(Index));
            }
            return View(typeOfRoom);
        }

        // Edit a typeOfRoom (Shows the form for edit)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var typeOfRoom = await _typeOfRoomRepository.GetById(id.Value);
            if (typeOfRoom == null)
                return NotFound();

            return View(typeOfRoom);
        }

        // Udates a typeOfRoom validating if the id is valid
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Edit(int? id, TypeOfRoom typeOfRoom)
        {
            if (!id.HasValue)
                return BadRequest();

            if (id.Value != typeOfRoom.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _typeOfRoomRepository.Update(typeOfRoom);
                return RedirectToAction(nameof(Index));
            }

            return View(typeOfRoom);
        }

        // Delete a typeOfRoom
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Delete(int id)
        {
            var typeOfRoom = await _typeOfRoomRepository.GetById(id);
            if (typeOfRoom == null)
                return NotFound();

            await _typeOfRoomRepository.Delete(typeOfRoom);
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

        [HttpGet]
        public async Task<IActionResult> Search(string? search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var typeOfRoomFiltred = await _typeOfRoomRepository.GetByName(search);
                return View(typeOfRoomFiltred);
            }

            var typesOfRoom = await _typeOfRoomRepository.GetAll();
            return View(typesOfRoom);
        }
    }
}
