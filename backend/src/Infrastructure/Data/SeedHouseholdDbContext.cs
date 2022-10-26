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

            Guid AuthUser2Guid = new Guid("286c4279-bce5-4dbd-830a-10d2aab95ecd");
            Guid AuthUser3Guid = new Guid("e4d3ffd4-efa2-43ae-b305-4ed137c91cda");
            Guid AuthUser4Guid = new Guid("a1ed58f7-f109-42fb-9e80-ba1b76e3e808");
            Guid AuthUser5Guid = new Guid("4680a101-1e31-42ec-b376-48493ee9123b");
            Guid AuthUser6Guid = new Guid("1f194b9c-0c2a-4c2b-9743-c2d746e97c6b");

            Guid Pause1Guid = new Guid("A0000000-0000-0000-0000-000000000001");
            Guid Pause2Guid = new Guid("A0000000-0000-0000-0000-000000000002");
            Guid Pause3Guid = new Guid("A0000000-0000-0000-0000-000000000003");
            Guid Pause4Guid = new Guid("A0000000-0000-0000-0000-000000000004");


            Guid Profile1Guid = new Guid("B0000000-0000-0000-0000-000000000001");
            Guid Profile2Guid = new Guid("B0000000-0000-0000-0000-000000000002");

            Guid Profile3Guid = new Guid("B0000000-0000-0000-0000-000000000003");
            Guid Profile4Guid = new Guid("B0000000-0000-0000-0000-000000000004");
            Guid Profile5Guid = new Guid("B0000000-0000-0000-0000-000000000005");
            Guid Profile6Guid = new Guid("B0000000-0000-0000-0000-000000000006");
            Guid Profile7Guid = new Guid("B0000000-0000-0000-0000-000000000007");


            Guid Household1Guid = new Guid("C0000000-0000-0000-0000-000000000001");
            Guid Household2Guid = new Guid("C0000000-0000-0000-0000-000000000002");

            Guid Household3Guid = new Guid("C0000000-0000-0000-0000-000000000003");

            Guid Chore1Guid = new Guid("D0000000-0000-0000-0000-000000000001");
            Guid Chore2Guid = new Guid("D0000000-0000-0000-0000-000000000002");
            Guid Chore3Guid = new Guid("D0000000-0000-0000-0000-000000000003");
            Guid Chore4Guid = new Guid("D0000000-0000-0000-0000-000000000004");

            Guid Chore5Guid = new Guid("D0000000-0000-0000-0000-000000000005");
            Guid Chore6Guid = new Guid("D0000000-0000-0000-0000-000000000006");
            Guid Chore7Guid = new Guid("D0000000-0000-0000-0000-000000000007");
            Guid Chore8Guid = new Guid("D0000000-0000-0000-0000-000000000008");

            Guid ChoreCompleted1 = new Guid("E0000000-0000-0000-0000-000000000001");
            Guid ChoreCompleted2 = new Guid("E0000000-0000-0000-0000-000000000002");
            Guid ChoreCompleted3 = new Guid("E0000000-0000-0000-0000-000000000003");


            if (!await context.Household.AnyAsync())
            {
                Profile JanBannanHemma = new Profile() { Id = Profile1Guid, Alias = "JanBannanHemma", Avatar = "", IsAdmin = false, PendingRequest = false, AuthUserId = AuthUser1Guid, HouseholdId = Household1Guid };
                Profile JanBannanHosMorsan = new Profile() { Id = Profile2Guid, Alias = "JanBannanHosMorsan", Avatar = "", IsAdmin = false, PendingRequest = false, AuthUserId = AuthUser1Guid, HouseholdId = Household2Guid };

                Profile Banjo = new Profile() { Id = Profile3Guid, Alias = "Banjo", Avatar = "frog", IsAdmin = true, PendingRequest = false, AuthUserId = AuthUser2Guid, HouseholdId = Household3Guid };
                Profile Tuco = new Profile() { Id = Profile4Guid, Alias = "Tuco", Avatar = "fox", IsAdmin = true, PendingRequest = false, AuthUserId = AuthUser3Guid, HouseholdId = Household3Guid };
                Profile Maylee = new Profile() { Id = Profile5Guid, Alias = "Maylee", Avatar = "unicorn", IsAdmin = true, PendingRequest = false, AuthUserId = AuthUser4Guid, HouseholdId = Household3Guid };
                Profile Alann = new Profile() { Id = Profile6Guid, Alias = "Alann", Avatar = "owl", IsAdmin = true, PendingRequest = false, AuthUserId = AuthUser5Guid, HouseholdId = Household3Guid };
                Profile Jimmy = new Profile() { Id = Profile7Guid, Alias = "Jimmy", Avatar = "dolphin", IsAdmin = true, PendingRequest = false, AuthUserId = AuthUser6Guid, HouseholdId = Household3Guid };

                await context.Profile.AddAsync(JanBannanHemma);
                await context.Profile.AddAsync(JanBannanHosMorsan);

                await context.Profile.AddAsync(Banjo);
                await context.Profile.AddAsync(Tuco);
                await context.Profile.AddAsync(Maylee);
                await context.Profile.AddAsync(Alann);
                await context.Profile.AddAsync(Jimmy);

                Pause pause1 = new Pause() { Id = Pause1Guid, StartDate = new DateTime(2021, 1, 1), EndDate = new DateTime(2021, 1, 2), ProfileIdQol = Profile7Guid, HouseholdId = Household3Guid };
                Pause pause2 = new Pause() { Id = Pause2Guid, StartDate = new DateTime(2021, 1, 3), EndDate = new DateTime(2021, 1, 4), ProfileIdQol = Profile7Guid, HouseholdId = Household3Guid };
                Pause pause3 = new Pause() { Id = Pause3Guid, StartDate = new DateTime(2021, 1, 15), EndDate = new DateTime(2021, 1, 20), ProfileIdQol = Profile7Guid, HouseholdId = Household3Guid };
                Pause pause4 = new Pause() { Id = Pause4Guid, StartDate = new DateTime(2021, 1, 15), EndDate = new DateTime(2021, 1, 20), ProfileIdQol = Profile6Guid, HouseholdId = Household3Guid };
                List<Pause> pauses = new List<Pause>();
                pauses.Add(pause1);
                pauses.Add(pause2);
                pauses.Add(pause3);
                pauses.Add(pause4);
                await context.Pause.AddRangeAsync(pauses);

                //await context.Household.AddRangeAsync(GetPreconfiguredHouseholds());
                Household SunkigaStudentHålet = new Household() { Id = Household1Guid, Name = "SunkigaStudentHålet", Code = "666" };
                Household OrdnignORedaHuset = new Household() { Id = Household2Guid, Name = "OrdnignORedaHuset", Code = "1234" };

                Household KodStugan = new Household() { Id = Household3Guid, Name = "KodStugan", Code = "1234" };

                await context.Household.AddAsync(SunkigaStudentHålet);
                await context.Household.AddAsync(OrdnignORedaHuset);
                await context.Household.AddAsync(KodStugan);

                Chore chore1 = new Chore() { Id = Chore1Guid, Name = "Sanera", Points = 10, Description = "Ta bort Mögel", PictureUrl = "", AudioUrl = "", Frequency = 0, IsArchived = false, HouseholdId = Household1Guid };
                Chore chore2 = new Chore() { Id = Chore2Guid, Name = "Torka", Points = 4, Description = "Torka Bokhyllan", PictureUrl = "", AudioUrl = "", Frequency = 7, IsArchived = false, HouseholdId = Household2Guid };
                Chore chore3 = new Chore() { Id = Chore3Guid, Name = "Skrapa", Points = 12, Description = "Skrapa stenläggning", PictureUrl = "", AudioUrl = "", Frequency = 21, IsArchived = false, HouseholdId = Household2Guid };
                Chore chore4 = new Chore() { Id = Chore4Guid, Name = "Tvätta bilen", Points = 8, Description = "Tvätta bilen utvänding ocn invändigt", PictureUrl = "", AudioUrl = "", Frequency = 14, IsArchived = false, HouseholdId = Household2Guid };

                Chore chore5 = new Chore() { Id = Chore5Guid, Name = "Sanera", Points = 10, Description = "Ta bort Mögel", PictureUrl = "", AudioUrl = "", Frequency = 0, IsArchived = false, HouseholdId = Household3Guid };
                Chore chore6 = new Chore() { Id = Chore6Guid, Name = "Torka", Points = 4, Description = "Torka Bokhyllan", PictureUrl = "", AudioUrl = "", Frequency = 7, IsArchived = false, HouseholdId = Household3Guid };
                Chore chore7 = new Chore() { Id = Chore7Guid, Name = "Skrapa", Points = 12, Description = "Skrapa stenläggning", PictureUrl = "", AudioUrl = "", Frequency = 21, IsArchived = false, HouseholdId = Household3Guid };
                Chore chore8 = new Chore() { Id = Chore8Guid, Name = "Tvätta bilen", Points = 8, Description = "Tvätta bilen utvänding ocn invändigt", PictureUrl = "", AudioUrl = "", Frequency = 14, IsArchived = false, HouseholdId = Household3Guid };
                List<Chore> chores = new List<Chore>();
                List<Chore> chores2 = new List<Chore>();
                chores2.Add(chore5);
                chores2.Add(chore6);
                chores2.Add(chore7);
                chores2.Add(chore8);
                chores.Add(chore1);
                chores.Add(chore2);
                chores.Add(chore3);
                chores.Add(chore4);
                await context.Chore.AddRangeAsync(chores);
                await context.Chore.AddRangeAsync(chores2);

                ChoreCompleted choreCompleted1 = new ChoreCompleted() { Id = ChoreCompleted1, CompletedAt = new DateTime(2021, 1, 1), ProfileIdQol = Profile1Guid, ChoreId = Chore4Guid, HouseholdId = Household1Guid };
                ChoreCompleted choreCompleted2 = new ChoreCompleted() { Id = ChoreCompleted2, CompletedAt = new DateTime(2021, 1, 14), ProfileIdQol = Profile1Guid, ChoreId = Chore4Guid, HouseholdId = Household1Guid };
                ChoreCompleted choreCompleted3 = new ChoreCompleted() { Id = ChoreCompleted3, CompletedAt = new DateTime(2021, 1, 15), ProfileIdQol = Profile1Guid, ChoreId = Chore4Guid, HouseholdId = Household1Guid };
                List<ChoreCompleted> choresCompleted = new List<ChoreCompleted>();
                choresCompleted.Add(choreCompleted1);
                choresCompleted.Add(choreCompleted2);
                choresCompleted.Add(choreCompleted3);
                var random = new Random();
                List<Guid> profileIds = new List<Guid> { Profile3Guid, Profile4Guid, Profile5Guid, Profile6Guid, Profile7Guid };

                for (int i = 0; i < 300; i++)
                {
                    choresCompleted.Add(new ChoreCompleted() { Id = new Guid(), CompletedAt = new DateTime(2022, random.Next(1, 12), random.Next(1, 28)), ProfileIdQol = profileIds[random.Next(0, profileIds.Count)], ChoreId = chores2[random.Next(0, chores.Count)].Id, HouseholdId = Household3Guid });
                }
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
