using BethanysPieShopAdmin.Models;
using BethanysPieShopAdmin.Models.Repositories;
using BethanysPieShopAdmin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BethanysPieShopAdmin.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index()
        {
            var pies = await _pieRepository.GetAllPiesAsync();
            return View(pies);
        }

        public async Task<IActionResult> Details(int id)
        {
            var pie = await _pieRepository.GetPieByIdAsync(id);
            return View(pie);
        }

        public async Task<IActionResult> Add()
        {
            var categories = await _pieRepository.GetAllPiesAsync();
            IEnumerable<SelectListItem> selectedListItems = new SelectList(categories, "CategoryId", "Name", null);
            PieAddViewModel viewModel = new()
            {
                Categories = selectedListItems
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PieAddViewModel pieViewModel) 
        {
            Pie pie = new()
            {
                CategoryId = pieViewModel.Pies.CategoryId,
                ShortDescription = pieViewModel.Pies.ShortDescription,
                LongDescription = pieViewModel.Pies.LongDescription,
                Price = pieViewModel.Pies.Price,
                AllergyInformation = pieViewModel.Pies.AllergyInformation,
                ImageThumbnailUrl = pieViewModel.Pies.ImageThumbnailUrl,
                ImageUrl = pieViewModel.Pies.ImageUrl,
                Ingredients = pieViewModel.Pies.Ingredients,
                InStock = pieViewModel.Pies.InStock,
                Name = pieViewModel.Pies.Name
            };
            await _pieRepository.AddPie(pie);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> List(string category)
        {
            var pies = await _pieRepository.GetAllPiesAsync();
            string? currentCategory = string.Empty;

            if (!string.IsNullOrEmpty(category))
            {
                pies = pies.Where(p => p.Category!.Name.Equals(category, StringComparison.OrdinalIgnoreCase));
                currentCategory = _categoryRepository.GetAllCategories().FirstOrDefault(c => c.Name == category)?.Name;
            }
            else
            {
                pies = pies.OrderBy(p => p.PieId);
            }
                return View( new PieListViewModel() { CurrentCategory = currentCategory!, Pies = pies });
        }
    }
}
