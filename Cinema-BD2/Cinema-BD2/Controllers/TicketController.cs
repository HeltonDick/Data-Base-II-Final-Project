using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Sockets;

namespace Cinema_BD2.Controllers
{
    public class TicketController : Controller
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IPerosonRepository _perosonRepository;
        private readonly ITicketRepository _ticketRepository;
        public TicketController(ISessionRepository sessionRepository, IPerosonRepository perosonRepository, ITicketRepository ticketRepository)
        {
            _sessionRepository = sessionRepository;
            _perosonRepository = perosonRepository;
            _ticketRepository = ticketRepository;
        }

        // Listar todas as salas
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketRepository.GetAll();
            return View(tickets);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.PersonId = new SelectList(await _perosonRepository.GetAll(), "Id", "Name");
            ViewBag.SessionId = new SelectList(await _sessionRepository.GetAll(), "Id", "SessionDate");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                await _ticketRepository.Create(ticket);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.PersonId = new SelectList(await _perosonRepository.GetAll(), "Id", "Name", ticket.PersonId);
            ViewBag.SessionId = new SelectList(await _sessionRepository.GetAll(), "Id", "SessionDate", ticket.SessionId);
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketRepository.GetById(id);
            if (ticket == null)
                return NotFound();

            await _ticketRepository.Delete(ticket);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketRepository.GetById(id);
            if (ticket == null) return NotFound();

            ViewBag.PersonId = new SelectList(await _perosonRepository.GetAll(), "Id", "Name", ticket.PersonId);
            ViewBag.SessionId = new SelectList(await _sessionRepository.GetAll(), "Id", "SessionDate", ticket.SessionId);

            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                var typesForInvalid = await _ticketRepository.GetAll();
                ViewBag.PersonId = new SelectList(await _perosonRepository.GetAll(), "Id", "Name", ticket.PersonId);
                ViewBag.SessionId = new SelectList(await _sessionRepository.GetAll(), "Id", "SessionDate", ticket.SessionId);
                return View(ticket);
            }

            try
            {
                await _ticketRepository.Update(ticket);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ocorreu um erro ao atualizar o ingresso. Tente novamente.");
                var typesOnError = await _ticketRepository.GetAll();

                ViewBag.PersonId = new SelectList(typesOnError, "Id", "Name", ticket.PersonId);
                return View(ticket);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
