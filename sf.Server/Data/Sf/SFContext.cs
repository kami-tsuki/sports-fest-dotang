namespace sf.Server.Data.Sf;

public class SfContext(DbContextOptions<SfContext> options)
    : DbContext(options)
{
    public new DbSet<User> Users { get; init; }
    public DbSet<Class> Classes { get; init; }
    public DbSet<School> Schools { get; init; }
    public DbSet<Discipline> Disciplines { get; init; }
    public DbSet<Entry> Entries { get; init; }
    public DbSet<Location> Locations { get; init; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().ToTable(nameof(Users));
        modelBuilder.Entity<Class>().ToTable(nameof(Classes));
        modelBuilder.Entity<School>().ToTable(nameof(Schools));
        modelBuilder.Entity<Discipline>().ToTable(nameof(Disciplines));
        modelBuilder.Entity<Entry>().ToTable(nameof(Entries));
        modelBuilder.Entity<Location>().ToTable(nameof(Locations));


        modelBuilder.Entity<User>()
                    .HasDiscriminator<string>(nameof(User.RoleString))
                    .HasValue<User>(RoleType.User.ToString())
                    .HasValue<Student>(RoleType.Student.ToString())
                    .HasValue<Tutor>(RoleType.Tutor.ToString())
                    .HasValue<CampaignManager>(RoleType.CampaignManager.ToString())
                    .HasValue<CampaignJudge>(RoleType.CampaignJudge.ToString());

        modelBuilder.Entity<Entry>()
                    .HasKey(e => e.Id);

        // School 1:1 CampaignManager
        modelBuilder.Entity<School>()
                    .HasOne(s => s.Manager)
                    .WithOne()
                    .HasForeignKey<School>(s => s.ManagerId);

        // School m:m Class
        modelBuilder.Entity<School>()
                    .HasMany(s => s.Classes)
                    .WithOne(c => c.School)
                    .HasForeignKey(c => c.SchoolId);

        // Class m:m Student
        modelBuilder.Entity<Class>()
                    .HasMany(c => c.Students)
                    .WithOne(s => s.Class)
                    .HasForeignKey(s => s.ClassId);

        // School m:m Tutor
        modelBuilder.Entity<School>()
                    .HasMany(s => s.Tutors)
                    .WithOne(t => t.School)
                    .HasForeignKey(t => t.SchoolId);

        //School m:m CampaignJudge
        modelBuilder.Entity<School>()
                    .HasMany(s => s.Judges)
                    .WithOne(j => j.School)
                    .HasForeignKey(j => j.SchoolId);

        // Class 1:1 Tutor
        modelBuilder.Entity<Class>()
                    .HasOne(c => c.Tutor)
                    .WithOne(t => t.Class)
                    .HasForeignKey<Class>(c => c.TutorId);

        // Class 1:m Location
        modelBuilder.Entity<Class>()
                    .HasOne(c => c.Room)
                    .WithMany(l => l.Classes)
                    .HasForeignKey(c => c.RoomId);

        // Discipline m:m Location
        modelBuilder.Entity<Discipline>()
                    .HasOne(d => d.Location)
                    .WithMany(l => l.Disciplines)
                    .HasForeignKey(d => d.LocationId);

        // Discipline 1:1 CampaignJudge
        modelBuilder.Entity<Discipline>()
                    .HasOne(d => d.Judge)
                    .WithOne()
                    .HasForeignKey<Discipline>(d => d.JudgeId);

        // Student m:m Entry
        modelBuilder.Entity<Student>()
                    .HasMany(s => s.Entries)
                    .WithOne(e => e.Student)
                    .HasForeignKey(e => e.StudentId);

        // Discipline m:m Entry
        modelBuilder.Entity<Discipline>()
                    .HasMany(d => d.Entries)
                    .WithOne(e => e.Discipline)
                    .HasForeignKey(e => e.DisciplineId);

        // Create timestamp
        modelBuilder.Entity<Entity<Guid>>()
                    .Property(e => e.CreatedAt)
                    .ValueGeneratedOnAdd();

        // Update timestamp
        modelBuilder.Entity<Entity<Guid>>()
                    .Property(e => e.UpdatedAt)
                    .ValueGeneratedOnAddOrUpdate();
    }
}