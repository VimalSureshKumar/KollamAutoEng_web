using System;
using System.ComponentModel.DataAnnotations;

namespace KollamAutoEng_web.ValidationAttributes
{
    public class DateValidator2 : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var date = (DateTime)value;
                var currentDate = DateTime.Now;
                var hundredYearBefore = currentDate.AddYears(-80);

                if (date < currentDate.Date || date > hundredYearBefore.Date)
                {
                    return new ValidationResult($"The date of birth must be between {currentDate:MM/dd/yyyy} and {hundredYearBefore:MM/dd/yyyy}.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
