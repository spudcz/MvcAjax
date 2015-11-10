using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MvcAjax.Models
{
    public class CustomEmailValidationAttribute : ValidationAttribute
    {
        private static readonly Regex Pattern = new Regex(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]+", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var email = value.ToString();
            return Pattern.IsMatch(email) 
                ? ValidationResult.Success 
                : new ValidationResult("Please enter a valid email or leave empty.");
        }
    }
}