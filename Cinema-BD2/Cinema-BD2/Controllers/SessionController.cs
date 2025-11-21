using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinema_BD2.Controllers
{
    public class SessionController : Controller
    {
        private readonly ILanguageRepository _languageRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IFilmRepository _filmRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IDimensionRepository _dimensionRepository;
        public SessionController(ILanguageRepository languageRepository, ISessionRepository sessionRepository, IFilmRepository filmRepository, IRoomRepository roomRepository, IDimensionRepository dimensionRepository)
        {
            _languageRepository = languageRepository;
            _sessionRepository = sessionRepository;
            _filmRepository = filmRepository;
            _roomRepository = roomRepository;
            _dimensionRepository = dimensionRepository;
        }

        // Listar todas as salas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var session = await _sessionRepository.GetAll();
            return View(session);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.LanguageId = new SelectList(await _languageRepository.GetAll(), "Id", "Name");
            ViewBag.FilmId = new SelectList(await _filmRepository.GetAll(), "Id", "Tittle");
            ViewBag.RoomId = new SelectList(await _roomRepository.GetAll(), "Id", "Name");
            ViewBag.DimensionId = new SelectList(await _dimensionRepository.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Session session)
        {
            if (ModelState.IsValid)
            {
                await _sessionRepository.Create(session);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.LanguageId = new SelectList(await _languageRepository.GetAll(), "Id", "Name", session.LanguageId);
            ViewBag.FilmId = new SelectList(await _filmRepository.GetAll(), "Id", "Tittle", session.FilmId);
            ViewBag.RoomId = new SelectList(await _roomRepository.GetAll(), "Id", "Name", session.RoomId);
            ViewBag.DimensionId = new SelectList(await _dimensionRepository.GetAll(), "Id", "Name", session.DimensionId);
            return View(session);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var session = await _sessionRepository.GetById(id);
            if (session == null)
                return NotFound();

            await _sessionRepository.Delete(session);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var session = await _sessionRepository.GetById(id);
            if (session == null) return NotFound();

            var types = await _sessionRepository.GetAll();
            ViewBag.LanguageId = new SelectList(await _languageRepository.GetAll(), "Id", "Name", session.LanguageId);
            ViewBag.FilmId = new SelectList(await _filmRepository.GetAll(), "Id", "Tittle", session.FilmId);
            ViewBag.RoomId = new SelectList(await _roomRepository.GetAll(), "Id", "Name", session.RoomId);
            ViewBag.DimensionId = new SelectList(await _dimensionRepository.GetAll(), "Id", "Name", session.DimensionId);

            return View(session);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Session session)
        {
            if (id != session.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                ViewBag.LanguageId = new SelectList(await _languageRepository.GetAll(), "Id", "Name", session.LanguageId);
                ViewBag.FilmId = new SelectList(await _filmRepository.GetAll(), "Id", "Tittle", session.FilmId);
                ViewBag.RoomId = new SelectList(await _roomRepository.GetAll(), "Id", "Name", session.RoomId);
                ViewBag.DimensionId = new SelectList(await _dimensionRepository.GetAll(), "Id", "Name", session.DimensionId);

                return View(session);
            }
            await _sessionRepository.Update(session);
            return RedirectToAction(nameof(Index));
        }
    }
}
