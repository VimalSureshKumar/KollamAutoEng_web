using System;
using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.ValidationAttributes
{
    public class DateValidator : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var date = (DateTime)value;
                var currentDate = DateTime.Now;
                var oneYearLater = currentDate.AddYears(1);

                if (date < currentDate.Date || date > oneYearLater.Date)
                {
                    return new ValidationResult($"The appointment date must be between {currentDate:d/MM/yyyy} and {oneYearLater:d/MM/yyyy}.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
