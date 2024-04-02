using Microsoft.AspNetCore.Authorization;
using KollamAutoEng_web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KollamAutoEng_web.Models;

namespace KollamAutoEng_web.Areas.Identity.Data;

public class KollamAutoEng_webContext : IdentityDbContext<KollamAutoEng_webUser>
{
    public KollamAutoEng_webContext(DbContextOptions<KollamAutoEng_webContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
