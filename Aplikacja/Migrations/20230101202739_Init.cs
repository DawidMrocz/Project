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
                    JobDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    System = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Engineer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ecm = table.Column<int>(type: "int", nullable: false),
                    Gpdm = table.Column<int>(type: "int", nullable: false),
                    ProjectNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Client = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Received = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DueDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Started = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Finished = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WhenComplete = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.JobId);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "JobId", "Client", "DueDate", "Ecm", "Engineer", "Finished", "Gpdm", "JobDescription", "Link", "ProjectName", "ProjectNumber", "Received", "Started", "Status", "System", "Type", "WhenComplete" },
                values: new object[,]
                {
                    { 1, "TOYOTA", null, 4561976, "Agata", null, 1, "Create drawing", "linkt o task", "sap text", "LASDl", "15.22.2022", null, "2D", "Catia", "2D", null },
                    { 2, "TOYOTA", "25.11.2022", 4561976, "Agata", null, 1, "Create drawing", "linkt o task", "sap text", "LASDl", "20.11.2022", null, "2D", "Catia", "2D", null },
                    { 3, "TOYOTA", null, 4561976, "Agata", null, 1, "Create drawing", "linkt o task", "sap text", "LASDl", "20.11.2022", null, "2D", "Catia", "2D", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
