using System.ComponentModel.DataAnnotations;

public class DateValidator : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            var date = (DateTime)value;
            var currentDate = DateTime.Now;

            var controllerName = validationContext.ObjectType.Name;

            if (controllerName.Contains("Create"))
            {
                var maxDate = currentDate.AddDays(14);
                if (date < currentDate.Date || date > maxDate.Date)
                {
                    return new ValidationResult($"The appointment date must be between {currentDate:d/MM/yyyy} and {maxDate:d/MM/yyyy}.");
                }
            }
            else if (controllerName.Contains("Edit"))
            {
                var minDate = currentDate.AddYears(-1);
                var maxDate = currentDate.AddYears(1);
                if (date < minDate.Date || date > maxDate.Date)
                {
                    return new ValidationResult($"The appointment date must be between {minDate:d/MM/yyyy} and {maxDate:d/MM/yyyy}.");
                }
            }
        }
        return ValidationResult.Success;
    }
}
