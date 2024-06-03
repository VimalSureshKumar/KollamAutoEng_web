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

    public DbSet<KollamAutoEng_web.Models.Employee> Employee { get; set; } = default!;

    public DbSet<KollamAutoEng_web.Models.Fault> Fault { get; set; } = default!;

    public DbSet<KollamAutoEng_web.Models.Vehicle> Vehicle { get; set; } = default!;

    public DbSet<KollamAutoEng_web.Models.Appointment> Appointment { get; set; } = default!;

    public DbSet<KollamAutoEng_web.Models.Customer> Customer { get; set; } = default!;

    public DbSet<KollamAutoEng_web.Models.Part> Part { get; set; } = default!;
    public DbSet<KollamAutoEng_web.Models.FaultPart> FaultPart { get; set; } = default!;
    public DbSet<KollamAutoEng_web.Models.Payment> Payment { get; set; } = default!;
}
