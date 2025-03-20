using System.ComponentModel.DataAnnotations;

namespace MoneyMate_WebApp.BusinessLogic.Attributes
{
    public class NotFutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is DateTime dateValue)
            {
                if (dateValue > DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage ?? "Дата не може бути пізнішою за сьогоднішню.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
