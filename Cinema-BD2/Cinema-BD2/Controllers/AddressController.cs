using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Cinema_BD2.Controllers
{
    public class AddressController : Controller
    {
        //Dependency Injection (Injeção de dependÇencia) para ter acesso a leitura dos valors no banco de dados
        private readonly IAddressRepository _addressRepository;
        private readonly IStreetRepository _streetRepository;
        private readonly IDistrictRepository _districtRepository;
        public AddressController(IAddressRepository addressRepository, IStreetRepository streetRepository, IDistrictRepository districtRepository)
        {
            _addressRepository = addressRepository;
            _streetRepository = streetRepository;
            _districtRepository = districtRepository;
        }

        //[ValidateAntiForgeryToken] CSRF (Cross-Site Request Forgery) é um tipo de ataque
        //onde um site mal-intencionado tenta fazer o navegador da vítima enviar uma requisição
        //para outro site onde ela já está autenticada

        // IActionResult é uma interface que representa qualquer tipo de resposta que um Controller pode retornar

        // Listar todos os endereços
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var addresses = await _addressRepository.GetAll();
            return View(addresses);
        }

        // Exibe o formulário de criação Juntamente de distrito e rua, carregados em uma viewBag (carreega valores temporarios para a view)
        // Select List para drop down
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
            // Model State checa se todas as validações ([Required], [StringLength], etc) foram atendidas
            if (ModelState.IsValid)
            {
                await _addressRepository.Create(address);
                return RedirectToAction(nameof(Index));
                // Se estiver valido, salva no banco e envia para a view
            }
            // Quando a view é reexibida após a validação falhar, os viewBag do get não existem mais, e ae os select ficaram vazios
            // Por isso carregamos denovo

            ViewBag.DistrictId = new SelectList(await _districtRepository.GetAll(), "Id", "Name", address.DistrictId);
            ViewBag.StreetId = new SelectList(await _streetRepository.GetAll(), "Id", "Name", address.StreetId);

            return View(address);
        }

        // Editar endereço
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)// Recebomos um Id
        {
            if (id == null) return BadRequest(); // Validamos o id

            var address = await _addressRepository.GetById(id.Value);
            if (address == null) return NotFound(); //validamos o endereço em si de acordo comn o id

            var districts = await _districtRepository.GetAll(); // Busca dados no banco
            ViewBag.DistrictId = new SelectList(districts, "Id", "Name", address.DistrictId); // Carrega dados no drop down e atrela eles ao endreço

            var streets = await _streetRepository.GetAll();
            ViewBag.StreetId = new SelectList(streets, "Id", "Name", address.StreetId);

            return View(address); // Envia esses valores para a view
        }

        // Atualizar endereço
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, Address address) // Receber id da e o objeto endereço (com os atributos)
        {
            if (id != address.Id) return BadRequest(); // Valida se o id recebido é diferento do id do endereço 

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
        public async Task<IActionResult> Delete(int id) // Receb Id
        {
            var address = await _addressRepository.GetById(id); // Busca no banco qual endereço tem tau Id
            if (address == null) return NotFound(); // Se o endereço encontrado for lullo, não foi encontrado

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
