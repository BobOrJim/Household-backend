using Core.Entities;
using Microsoft.EntityFrameworkCore;



//Add-Migration "V01" -context HouseholdDbContext
//update-database -context HouseholdDbContext
//Script-Migration 0 V01 -context HouseholdDbContext //OBS:Skall stå i Entry point projektet.

namespace Infrastructure.Data;

public class HouseholdDbContext : DbContext
{
    //private static readonly string azureConnectionString = @"Server=tcp:puppy.database.windows.net,1433;Initial Catalog=PuppyDb;Persist Security Info=False;User ID=puppy2022;Password=passwordA12;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
    public HouseholdDbContext(DbContextOptions<HouseholdDbContext> options) : base(options)
    {
        //Database.EnsureCreated(); //Behövs inte om jag kör update-database
    }

    public DbSet<Household> Household { get; set; }
    public DbSet<Pause> Pause { get; set; }
    public DbSet<Profile> Profile { get; set; }
    public DbSet<Chore> Chore { get; set; }
    public DbSet<ChoreCompleted> ChoreCompleted { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlServer(azureConnectionString); //When using Azure Db version
        // optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=HouseholdDb;Trusted_Connection=True;"); //When using local Db version
        optionsBuilder.UseInMemoryDatabase("inMemory"); //When using inMemory
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

}

