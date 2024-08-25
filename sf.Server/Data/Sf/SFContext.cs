using sf.Server.Models.Auth;
using sf.Server.Models.Core;
using sf.Server.Models.SF;

namespace sf.Server.Data.Sf;

public class SfContext(DbContextOptions<SfContext> options) : DbContext(options)
{
    public new DbSet<User> Users { get; init; }


    public DbSet<Class> Classes { get; init; }
    public DbSet<Discipline> Disciplines { get; init; }
    public DbSet<Entry> Entries { get; init; }
    public DbSet<Location> Locations { get; init; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Entry>().ToTable("Entries");
        modelBuilder.Entity<Class>().ToTable("Classes");
        modelBuilder.Entity<Discipline>().ToTable("Disciplines");
        modelBuilder.Entity<Location>().ToTable("Locations");
        // Composite key for Entry
        modelBuilder.Entity<Entry>()
                    .HasKey(e => e.Id);

        // User-Class relationship
        modelBuilder.Entity<User>()
                    .HasOne(u => u.Class)
                    .WithMany(c => c.Users)
                    .HasForeignKey(u => u.ClassId);

        // User-Discipline relationship
        modelBuilder.Entity<User>()
                    .HasMany(u => u.Disciplines)
                    .WithMany(d => d.Users)
                    .UsingEntity<Dictionary<string, object>>(
                         "UserDiscipline",
                         j => j.HasOne<Discipline>().WithMany().HasForeignKey("DisciplineId"),
                         j => j.HasOne<User>().WithMany().HasForeignKey("UserId"));

        // Entry relationships
        modelBuilder.Entity<Entry>()
                    .HasOne(e => e.Student)
                    .WithMany(u => u.Entries)
                    .HasForeignKey(e => e.StudentId);

        modelBuilder.Entity<Entry>()
                    .HasOne(e => e.Discipline)
                    .WithMany(d => d.Entries)
                    .HasForeignKey(e => e.DisciplineId);

        // Class-Location relationship
        modelBuilder.Entity<Class>()
                    .HasOne(c => c.Room)
                    .WithMany(l => l.Classes)
                    .HasForeignKey(c => c.RoomId);

        // Discipline-Location relationship
        modelBuilder.Entity<Discipline>()
                    .HasOne(d => d.Location)
                    .WithMany(l => l.Disciplines)
                    .HasForeignKey(d => d.LocationId);

        // Automatically set CreatedAt and UpdatedAt
        modelBuilder.Entity<Entity<Guid>>()
                    .Property(e => e.CreatedAt)
                    .ValueGeneratedOnAdd();

        modelBuilder.Entity<Entity<Guid>>()
                    .Property(e => e.UpdatedAt)
                    .ValueGeneratedOnAddOrUpdate();
    }
    
    
}