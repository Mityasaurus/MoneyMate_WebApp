using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyMate_WebApp.BusinessLogic.Dtos;
using MoneyMate_WebApp.Models.Transaction;
using MoneyMate_WebApp.BusinessLogic.Contracts;
using MoneyMate_WebApp.DataAccess.Entities;

namespace MoneyMate_WebApp.Controllers
{
    [Route("transaction")]
    public class TransactionController(ITransactionService transactionService, ICategoryService categoryService) : Controller
    {
        private readonly ITransactionService _transactionService = transactionService;
        private readonly ICategoryService _categoryService = categoryService;

        private async Task PopulateCategoryViewBagsAsync()
        {
            string languageCode = Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
            var allCategories = await _categoryService.GetAllCategoriesAsync(languageCode);
            var expenseCategories = allCategories
                .Where(c => c.Type == TransactionType.Expense)
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();
            var incomeCategories = allCategories
                .Where(c => c.Type == TransactionType.Income)
                .OrderBy(c => c.Name)
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();

            ViewBag.ExpenseCategories = expenseCategories;
            ViewBag.IncomeCategories = incomeCategories;
        }

        [HttpGet("create/{accountId:guid}")]
        public async Task<IActionResult> Create(Guid accountId)
        {
            await PopulateCategoryViewBagsAsync();
            var model = new CreateTransactionViewModel
            {
                AccountId = accountId,
                Date = DateTime.Now
            };
            return View(model);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateTransactionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCategoryViewBagsAsync();
                return View(model);
            }

            var transactionDto = new TransactionDto(
                Guid.Empty,
                model.AccountId,
                model.CategoryId,
                model.Date,
                model.Amount,
                model.Comment
            );

            try
            {

                await _transactionService.CreateTransactionAsync(transactionDto);
                return RedirectToAction("Index", "Account");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await PopulateCategoryViewBagsAsync();
                return View(model);
            }
        }
    }
}
