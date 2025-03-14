using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MoneyMate_WebApp.BusinessLogic.Dtos;
using MoneyMate_WebApp.Models.Account;
using MoneyMate_WebApp.DataAccess.UoW;
using MoneyMate_WebApp.BusinessLogic.Contracts;

namespace MoneyMate_WebApp.Controllers
{
    public class AccountController(ILogger<AccountController> logger, 
        IAccountService accountService, 
        IUnitOfWork unitOfWork) : Controller
    {
        private readonly ILogger<AccountController> _logger = logger;
        private readonly IAccountService _accountService = accountService;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IActionResult> Index()
        {
            var accountsDto = await _accountService.GetAllAccountsAsync();
            var viewModels = accountsDto.Select(dto => new AccountDisplayViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                AccountTypeName = dto.Type?.Name ?? string.Empty,
                CurrencyName = dto.Currency?.CurrencyName ?? string.Empty,
                CurrencyCode = dto.Currency?.CurrencyCode ?? string.Empty,
                CurrencySymbol = dto.Currency?.CurrencySymbol ?? string.Empty,
                Balance = dto.Balance
            }).ToList();

            return View(viewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var types = await _unitOfWork.Types.GetAllAsync();
            ViewBag.AccountTypes = types.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            }).ToList();

            var currencies = await _unitOfWork.Currencies.GetAllAsync();
            ViewBag.Currencies = currencies.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.CurrencyCode} - {c.CurrencyName}"
            }).ToList();

            return View(new CreateAccountViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAccountViewModel model)
        {
            _logger.LogInformation("Received request to create a new account with Name: {Name}, TypeId: {TypeId}, CurrencyId: {CurrencyId}, Balance: {Balance}",
                model.Name, model.TypeId, model.CurrencyId, model.Balance);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid. Returning to the creation form.");

                var types = await _unitOfWork.Types.GetAllAsync();
                ViewBag.AccountTypes = types.Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name
                }).ToList();

                var currencies = await _unitOfWork.Currencies.GetAllAsync();
                ViewBag.Currencies = currencies.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.CurrencyCode} - {c.CurrencyName}"
                }).ToList();

                return View(model);
            }

            var accountDto = new AccountDto(
                Guid.Empty,
                model.Name,
                model.TypeId,
                model.CurrencyId,
                model.Balance,
                null,
                null);

            await _accountService.CreateAccountAsync(accountDto);

            _logger.LogInformation("Account '{Name}' created successfully.", model.Name);

            return RedirectToAction(nameof(Index));
        }

    }
}
