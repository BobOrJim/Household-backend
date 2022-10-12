using Core.Entities;
using Microsoft.EntityFrameworkCore;


//In Package manager console
//Set startup project to only API
//Add-Migration "V01" -context HouseholdDbContext
//update-database -context HouseholdDbContext


namespace Infrastructure.Data;

public class HouseholdDbContext : DbContext
{
    //private static readonly string azureConnectionString = @"Server=tcp:puppy.database.windows.net,1433;Initial Catalog=PuppyDb;Persist Security Info=False;User ID=puppy2022;Password=passwordA12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    public HouseholdDbContext(DbContextOptions<HouseholdDbContext> options) : base(options)
    {
        //this.Database.EnsureCreated();
    }

    public DbSet<Household> Household { get; set; }
    public DbSet<Pause> Pause { get; set; }
    public DbSet<Profile> Profile { get; set; }
    public DbSet<Chore> Chore { get; set; }
    public DbSet<ChoreCompleted> ChoreCompleted { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer(azureConnectionString);
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=HouseholdDb;Trusted_Connection=True;");
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Guid AuthUser1Guid = new Guid("90000000-0000-0000-0000-000000000001");

        Guid Pause1Guid = new Guid("A0000000-0000-0000-0000-000000000001");
        Guid Pause2Guid = new Guid("A0000000-0000-0000-0000-000000000002");
        Guid Pause3Guid = new Guid("A0000000-0000-0000-0000-000000000003");

        Guid Profile1Guid = new Guid("B0000000-0000-0000-0000-000000000001");
        Guid Profile2Guid = new Guid("B0000000-0000-0000-0000-000000000002");

        Guid Household1Guid = new Guid("C0000000-0000-0000-0000-000000000001");
        Guid Household2Guid = new Guid("C0000000-0000-0000-0000-000000000002");
        
        Guid Chore1Guid = new Guid("D0000000-0000-0000-0000-000000000001");
        Guid Chore2Guid = new Guid("D0000000-0000-0000-0000-000000000002");
        Guid Chore3Guid = new Guid("D0000000-0000-0000-0000-000000000003");
        Guid Chore4Guid = new Guid("D0000000-0000-0000-0000-000000000004");

        Guid ChoreCompleted1 = new Guid("E0000000-0000-0000-0000-000000000001");
        Guid ChoreCompleted2 = new Guid("E0000000-0000-0000-0000-000000000002");
        Guid ChoreCompleted3 = new Guid("E0000000-0000-0000-0000-000000000003");
        
        /*
        modelBuilder.Entity<Pause>().HasData(new Pause() { Id = Pause1Guid, StartDate = new DateTime(2021, 1, 1), EndDate = new DateTime(2021, 1, 2), ProfileId = Profile1Guid });
        modelBuilder.Entity<Pause>().HasData(new Pause() { Id = Pause2Guid, StartDate = new DateTime(2021, 1, 4), EndDate = new DateTime(2021, 1, 6), ProfileId = Profile1Guid });
        modelBuilder.Entity<Pause>().HasData(new Pause() { Id = Pause3Guid, StartDate = new DateTime(2021, 1, 4), EndDate = new DateTime(2021, 1, 9), ProfileId = Profile2Guid });
        
        
        
        modelBuilder.Entity<Profile>().HasData(new Profile() { 
            Id = Profile1Guid, 
            Alias = "JanBannanHemma", 
            Avatar = "", 
            AvatarColor = "", 
            IsAdmin = false, 
            PendingRequest = false,
            AuthUserId = AuthUser1Guid, 
            HouseholdId = Household1Guid 
        });

        modelBuilder.Entity<Profile>().HasData(new Profile()
        {
            Id = Profile2Guid,
            Alias = "JanBannanHosMorsan",
            Avatar = "",
            AvatarColor = "",
            IsAdmin = false,
            PendingRequest = false,
            AuthUserId = AuthUser1Guid,
            HouseholdId = Household2Guid
        });
        modelBuilder.Entity<Household>().HasData(new Household()
        {
            Id = Household1Guid,
            Name = "SunkigaStudentHålet",
            Code = "666",
        });
        modelBuilder.Entity<Household>().HasData(new Household()
        {
            Id = Household2Guid,
            Name = "KristanVägen",
            Code = "1234",
        });

        modelBuilder.Entity<Chore>().HasData(new Chore()
        {
            Id = Chore1Guid,
            Name = "Sanera",
            Points = 10,
            Description = "Ta bort Mögel",
            PictureUrl = "",
            AudioUrl = "",
            Frequency = 0,
            IsArchived = false,
            HouseholdId = Household1Guid
        });

        modelBuilder.Entity<Chore>().HasData(new Chore()
        {
            Id = Chore2Guid,
            Name = "Torka",
            Points = 10,
            Description = "Torka Bokhyllan",
            PictureUrl = "",
            AudioUrl = "",
            Frequency = 7,
            IsArchived = false,
            HouseholdId = Household2Guid
        });

        modelBuilder.Entity<Chore>().HasData(new Chore()
        {
            Id = Chore3Guid,
            Name = "Skrapa",
            Points = 10,
            Description = "Skrapa stenläggning",
            PictureUrl = "",
            AudioUrl = "",
            Frequency = 21,
            IsArchived = false,
            HouseholdId = Household2Guid
        });

        modelBuilder.Entity<Chore>().HasData(new Chore()
        {
            Id = Chore4Guid,
            Name = "Tvätta bilen",
            Points = 10,
            Description = "Tvätta bilen utvänding ocn invändigt",
            PictureUrl = "",
            AudioUrl = "",
            Frequency = 14,
            IsArchived = false,
            HouseholdId = Household2Guid
        });

        modelBuilder.Entity<ChoreCompleted>().HasData(new ChoreCompleted()
        {
            Id = ChoreCompleted1,
            CompletedAt = new DateTime(2021, 1, 1),
            ProfileId = Profile1Guid,
            ChoreId = Chore4Guid
        });

        modelBuilder.Entity<ChoreCompleted>().HasData(new ChoreCompleted()
        {
            Id = ChoreCompleted2,
            CompletedAt = new DateTime(2021, 1, 14),
            ProfileId = Profile1Guid,
            ChoreId = Chore4Guid
        });
        
        modelBuilder.Entity<ChoreCompleted>().HasData(new ChoreCompleted()
        {
            Id = ChoreCompleted3,
            CompletedAt = new DateTime(2021, 2, 1),
            ProfileId = Profile1Guid,
            ChoreId = Chore4Guid
        });
        
        */

    }

}

