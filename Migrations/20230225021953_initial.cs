using Microsoft.EntityFrameworkCore.Migrations;

namespace Mission8_2_5.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    categoryName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskName = table.Column<string>(nullable: false),
                    dueDate = table.Column<string>(nullable: true),
                    quadrant = table.Column<int>(nullable: false),
                    completed = table.Column<bool>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_tasks_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "CategoryId", "categoryName" },
                values: new object[] { 1, "Home" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "CategoryId", "categoryName" },
                values: new object[] { 2, "School" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "CategoryId", "categoryName" },
                values: new object[] { 3, "Work" });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "CategoryId", "categoryName" },
                values: new object[] { 4, "Church" });

            migrationBuilder.InsertData(
                table: "tasks",
                columns: new[] { "TaskId", "CategoryId", "TaskName", "completed", "dueDate", "quadrant" },
                values: new object[] { 1, 1, "Apply for internships", false, "2023-01-01", 3 });

            migrationBuilder.InsertData(
                table: "tasks",
                columns: new[] { "TaskId", "CategoryId", "TaskName", "completed", "dueDate", "quadrant" },
                values: new object[] { 2, 2, "Finish Mission 8", false, "2023-02-24", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_tasks_CategoryId",
                table: "tasks",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "categories");
        }
    }
}
