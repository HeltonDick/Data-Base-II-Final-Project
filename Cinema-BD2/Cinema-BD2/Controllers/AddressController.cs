using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Cinema_BD2.Controllers
{
    public class AddressController : Controller
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IStreetRepository _streetRepository;
        private readonly IDistrictRepository _districtRepository;
        public AddressController(IAddressRepository addressRepository, IStreetRepository streetRepository, IDistrictRepository districtRepository)
        {
            _addressRepository = addressRepository;
            _streetRepository = streetRepository;
            _districtRepository = districtRepository;
        }

        // Listar todos os endereços
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var addresses = await _addressRepository.GetAll();
            return View(addresses);
        }

        // Exibe o formulário de criação
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.DistrictId = new SelectList(await _districtRepository.GetAll(), "Id", "Name");
            ViewBag.StreetId = new SelectList(await _streetRepository.GetAll(), "Id", "Name");
            return View();
        }

        // Cria um novo endereço
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Address address)
        {
            if (ModelState.IsValid)
            {
                await _addressRepository.Create(address);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.DistrictId = new SelectList(await _districtRepository.GetAll(), "Id", "Name", address.DistrictId);
            ViewBag.StreetId = new SelectList(await _streetRepository.GetAll(), "Id", "Name", address.StreetId);

            return View(address);
        }

        // Editar endereço
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            var address = await _addressRepository.GetById(id.Value);
            if (address == null) return NotFound();

            var districts = await _districtRepository.GetAll();
            ViewBag.DistrictId = new SelectList(districts, "Id", "Name", address.DistrictId);

            var streets = await _streetRepository.GetAll();
            ViewBag.StreetId = new SelectList(streets, "Id", "Name", address.StreetId);

            return View(address);
        }

        // Atualizar endereço
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Address address)
        {
            if (id != address.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                await _addressRepository.Update(address);
                return RedirectToAction(nameof(Index));
            }
            var districts = await _districtRepository.GetAll();
            ViewBag.DistrictId = new SelectList(districts, "Id", "Name", address.DistrictId);

            var streets = await _streetRepository.GetAll();
            ViewBag.StreetId = new SelectList(streets, "Id", "Name", address.StreetId);

            return View(address);
        }

        // Deletar direto no Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var address = await _addressRepository.GetById(id);
            if (address == null) return NotFound();

            await _addressRepository.Delete(address);
            return RedirectToAction(nameof(Index));
        }

        // Métodos padrão MVC
        [HttpGet]
        public IActionResult Privacy() => View();

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
