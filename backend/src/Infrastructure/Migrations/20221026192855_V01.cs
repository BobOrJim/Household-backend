using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class V01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Household",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Household", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chore",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AudioUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    HouseholdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chore_Household_HouseholdId",
                        column: x => x.HouseholdId,
                        principalTable: "Household",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pause",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HouseholdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileIdQol = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pause", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pause_Household_HouseholdId",
                        column: x => x.HouseholdId,
                        principalTable: "Household",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    PendingRequest = table.Column<bool>(type: "bit", nullable: false),
                    AuthUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HouseholdId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profile_Household_HouseholdId",
                        column: x => x.HouseholdId,
                        principalTable: "Household",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChoreCompleted",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProfileIdQol = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ChoreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    HouseholdId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoreCompleted", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChoreCompleted_Chore_ChoreId",
                        column: x => x.ChoreId,
                        principalTable: "Chore",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChoreCompleted_Household_HouseholdId",
                        column: x => x.HouseholdId,
                        principalTable: "Household",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chore_HouseholdId",
                table: "Chore",
                column: "HouseholdId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoreCompleted_ChoreId",
                table: "ChoreCompleted",
                column: "ChoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ChoreCompleted_HouseholdId",
                table: "ChoreCompleted",
                column: "HouseholdId");

            migrationBuilder.CreateIndex(
                name: "IX_Pause_HouseholdId",
                table: "Pause",
                column: "HouseholdId");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_HouseholdId",
                table: "Profile",
                column: "HouseholdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChoreCompleted");

            migrationBuilder.DropTable(
                name: "Pause");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "Chore");

            migrationBuilder.DropTable(
                name: "Household");
        }
    }
}
