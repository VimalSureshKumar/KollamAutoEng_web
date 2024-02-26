using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace KollamAutoEng_web.Areas.Identity.Data;

public class KollamAutoEng_webUser : IdentityUser
{
    public string ProfilePicturePath { get; set; }
}

