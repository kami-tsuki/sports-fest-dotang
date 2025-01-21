using sf.Server.Models.SF;

namespace sf.Server.Data.Sf;

public class SfContext(DbContextOptions<SfContext> options)
    : DbContext(options)
{
    public DbSet<UserModel> Users { get; init; }
    public DbSet<ClassModel> Classes { get; init; }
    public DbSet<TeamModel> Teams { get; init; }
    public DbSet<DisciplineModel> Disciplines { get; init; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.Entity<UserModel>().ToTable(nameof(Users));
        modelBuilder.Entity<ClassModel>().ToTable(nameof(Classes));
        modelBuilder.Entity<TeamModel>().ToTable(nameof(Teams));
        modelBuilder.Entity<DisciplineModel>().ToTable(nameof(Disciplines));

        modelBuilder.Entity<Entity<Guid>>()
                    .Property(e => e.CreatedAt)
                    .ValueGeneratedOnAdd();

        modelBuilder.Entity<Entity<Guid>>()
                    .Property(e => e.UpdatedAt)
                    .ValueGeneratedOnAddOrUpdate();
    }
}