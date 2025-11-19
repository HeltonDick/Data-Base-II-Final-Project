using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Cinema_BD2.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ITypeOfRoomRepository _typeOfRoomRepository;
        public RoomController(IRoomRepository roomRepository, ITypeOfRoomRepository typeOfRoomRepository)
        {
            _roomRepository = roomRepository;
            _typeOfRoomRepository = typeOfRoomRepository;
        }

        // Listar todas as salas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var rooms = await _roomRepository.GetAll();
            return View(rooms);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.TypeOfRoomId = new SelectList(await _typeOfRoomRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            if (ModelState.IsValid)
            {
                await _roomRepository.Create(room);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TypeOfRoomId = new SelectList(await _typeOfRoomRepository.GetAll(), "Id", "Name", room.TypeOfRoomId);
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _roomRepository.GetById(id);
            if (room == null)
                return NotFound();

            await _roomRepository.Delete(room);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var room = await _roomRepository.GetById(id);
            if (room == null) return NotFound();

            var types = await _typeOfRoomRepository.GetAll();
            ViewBag.TypeOfRoomId = new SelectList(types, "Id", "Name", room.TypeOfRoomId);

            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Room room)
        {
            if (id != room.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                var typesForInvalid = await _typeOfRoomRepository.GetAll();
                ViewBag.TypeOfRoomId = new SelectList(typesForInvalid, "Id", "Name", room.TypeOfRoomId);
                return View(room);
            }

            try
            {
                await _roomRepository.Update(room);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao atualizar a sala. Tente novamente.");
                var typesOnError = await _typeOfRoomRepository.GetAll();

                ViewBag.TypeOfRoomId = new SelectList(typesOnError, "Id", "Name", room.TypeOfRoomId);
                return View(room);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
