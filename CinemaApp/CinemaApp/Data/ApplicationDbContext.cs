using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CinemaApp.Models;

namespace CinemaApp.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Booking> Bookings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Room>()
            .HasMany(r => r.Schedules)
            .WithOne(s => s.Room)
            .HasForeignKey(s => s.RoomId);

        modelBuilder.Entity<Schedule>()
            .HasOne(s => s.Movie)
            .WithMany(m => m.Schedules)
            .HasForeignKey(s => s.MovieId);

        modelBuilder.Entity<Schedule>()
            .HasMany(s => s.Bookings)
            .WithOne(b => b.Schedule)
            .HasForeignKey(b => b.ScheduleId);
    }
}