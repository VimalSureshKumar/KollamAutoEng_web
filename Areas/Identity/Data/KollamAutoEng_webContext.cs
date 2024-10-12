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

        builder.Entity<Vehicle>()
            .HasOne(v => v.Customer)
            .WithMany(c => c.Vehicles)
            .HasForeignKey(v => v.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Vehicle>()
            .HasOne(v => v.VehicleBrand)
            .WithMany(b => b.Vehicles)
            .HasForeignKey(v => v.BrandId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Vehicle>()
            .HasOne(v => v.VehicleModel)
            .WithMany(m => m.Vehicles)
            .HasForeignKey(v => v.ModelId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<VehicleModel>()
            .HasOne(vm => vm.VehicleBrand)
            .WithMany(vb => vb.VehicleModels)
            .HasForeignKey(vm => vm.BrandId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Appointment>()
            .HasOne(a => a.Customer)
            .WithMany(c => c.Appointments)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Appointment>()
            .HasOne(a => a.Vehicle)
            .WithMany(v => v.Appointments)
            .HasForeignKey(a => a.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Appointment>()
            .HasOne(a => a.Employee)
            .WithMany(e => e.Appointments)
            .HasForeignKey(a => a.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Fault>()
            .HasOne(f => f.Customer)
            .WithMany(c => c.Faults)
            .HasForeignKey(f => f.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Fault>()
            .HasOne(f => f.Vehicle)
            .WithMany(v => v.Faults)
            .HasForeignKey(f => f.VehicleId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<FaultPart>()
            .HasOne(fp => fp.Fault)
            .WithMany(f => f.FaultParts)
            .HasForeignKey(fp => fp.FaultId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<FaultPart>()
            .HasOne(fp => fp.Part)
            .WithMany(p => p.FaultParts)
            .HasForeignKey(fp => fp.PartId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<FaultPart>()
            .HasOne(fp => fp.Appointment)
            .WithMany(a => a.FaultParts)
            .HasForeignKey(fp => fp.AppointmentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Payment>()
            .HasOne(p => p.Customer)
            .WithMany(a => a.Payments)
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
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
