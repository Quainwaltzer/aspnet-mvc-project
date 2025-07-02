using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using PracticeNa.Data;
using PracticeNa.Models;
using PracticeNa.ViewModels;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace PracticeNa.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BudgetDbContext _context;
        public HomeController(ILogger<HomeController> logger, BudgetDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var viewModel = new BudgetViewModel
            {
                NewBudget = new Budget(), // empty model for the form
                BudgetList = _context.Budgets.ToList(), // show all entries NewBudget = item,
                
                newCat = new Categories(),
                CatList = _context.Second.ToList()
            };

            return View(viewModel);
        }

        // POST: Budget/Create
        [HttpPost]
        public IActionResult Create(BudgetViewModel viewModel)
        {

            _context.Second.Add(viewModel.newCat);
            _context.SaveChanges();

            // Step 2: Assign the new category ID to the budget
            viewModel.NewBudget.CategoryId = viewModel.newCat.Id;

            // Step 3: Save the budget
            _context.Budgets.Add(viewModel.NewBudget);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var budget = _context.Budgets.FirstOrDefault(t => t.Id == id);
                _context.Budgets.Remove(budget);
                _context.SaveChanges();
            

            return RedirectToAction(nameof(Index));
        }


        // GET: Budget/Edit/5
        // GET: Home/Edit/5
        public IActionResult Edit(int id)
        {
            var item = _context.Budgets.Find(id);
            var catitem = _context.Second.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            var viewModel = new BudgetViewModel
            {
                NewBudget = item,
                BudgetList = _context.Budgets.ToList(),
                newCat = catitem,
                CatList = _context.Second.ToList()
            };

            return View(viewModel);
        }

        // POST: Home/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(BudgetViewModel viewModel)
        {
            _context.Update(viewModel.newCat);
            _context.SaveChanges();

            viewModel.NewBudget.CategoryId = viewModel.newCat.Id;
            _context.Update(viewModel.NewBudget);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
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

        public IActionResult ConfirmDelete(int id)
        {
            return PartialView("_DeleteConfirmation", id);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {

            //BudgetViewModel viewModel = new BudgetViewModel();
            var budget = _context.Budgets.FirstOrDefault(t => t.Id == id);
            var budget2 = _context.Second.FirstOrDefault(t => t.Id == id);
            _context.Remove(budget);
            _context.SaveChanges();

            
            _context.Remove(budget2);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Expenses()
        {
            var budgets = _context.Budgets.Include(b => b.Category).ToList();

            var chartData = budgets
                .GroupBy(b => b.Category.Description)
                .Select(g => new CategoryChartData
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(b => b.Amount ?? 0)
                }).ToList();

            var mostExp = budgets
                .GroupBy(b => b.Category.Description)
                .Select(g => new CategoryChartData
                {
                    Category = g.Key,
                    TotalCount = g.Count()
                }).ToList();


            var viewModel = new BudgetViewModel
            {
                BudgetList = budgets,
                ChartData = chartData,
                MostExp = mostExp
            };

            return View(viewModel);
        }


    }
}
