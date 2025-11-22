using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Cinema_BD2.Controllers
{
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;
        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // List's all roles
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleRepository.GetAll();
            return View(roles);
        }

        // Create a role (Shows the form for create)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create a new role and redirect to index
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Create(Role role)
        {
            if (ModelState.IsValid)
            {
                await _roleRepository.Create(role);
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // Edit a role (Shows the form for edit)
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var role = await _roleRepository.GetById(id.Value);
            if (role == null)
                return NotFound();

            return View(role);
        }

        // Udates a role validating if the id is valid
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Edit(int? id, Role role)
        {
            if (!id.HasValue)
                return BadRequest();

            if (id.Value != role.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _roleRepository.Update(role);
                return RedirectToAction(nameof(Index));
            }

            return View(role);
        }

        // Delete a role
        [HttpPost]
        [ValidateAntiForgeryToken] // Protects against CSRF attacks
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _roleRepository.GetById(id);
            if (role == null)
                return NotFound();

            await _roleRepository.Delete(role);
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
                var roleFiltred = await _roleRepository.GetByName(search);
                return View(roleFiltred);
            }

            var roles = await _roleRepository.GetAll();
            return View(roles);
        }
    }
}
