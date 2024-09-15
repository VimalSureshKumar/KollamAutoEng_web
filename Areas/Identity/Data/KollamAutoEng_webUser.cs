using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using KollamAutoEng_web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Scripting;

namespace KollamAutoEng_web.Areas.Identity.Data;

public class KollamAutoEng_webUser : IdentityUser
{
    [Required(ErrorMessage = "Please enter First Name")]
    [MaxLength(25)]
    [RegularExpression("^[A-Za-z]+( [A-Za-z]+)*$", ErrorMessage = "Only letters and single spaces between words are allowed.")]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Please enter Last Name")]
    [MaxLength(25)]
    [RegularExpression("^[A-Za-z]+( [A-Za-z]+)*$", ErrorMessage = "Only letters and single spaces between words are allowed.")]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Please enter an email address")]
    [DataType(DataType.EmailAddress)]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Please enter an phone number")]
    [DataType(DataType.PhoneNumber)]
    [MaxLength(17)]
    [RegularExpression(@"^(\+64|0)\d{2,4}[\s-]?\d{4}[\s-]?\d{3,4}$|^(\+91|0)\d{10}$", ErrorMessage = "Please enter a valid phone number in New Zealand or India format.")]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }
}


