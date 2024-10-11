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

                // Calculate the minimum valid date (current year - 100 years)
                var minDate = DateTime.Now.AddYears(-100);

                // Calculate the maximum valid date (current date - 16 years)
                var maxDate = DateTime.Now.AddYears(-16);

                // Check if the date is outside the valid range
                if (date < minDate || date > maxDate)
                {
                    return new ValidationResult($"The date of birth must be between {minDate:dd/MM/yyyy} and {maxDate:dd/MM/yyyy}.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
