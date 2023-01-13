using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aplikacja.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    System = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Engineer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ecm = table.Column<int>(type: "int", nullable: true),
                    Gpdm = table.Column<int>(type: "int", nullable: true),
                    ProjectNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Received = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Started = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Finished = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenComplete = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                });

            migrationBuilder.CreateTable(
                name: "Raports",
                columns: table => new
                {
                    RaportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalHours = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raports", x => x.RaportId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CCtr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActTyp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValue: "User"),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Cats",
                columns: table => new
                {
                    CatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatCreated = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cats", x => x.CatId);
                    table.ForeignKey(
                        name: "FK_Cats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inboxs",
                columns: table => new
                {
                    InboxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inboxs", x => x.InboxId);
                    table.ForeignKey(
                        name: "FK_Inboxs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRaports",
                columns: table => new
                {
                    UserRaportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserAllHours = table.Column<double>(type: "float", nullable: false),
                    RaportId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRaports", x => x.UserRaportId);
                    table.ForeignKey(
                        name: "FK_UserRaports_Raports_RaportId",
                        column: x => x.RaportId,
                        principalTable: "Raports",
                        principalColumn: "RaportId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRaports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InboxItems",
                columns: table => new
                {
                    InboxItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    Components = table.Column<int>(type: "int", nullable: false),
                    DrawingsComponents = table.Column<int>(type: "int", nullable: false),
                    DrawingsAssembly = table.Column<int>(type: "int", nullable: false),
                    WhenComplete = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    InboxId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboxItems", x => x.InboxItemId);
                    table.ForeignKey(
                        name: "FK_InboxItems_Inboxs_InboxId",
                        column: x => x.InboxId,
                        principalTable: "Inboxs",
                        principalColumn: "InboxId");
                    table.ForeignKey(
                        name: "FK_InboxItems_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatRecords",
                columns: table => new
                {
                    CatRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Receiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SapText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InboxItemId = table.Column<int>(type: "int", nullable: false),
                    CatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatRecords", x => x.CatRecordId);
                    table.ForeignKey(
                        name: "FK_CatRecords_Cats_CatId",
                        column: x => x.CatId,
                        principalTable: "Cats",
                        principalColumn: "CatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatRecords_InboxItems_InboxItemId",
                        column: x => x.InboxItemId,
                        principalTable: "InboxItems",
                        principalColumn: "InboxItemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRaportRecords",
                columns: table => new
                {
                    UserRaportRecordId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskHours = table.Column<double>(type: "float", nullable: false),
                    UserRaportId = table.Column<int>(type: "int", nullable: false),
                    InboxItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRaportRecords", x => x.UserRaportRecordId);
                    table.ForeignKey(
                        name: "FK_UserRaportRecords_InboxItems_InboxItemId",
                        column: x => x.InboxItemId,
                        principalTable: "InboxItems",
                        principalColumn: "InboxItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRaportRecords_UserRaports_UserRaportId",
                        column: x => x.UserRaportId,
                        principalTable: "UserRaports",
                        principalColumn: "UserRaportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CatRecordHourss",
                columns: table => new
                {
                    CatRecordHoursId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: true),
                    Hours = table.Column<double>(type: "float", nullable: false),
                    CatRecordId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatRecordHourss", x => x.CatRecordHoursId);
                    table.ForeignKey(
                        name: "FK_CatRecordHourss_CatRecords_CatRecordId",
                        column: x => x.CatRecordId,
                        principalTable: "CatRecords",
                        principalColumn: "CatRecordId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "JobId", "Client", "DueDate", "Ecm", "Engineer", "Finished", "Gpdm", "JobDescription", "Link", "ProjectName", "ProjectNumber", "Received", "Started", "Status", "System", "Type", "WhenComplete" },
                values: new object[,]
                {
                    { 1, "TOYOTA", null, 4561976, "Agata", null, 1, "Create Muffler", "linkt o task", "sap text", "LASDl", "15.22.2022", null, "2D", "Catia", "2D", null },
                    { 2, "TOYOTA", "25.11.2022", 4561976, "Agata", null, 1, "Create drawing", "linkt o task", "sap text", "LASDl", "20.11.2022", null, "2D", "Catia", "3D", null },
                    { 3, "TOYOTA", null, 4561976, "Agata", null, 1, "Update hangers", "linkt o task", "sap text", "LASDl", "20.11.2022", null, "2D", "Catia", "2D", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "ActTyp", "CCtr", "Email", "Name", "PasswordHash", "Photo", "Role" },
                values: new object[,]
                {
                    { 1, "L8531", "RS8531", "dawid@tlen.pl", "Dawid", "zxcasdqwe", "zdjecie", "Admin" },
                    { 2, "L8531", "RS8531", "agata@tlen.pl", "Agata", "zxcasdqwe", "zdjecie", "Manager" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatRecordHourss_CatRecordId",
                table: "CatRecordHourss",
                column: "CatRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_CatRecords_CatId",
                table: "CatRecords",
                column: "CatId");

            migrationBuilder.CreateIndex(
                name: "IX_CatRecords_InboxItemId",
                table: "CatRecords",
                column: "InboxItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Cats_UserId",
                table: "Cats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxItems_InboxId",
                table: "InboxItems",
                column: "InboxId");

            migrationBuilder.CreateIndex(
                name: "IX_InboxItems_JobId",
                table: "InboxItems",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inboxs_UserId",
                table: "Inboxs",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRaportRecords_InboxItemId",
                table: "UserRaportRecords",
                column: "InboxItemId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRaportRecords_UserRaportId",
                table: "UserRaportRecords",
                column: "UserRaportId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRaports_RaportId",
                table: "UserRaports",
                column: "RaportId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRaports_UserId",
                table: "UserRaports",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatRecordHourss");

            migrationBuilder.DropTable(
                name: "UserRaportRecords");

            migrationBuilder.DropTable(
                name: "CatRecords");

            migrationBuilder.DropTable(
                name: "UserRaports");

            migrationBuilder.DropTable(
                name: "Cats");

            migrationBuilder.DropTable(
                name: "InboxItems");

            migrationBuilder.DropTable(
                name: "Raports");

            migrationBuilder.DropTable(
                name: "Inboxs");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
