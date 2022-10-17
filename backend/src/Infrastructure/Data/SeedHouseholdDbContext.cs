using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;




namespace Infrastructure.Data
{
    public class SeedHouseholdDbContext
    {

        public static async Task SeedAsync(HouseholdDbContext context)
        {
            //remove database
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

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


            if (!await context.Household.AnyAsync())
            {
                Profile JanBannanHemma = new Profile() { Id = Profile1Guid, Alias = "JanBannanHemma", Avatar = "", Color = "", IsAdmin = false, PendingRequest = false, AuthUserId = AuthUser1Guid, HouseholdId = Household1Guid };
                Profile JanBannanHosMorsan = new Profile() { Id = Profile2Guid, Alias = "JanBannanHosMorsan", Avatar = "", Color = "", IsAdmin = false, PendingRequest = false, AuthUserId = AuthUser1Guid, HouseholdId = Household2Guid };
                await context.Profile.AddAsync(JanBannanHemma);
                await context.Profile.AddAsync(JanBannanHosMorsan);

                Pause pause1 = new Pause() { Id = Pause1Guid, StartDate = new DateTime(2021, 1, 1), EndDate = new DateTime(2021, 1, 2), ProfileIdQol = Profile1Guid, HouseholdId = Household2Guid };
                Pause pause2 = new Pause() { Id = Pause2Guid, StartDate = new DateTime(2021, 1, 3), EndDate = new DateTime(2021, 1, 4), ProfileIdQol = Profile1Guid, HouseholdId = Household2Guid };
                Pause pause3 = new Pause() { Id = Pause3Guid, StartDate = new DateTime(2021, 1, 15), EndDate = new DateTime(2021, 1, 20), ProfileIdQol = Profile1Guid, HouseholdId = Household2Guid };
                List<Pause> pauses = new List<Pause>();
                pauses.Add(pause1);
                pauses.Add(pause2);
                pauses.Add(pause3);
                await context.Pause.AddRangeAsync(pauses);

                //await context.Household.AddRangeAsync(GetPreconfiguredHouseholds());
                Household SunkigaStudentHålet = new Household() { Id = Household1Guid, Name = "SunkigaStudentHålet", Code = "666" };
                Household OrdnignORedaHuset = new Household() { Id = Household2Guid, Name = "OrdnignORedaHuset", Code = "1234" };
                await context.Household.AddAsync(SunkigaStudentHålet);
                await context.Household.AddAsync(OrdnignORedaHuset);

                Chore chore1 = new Chore() { Id = Chore1Guid, Name = "Sanera", Points = 10, Description = "Ta bort Mögel", PictureUrl = "", AudioUrl = "", Frequency = 0, IsArchived = false, HouseholdId = Household1Guid };
                Chore chore2 = new Chore() { Id = Chore2Guid, Name = "Torka", Points = 4, Description = "Torka Bokhyllan", PictureUrl = "", AudioUrl = "", Frequency = 7, IsArchived = false, HouseholdId = Household2Guid };
                Chore chore3 = new Chore() { Id = Chore3Guid, Name = "Skrapa", Points = 12, Description = "Skrapa stenläggning", PictureUrl = "", AudioUrl = "", Frequency = 21, IsArchived = false, HouseholdId = Household2Guid };
                Chore chore4 = new Chore() { Id = Chore4Guid, Name = "Tvätta bilen", Points = 8, Description = "Tvätta bilen utvänding ocn invändigt", PictureUrl = "", AudioUrl = "", Frequency = 14, IsArchived = false, HouseholdId = Household2Guid };
                List<Chore> chores = new List<Chore>();
                chores.Add(chore1);
                chores.Add(chore2);
                chores.Add(chore3);
                chores.Add(chore4);
                await context.Chore.AddRangeAsync(chores);

                ChoreCompleted choreCompleted1 = new ChoreCompleted() { Id = ChoreCompleted1, CompletedAt = new DateTime(2021, 1, 1), ProfileIdQol = Profile1Guid, ChoreId = Chore4Guid, HouseholdId = Household1Guid };
                ChoreCompleted choreCompleted2 = new ChoreCompleted() { Id = ChoreCompleted2, CompletedAt = new DateTime(2021, 1, 14), ProfileIdQol = Profile1Guid, ChoreId = Chore4Guid, HouseholdId = Household1Guid };
                ChoreCompleted choreCompleted3 = new ChoreCompleted() { Id = ChoreCompleted3, CompletedAt = new DateTime(2021, 1, 15), ProfileIdQol = Profile1Guid, ChoreId = Chore4Guid, HouseholdId = Household1Guid };
                List<ChoreCompleted> choresCompleted = new List<ChoreCompleted>();
                choresCompleted.Add(choreCompleted1);
                choresCompleted.Add(choreCompleted2);
                choresCompleted.Add(choreCompleted3);
                await context.ChoreCompleted.AddRangeAsync(choresCompleted);





                Debug.WriteLine("Attempting seed save...");
                await context.SaveChangesAsync();
                Debug.WriteLine("Succeeded(?)");
            }
        }

        static IEnumerable<Household> GetPreconfiguredHouseholds()
        {
            return new List<Household>() {
                new Household() { Name = "Household 1", Code ="myCode" },
            };

        }
    }
}
