using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Cinema_BD2.Models.PersonFormViewModel;

namespace Cinema_BD2.Controllers
{
    public class PersonController : Controller
    {
        private readonly IPerosonRepository _personRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IAddressRepository _addressRepository;

        public PersonController(IPerosonRepository personRepository,
                                IRoleRepository roleRepository,
                                IGenderRepository genderRepository,
                                IAddressRepository addressRepository)
        {
            _personRepository = personRepository;
            _roleRepository = roleRepository;
            _genderRepository = genderRepository;
            _addressRepository = addressRepository;
        }

        // GET LIST
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var people = await _personRepository.GetAll();
            return View(people);
        }

        // GET CREATE FORM
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new PersonFormViewModel
            {
                Roles = await _roleRepository.GetAll(),
                Genders = await _genderRepository.GetAll(),
                Addresses = await _addressRepository.GetAll()
            };

            return View(viewModel);
        }

        // POST CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PersonFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Roles = await _roleRepository.GetAll();
                viewModel.Genders = await _genderRepository.GetAll();
                viewModel.Addresses = await _addressRepository.GetAll();
                return View(viewModel);
            }

            viewModel.Person.Roles = new List<Role>();
            if (viewModel.SelectedRoles != null)
            {
                foreach (var roleId in viewModel.SelectedRoles)
                    viewModel.Person.Roles.Add(await _roleRepository.GetById(roleId));
            }

            await _personRepository.Create(viewModel.Person);
            return RedirectToAction(nameof(Index));
        }

        // GET EDIT
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _personRepository.GetById(id);
            if (person == null) return NotFound();

            var viewModel = new PersonFormViewModel
            {
                Person = person,
                Roles = await _roleRepository.GetAll(),
                SelectedRoles = person.Roles?.Select(r => r.Id).ToList(),
                Genders = await _genderRepository.GetAll(),
                Addresses = await _addressRepository.GetAll()
            };

            return View(viewModel);
        }

        // POST EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PersonFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Roles = await _roleRepository.GetAll();
                viewModel.Genders = await _genderRepository.GetAll();
                viewModel.Addresses = await _addressRepository.GetAll();
                return View(viewModel);
            }

            var person = await _personRepository.GetById(viewModel.Person.Id);
            if (person == null) return NotFound();

            person.Name = viewModel.Person.Name;
            person.Cpf = viewModel.Person.Cpf;
            person.BirthDate = viewModel.Person.BirthDate;
            person.Contact = viewModel.Person.Contact;
            person.GenderId = viewModel.Person.GenderId;
            person.AddressId = viewModel.Person.AddressId;

            person.Roles ??= new List<Role>();
            person.Roles.Clear();

            if (viewModel.SelectedRoles != null)
            {
                person.Roles = new List<Role>();
                foreach (var roleId in viewModel.SelectedRoles)
                    person.Roles.Add(await _roleRepository.GetById(roleId));
            }

            await _personRepository.Update(person);
            return RedirectToAction(nameof(Index));
        }

        // DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _personRepository.GetById(id);
            if (person == null) return NotFound();

            await _personRepository.Delete(person);
            return RedirectToAction(nameof(Index));
        }

        // Search Bar
        [HttpGet]
        public async Task<IActionResult> Search(string? search)
        {
            if (!string.IsNullOrEmpty(search))
            {
                var personFiltred = await _personRepository.GetByName(search);
                return View(personFiltred);
            }

            var persons = await _personRepository.GetAll();
            return View(persons);
        }
    }
}
