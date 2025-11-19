using Cinema_BD2.Models;
using Cinema_BD2.Repository;
using Cinema_BD2.ViewModels;
using Microsoft.AspNetCore.Mvc;

public class FilmController : Controller
{
    private readonly IFilmRepository _filmRepo;
    private readonly IGenreRepository _genreRepo;
    private readonly IStudioRepository _studioRepo;
    private readonly IClassificationRepository _classificationRepo;

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

    public async Task<IActionResult> Index()
    {
        var films = await _filmRepo.GetAll();
        return View(films);
    }

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

        var film = new Film
        {
            Title = vm.Title,
            Duration = vm.Duration,
            Description = vm.Description,
            ClassificationId = vm.ClassificationId,
            Genres = new List<Genre>(),
            Studios = new List<Studio>()
        };

        if (vm.SelectedGenreIds != null)
            film.Genres = (await _genreRepo.GetAll())
                .Where(x => vm.SelectedGenreIds.Contains(x.Id)).ToList();

        if (vm.SelectedStudioIds != null)
            film.Studios = (await _studioRepo.GetAll())
                .Where(x => vm.SelectedStudioIds.Contains(x.Id)).ToList();

        await _filmRepo.Create(film);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var film = await _filmRepo.GetById(id);
        if (film == null) return NotFound();


        var vm = new FilmFormViewModel
        {
            Id = film.Id,
            Title = film.Title,
            Duration = film.Duration,
            Description = film.Description,
            ClassificationId = film.ClassificationId,
            Genres = await _genreRepo.GetAll(),
            Studios = await _studioRepo.GetAll(),
            Classifications = await _classificationRepo.GetAll(),
            SelectedGenreIds = film.Genres.Select(g => g.Id).ToList(),
            SelectedStudioIds = film.Studios.Select(s => s.Id).ToList()
        };


        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(FilmFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            vm.Genres = await _genreRepo.GetAll();
            vm.Studios = await _studioRepo.GetAll();
            vm.Classifications = await _classificationRepo.GetAll();
            return View(vm);
        }


        var film = await _filmRepo.GetById(vm.Id);
        film.Title = vm.Title;
        film.Duration = vm.Duration;
        film.Description = vm.Description;
        film.ClassificationId = vm.ClassificationId;


        film.Genres ??= new List<Genre>();
        film.Studios ??= new List<Studio>();

        film.Genres.Clear();
        film.Studios.Clear();


        if (vm.SelectedGenreIds != null)
            film.Genres = (await _genreRepo.GetAll()).Where(x => vm.SelectedGenreIds.Contains(x.Id)).ToList();


        if (vm.SelectedStudioIds != null)
            film.Studios = (await _studioRepo.GetAll()).Where(x => vm.SelectedStudioIds.Contains(x.Id)).ToList();


        await _filmRepo.Update(film);
        return RedirectToAction("Index");
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
