using Microsoft.AspNetCore.Authorization;
using KollamAutoEng_web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KollamAutoEng_web.Models;

namespace KollamAutoEng_web.Areas.Identity.Data;

// Context class for managing the identity database for the application
public class KollamAutoEng_webContext : IdentityDbContext<KollamAutoEng_webUser>
{
    // Constructor accepting DbContextOptions to configure the context
    public KollamAutoEng_webContext(DbContextOptions<KollamAutoEng_webContext> options)
        : base(options)
    {
    }

    // Override method to configure the model before it is used
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // Call the base method to ensure identity model configurations are applied

        // Seeding roles with RoleValue
        var admin = new ApplicationRole
        {
            Id = "1", // Unique identifier for the role
            Name = "Admin", // Role name
            NormalizedName = "ADMIN", // Normalized name for querying
            RoleValue = 1 // Role value for assigning privileges
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
            .HasOne(v => v.Customer) // Each Vehicle has one Customer
            .WithMany(c => c.Vehicles) // A Customer can have many Vehicles
            .HasForeignKey(v => v.CustomerId) // Foreign key relationship
            .OnDelete(DeleteBehavior.Cascade); // Specify delete behavior

        builder.Entity<Vehicle>()
            .HasOne(v => v.VehicleBrand) // Each Vehicle has one VehicleBrand
            .WithMany(b => b.Vehicles) // A VehicleBrand can have many Vehicles
            .HasForeignKey(v => v.BrandId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Vehicle>()
            .HasOne(v => v.VehicleModel) // Each Vehicle has one VehicleModel
            .WithMany(m => m.Vehicles) // A VehicleModel can have many Vehicles
            .HasForeignKey(v => v.ModelId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure relationships for the VehicleModel entity
        builder.Entity<VehicleModel>()
            .HasOne(vm => vm.VehicleBrand) // Each VehicleModel has one VehicleBrand
            .WithMany(vb => vb.VehicleModels) // A VehicleBrand can have many VehicleModels
            .HasForeignKey(vm => vm.BrandId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure relationships for the Appointment entity
        builder.Entity<Appointment>()
            .HasOne(a => a.Customer) // Each Appointment has one Customer
            .WithMany(c => c.Appointments) // A Customer can have many Appointments
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade); // Delete Appointments when the Customer is deleted

        builder.Entity<Appointment>()
            .HasOne(a => a.Vehicle) // Each Appointment has one Vehicle
            .WithMany(v => v.Appointments) // A Vehicle can have many Appointments
            .HasForeignKey(a => a.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Appointment>()
            .HasOne(a => a.Employee) // Each Appointment has one Employee
            .WithMany(e => e.Appointments) // An Employee can have many Appointments
            .HasForeignKey(a => a.EmployeeId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure relationships for the Fault entity
        builder.Entity<Fault>()
            .HasOne(f => f.Customer) // Each Fault has one Customer
            .WithMany(c => c.Faults) // A Customer can have many Faults
            .HasForeignKey(f => f.CustomerId)
            .OnDelete(DeleteBehavior.Cascade); // Do not delete Faults when the Customer is deleted

        builder.Entity<Fault>()
            .HasOne(f => f.Vehicle) // Each Fault has one Vehicle
            .WithMany(v => v.Faults) // A Vehicle can have many Faults
            .HasForeignKey(f => f.VehicleId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure relationships for the FaultPart entity
        builder.Entity<FaultPart>()
            .HasOne(fp => fp.Fault) // Each FaultPart has one Fault
            .WithMany(f => f.FaultParts) // A Fault can have many FaultParts
            .HasForeignKey(fp => fp.FaultId)
            .OnDelete(DeleteBehavior.Cascade); // Delete FaultParts when the Fault is deleted

        builder.Entity<FaultPart>()
            .HasOne(fp => fp.Part) // Each FaultPart has one Part
            .WithMany(p => p.FaultParts) // A Part can have many FaultParts
            .HasForeignKey(fp => fp.PartId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<FaultPart>()
            .HasOne(fp => fp.Appointment) // Each FaultPart has one Appointment
            .WithMany(a => a.FaultParts) // An Appointment can have many FaultParts
            .HasForeignKey(fp => fp.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configure relationships for the Payment entity
        builder.Entity<Payment>()
            .HasOne(p => p.Customer) // Each Payment has one Customer
            .WithMany(a => a.Payments) // A Customer can have many Payments
            .HasForeignKey(p => p.CustomerId)
            .OnDelete(DeleteBehavior.Cascade); // Delete Payments when the Customer is deleted
    }

    // DbSet properties for each entity in the context
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
