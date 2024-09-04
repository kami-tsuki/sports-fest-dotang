using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sf.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Entity<Guid>",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity<Guid>", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShortName = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comment = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Entity<Guid>_Id",
                        column: x => x.Id,
                        principalTable: "Entity<Guid>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShortName = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comment = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TutorId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    RoomId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    SchoolId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LocationId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_Entity<Guid>_Id",
                        column: x => x.Id,
                        principalTable: "Entity<Guid>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Classes_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Classes_Locations_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Disciplines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShortName = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comment = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    JudgeId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    LocationId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disciplines_Entity<Guid>_Id",
                        column: x => x.Id,
                        principalTable: "Entity<Guid>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Disciplines_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DisciplineStudent",
                columns: table => new
                {
                    DisciplinesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplineStudent", x => new { x.DisciplinesId, x.StudentsId });
                    table.ForeignKey(
                        name: "FK_DisciplineStudent_Disciplines_DisciplinesId",
                        column: x => x.DisciplinesId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Entries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StudentId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    DisciplineId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Score = table.Column<double>(type: "double", nullable: false),
                    Comment = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", rowVersion: true, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entries_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShortName = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comment = table.Column<string>(type: "varchar(1024)", maxLength: 1024, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ManagerId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schools_Entity<Guid>_Id",
                        column: x => x.Id,
                        principalTable: "Entity<Guid>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FirstName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RoleString = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ApiKey = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", rowVersion: true, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", rowVersion: true, nullable: false),
                    DisciplineId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    SchoolId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    CampaignManager_SchoolId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    ClassId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Student_SchoolId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Tutor_ClassId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    Tutor_SchoolId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                    UserName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedUserName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NormalizedEmail = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Disciplines_DisciplineId",
                        column: x => x.DisciplineId,
                        principalTable: "Disciplines",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Users_Schools_CampaignManager_SchoolId",
                        column: x => x.CampaignManager_SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Schools_Student_SchoolId",
                        column: x => x.Student_SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Schools_Tutor_SchoolId",
                        column: x => x.Tutor_SchoolId,
                        principalTable: "Schools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_LocationId",
                table: "Classes",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_RoomId",
                table: "Classes",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SchoolId",
                table: "Classes",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_ShortName",
                table: "Classes",
                column: "ShortName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_TutorId",
                table: "Classes",
                column: "TutorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_JudgeId",
                table: "Disciplines",
                column: "JudgeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_LocationId",
                table: "Disciplines",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Disciplines_ShortName",
                table: "Disciplines",
                column: "ShortName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DisciplineStudent_StudentsId",
                table: "DisciplineStudent",
                column: "StudentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_DisciplineId",
                table: "Entries",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Entries_StudentId",
                table: "Entries",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ShortName",
                table: "Locations",
                column: "ShortName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schools_ManagerId",
                table: "Schools",
                column: "ManagerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CampaignManager_SchoolId",
                table: "Users",
                column: "CampaignManager_SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ClassId",
                table: "Users",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DisciplineId",
                table: "Users",
                column: "DisciplineId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SchoolId",
                table: "Users",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Student_SchoolId",
                table: "Users",
                column: "Student_SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Tutor_SchoolId",
                table: "Users",
                column: "Tutor_SchoolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Schools_SchoolId",
                table: "Classes",
                column: "SchoolId",
                principalTable: "Schools",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Users_TutorId",
                table: "Classes",
                column: "TutorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Disciplines_Users_JudgeId",
                table: "Disciplines",
                column: "JudgeId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DisciplineStudent_Users_StudentsId",
                table: "DisciplineStudent",
                column: "StudentsId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entries_Users_StudentId",
                table: "Entries",
                column: "StudentId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_Users_ManagerId",
                table: "Schools",
                column: "ManagerId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Entity<Guid>_Id",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Disciplines_Entity<Guid>_Id",
                table: "Disciplines");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Entity<Guid>_Id",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_Schools_Entity<Guid>_Id",
                table: "Schools");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Locations_LocationId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Locations_RoomId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Disciplines_Locations_LocationId",
                table: "Disciplines");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Schools_SchoolId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Schools_CampaignManager_SchoolId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Schools_SchoolId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Schools_Student_SchoolId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Schools_Tutor_SchoolId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Users_TutorId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Disciplines_Users_JudgeId",
                table: "Disciplines");

            migrationBuilder.DropTable(
                name: "DisciplineStudent");

            migrationBuilder.DropTable(
                name: "Entries");

            migrationBuilder.DropTable(
                name: "Entity<Guid>");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Schools");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Disciplines");
        }
    }
}
