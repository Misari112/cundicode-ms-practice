using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ms_practice.Migrations
{
    /// <inheritdoc />
    public partial class m1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DifficultyLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categories = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Examples = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeLimit = table.Column<float>(type: "real", nullable: false),
                    MemoryLimit = table.Column<int>(type: "int", nullable: false),
                    FunctionSignature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolutionTemplate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hints = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TestCases = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Visibility = table.Column<bool>(type: "bit", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompleteExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProgrammingExerciseId = table.Column<int>(type: "int", nullable: false),
                    Script = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompleteExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompleteExercises_Exercises_ProgrammingExerciseId",
                        column: x => x.ProgrammingExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompleteExercises_ProgrammingExerciseId",
                table: "CompleteExercises",
                column: "ProgrammingExerciseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompleteExercises");

            migrationBuilder.DropTable(
                name: "Exercises");
        }
    }
}
