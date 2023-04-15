using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using ToDoList.Models;
using ToDoList.Models.DapperClasses;
using ToDoList.ViewModel;
namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAppRepository _appRepository;

        private List<Category> _categories;
        private List<Deal> _deals;

        private List<Deal> SortDeals()
        {
            List<Deal> a = new();

            List<Deal> dealsWithDueDate = _deals.Where(d => d.DueDate != null && !d.IsComplete).OrderBy(d => d.DueDate).ToList();
            List<Deal> dealsWithOutDueDate = _deals.Where(d => d.DueDate == null && !d.IsComplete).ToList();
            List<Deal> dealsIsComplete = _deals.Where(d => d.IsComplete).ToList();

            a.AddRange(dealsWithDueDate);
            a.AddRange(dealsWithOutDueDate);
            a.AddRange(dealsIsComplete);

            return a;
        }

        public HomeController(ILogger<HomeController> logger, IAppRepository repo)
        {
            _logger = logger;
            _appRepository = repo;
            _categories = repo.GetCategories();
            _categories.Insert(0, new Category { Id = -1, Name = "All Categories" });
            _deals = repo.GetDeals();
        }

        
        public IActionResult GetTask(DealUpdateView DealUpdate)
        {
            int? CategorySortId = HttpContext.Session.GetInt32("CategorySort");
            var a = new ToDoListView();
            a.Categories = _categories;
            a.Deals = SortDeals();
            a.DealUpdate = DealUpdate;
            if (CategorySortId == null || CategorySortId<0)
                return View("Index", a);

            a.Deals = SortDeals().Where(d => d.CategoryId == CategorySortId).ToList();
            return View("Index",a);
        }

        [HttpPost]
        public IActionResult SortTask(CategorySortView CategorySort)
        {
            HttpContext.Session.SetInt32("CategorySort",CategorySort.CategorySortId);
            return RedirectToAction("GetTask", new {CategorySort.CategorySortId });
        }

        [HttpPost]
        public async Task<IActionResult> CategoryAdd(CategoryView CategoryV)
        {
            if (ModelState.IsValid)
            {
                await _appRepository.AddCategory(CategoryV);
                Category newCategory = new(CategoryV);
                newCategory.Id = _categories.LastOrDefault() == null ? 1 : _categories.Last().Id++;
                _categories.Add(newCategory);
            }
            return RedirectToAction("GetTask");
        }

        
        public async Task<IActionResult> TaskAdd(DealView DealV)
        {
            if (ModelState.IsValid)
            {
                await _appRepository.AddDeal(DealV);
                Deal newDeal = new(DealV);
                newDeal.Id = _deals.LastOrDefault() == null ? 1 : _deals.Last().Id++;
                _deals.Add(newDeal);
            }
            return RedirectToAction("GetTask");
        }

        public async Task<IActionResult> UpdateDeal(int Id,DealView DealV)
        {
            if (ModelState.IsValid)
            {
                Deal deal = new(DealV);
                deal.Id = Id;
                await _appRepository.UpdateDeal(deal);
                _deals.ForEach(d =>
                {
                    if (d.Id == Id)
                        d = deal;
                });
            }
            return RedirectToAction("GetTask");
        }

        public async Task<IActionResult> DeleteDeal(int Id)
        {
            var deal = _deals.FirstOrDefault(d => d.Id == Id)!;
            _deals.Remove(deal);
           await _appRepository.DeleteDeal(Id);
            return RedirectToAction("GetTask");
        }

        public RedirectToActionResult UpdateDealRedirect(int Id)
        {
            var DealUpdate = new DealUpdateView(_deals.FirstOrDefault(d => d.Id == Id));
            return RedirectToAction("GetTask", new {DealUpdate.Id ,DealUpdate.DueDate, DealUpdate.CategoryId,DealUpdate.Name,DealUpdate.IsComplete });
        }

        public IActionResult UpdateDealState(int Id)
        {
            Deal deal = _deals.FirstOrDefault(d => d.Id == Id);
            _appRepository.UpdateStateDeal(Id, !deal.IsComplete);
            return RedirectToAction("GetTask");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}