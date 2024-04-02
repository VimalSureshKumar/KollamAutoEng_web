using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Scripting;

namespace KollamAutoEng_web.Areas.Identity.Data;

public class KollamAutoEng_webUser : IdentityUser
{
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Please enter Full Name"), MaxLength(30)]
    [RegularExpression(@"^[A-Z][a-zA-Z]*(\s[A-Z][a-zA-Z]*){1,2}$", ErrorMessage = "Please enter a valid fullname starting with capital letters.")]
    [Display(Name = "Full Name")]
    public string FirstName { get; set; }
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Please enter Full Name"), MaxLength(30)]
    [RegularExpression(@"^[A-Z][a-zA-Z]*(\s[A-Z][a-zA-Z]*){1,2}$", ErrorMessage = "Please enter a valid fullname starting with capital letters.")]
    [Display(Name = "Full Name")]
    public string LastName { get; set; }
    [DataType(DataType.PhoneNumber), MaxLength(17)]
    [RegularExpression(@"^\(\+64\) \d{2}-\d{3}-\d{4}$", ErrorMessage = "Phone Number is not valid. Accepted format (+64) 12-345-6789 for example.")]
    [Display(Name = "Phone Number")]
    public string Phone_Number { get; set; }

}


