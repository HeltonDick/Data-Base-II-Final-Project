using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Cinema_BD2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

public class FilmController : Controller
{
    // Injeção de dependência
    private readonly IFilmRepository _filmRepo;
    private readonly IGenreRepository _genreRepo;
    private readonly IStudioRepository _studioRepo;
    private readonly IClassificationRepository _classificationRepo;

    // Construtor
    public FilmController(
        IFilmRepository filmRepo,
        IGenreRepository genreRepo,
        IStudioRepository studioRepo,
        IClassificationRepository classificationRepo)
    {
        _filmRepo = filmRepo;
        _genreRepo = genreRepo;
        _studioRepo = studioRepo;
        _classificationRepo = classificationRepo;
    }


    // Busca no banco e envia para a view
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var films = await _filmRepo.GetAll();
        return View(films);
    }

    // Cria um ViewModel com as listas que populam os dropdowns e checklists
    // Retorna a view com os dados necessários para preencher o formulário
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var vm = new FilmFormViewModel
        {
            Genres = await _genreRepo.GetAll(),
            Studios = await _studioRepo.GetAll(),
            Classifications = await _classificationRepo.GetAll()
        };

        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(FilmFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Genres = await _genreRepo.GetAll();
            vm.Studios = await _studioRepo.GetAll();
            vm.Classifications = await _classificationRepo.GetAll();
            return View(vm);
        }

        vm.Film.Genres = new List<Genre>();
        if (vm.SelectedGenreIds != null)
            vm.Film.Genres = (await _genreRepo.GetAll())
                .Where(x => vm.SelectedGenreIds.Contains(x.Id)).ToList();

        vm.Film.Studios = new List<Studio>();
        if (vm.SelectedStudioIds != null)
            vm.Film.Studios = (await _studioRepo.GetAll())
                .Where(x => vm.SelectedStudioIds.Contains(x.Id)).ToList();

        await _filmRepo.Create(vm.Film);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id) // Busca por id
    {
        var film = await _filmRepo.GetById(id);
        if (film == null) return NotFound(); // valida

        var vm = new FilmFormViewModel
        {
            Film = film,
            Genres = await _genreRepo.GetAll(), // Busca todos os generos
            Studios = await _studioRepo.GetAll(), // Busca todos os estudios
            Classifications = await _classificationRepo.GetAll(), // Todas as classificacoes
            SelectedGenreIds = film.Genres.Select(g => g.Id).ToList(), // Busca o genero pelo id, relacionando com filme
            SelectedStudioIds = film.Studios.Select(s => s.Id).ToList() // Mesma coisa
        };

        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(FilmFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Genres = await _genreRepo.GetAll();
            vm.Studios = await _studioRepo.GetAll();
            vm.Classifications = await _classificationRepo.GetAll();
            return View(vm);
        }

        var film = await _filmRepo.GetById(vm.Film.Id);
        if(film == null) return NotFound();

        film.Title = vm.Film.Title;
        film.Duration = vm.Film.Duration;
        film.Description = vm.Film.Description;
        film.ClassificationId = vm.Film.ClassificationId;

        film.Genres ??= new List<Genre>();
        film.Studios ??= new List<Studio>();
        film.Genres.Clear();
        film.Studios.Clear();

        if (vm.SelectedGenreIds != null && vm.SelectedStudioIds != null)
        {
            film.Genres = new List<Genre>();
            foreach (var genreId in vm.SelectedGenreIds)
                film.Genres.Add(await _genreRepo.GetById(genreId));

            film.Studios = new List<Studio>();
            foreach (var studioId in vm.SelectedStudioIds)
                film.Studios.Add(await _studioRepo.GetById(studioId));
        }

        await _filmRepo.Update(film);
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var film = await _filmRepo.GetById(id);
        if (film == null) return NotFound();

        await _filmRepo.Delete(film);
        return RedirectToAction(nameof(Index));
    }
}
