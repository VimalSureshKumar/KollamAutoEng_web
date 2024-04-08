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
    [RegularExpression(@"^[A-Z][a-zA-Z]*(\s[A-Z][a-zA-Z]*){1,2}$", ErrorMessage = "Please enter a valid name starting with capital letters.")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }
    [DataType(DataType.Text)]
    [Required(ErrorMessage = "Please enter Full Name"), MaxLength(30)]
    [RegularExpression(@"^[A-Z][a-zA-Z]*(\s[A-Z][a-zA-Z]*){1,2}$", ErrorMessage = "Please enter a valid name starting with capital letters.")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

}


