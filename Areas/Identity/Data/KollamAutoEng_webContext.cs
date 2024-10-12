using Microsoft.AspNetCore.Authorization;
using KollamAutoEng_web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KollamAutoEng_web.Models;
using System.Reflection.Emit;

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

        // Seeding roles with RoleValue
        var admin = new ApplicationRole
        {
            Id = "1", 
            Name = "Admin",
            NormalizedName = "ADMIN",
            RoleValue = 1
        };

        var employee = new ApplicationRole
        {
            Id = "2",
            Name = "Employee",
            NormalizedName = "EMPLOYEE",
            RoleValue = 2
        };

        var user = new ApplicationRole
        {
            Id = "3",
            Name = "User",
            NormalizedName = "USER",
            RoleValue = 3
        };

        // Seed data into the roles table
        builder.Entity<ApplicationRole>().HasData(admin, employee, user);
    }

    public DbSet<KollamAutoEng_web.Models.Employee> Employee { get; set; } = default!;
    public DbSet<KollamAutoEng_web.Models.Fault> Fault { get; set; } = default!;
    public DbSet<KollamAutoEng_web.Models.Vehicle> Vehicle { get; set; } = default!;
    public DbSet<KollamAutoEng_web.Models.Appointment> Appointment { get; set; } = default!;
    public DbSet<KollamAutoEng_web.Models.Customer> Customer { get; set; } = default!;
    public DbSet<KollamAutoEng_web.Models.Part> Part { get; set; } = default!;
    public DbSet<KollamAutoEng_web.Models.FaultPart> FaultPart { get; set; } = default!;
    public DbSet<KollamAutoEng_web.Models.Payment> Payment { get; set; } = default!;
    public DbSet<KollamAutoEng_web.Models.VehicleBrand> VehicleBrand { get; set; } = default!;
    public DbSet<KollamAutoEng_web.Models.VehicleModel> VehicleModel { get; set; } = default!;
}
