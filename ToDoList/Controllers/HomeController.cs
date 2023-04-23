using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using ToDoList.Models;
using ToDoList.Models.DapperClasses;
using ToDoList.Models.FileMethodClasses;
using ToDoList.ViewModel;
using System.Web;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileMethod _appRepository;

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

        public HomeController(ILogger<HomeController> logger, IFileMethod repo)
        {
            _logger = logger;
            _appRepository = repo;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            _appRepository.fileMethod(HttpContext.Session.GetInt32("FileMethod"));
            _categories = _appRepository.GetCategories();
            _categories.Insert(0, new Category { Id = -1, Name = "All Categories" });
            _deals = _appRepository.GetDeals();
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
            }
            return RedirectToAction("GetTask");
        }

        
        public async Task<IActionResult> TaskAdd(DealView DealV)
        {
            if (ModelState.IsValid)
            {
                await _appRepository.AddDeal(DealV);
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
               
            }
            return RedirectToAction("GetTask");
        }

        public async Task<IActionResult> DeleteDeal(int Id)
        {
            var deal = _deals.FirstOrDefault(d => d.Id == Id)!;
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

        public IActionResult FileMethod(int Id)
        {
            ControllerContext.HttpContext.Session.SetInt32("FileMethod", Id);
            ControllerContext.HttpContext.Session.Remove("CategorySort");
            return RedirectToAction("GetTask");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}