using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeFit.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TargetMuscle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Equipment = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutSessions", x => x.Id);
                    table.CheckConstraint("CK_WorkoutSession_Dates", "[EndDate] >= [StartDate]");
                    table.ForeignKey(
                        name: "FK_WorkoutSessions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Goals = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PreferredDay = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutTemplates_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutSessionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkoutSessionId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Sets = table.Column<int>(type: "int", nullable: true),
                    Repetitions = table.Column<int>(type: "int", nullable: true),
                    Weight = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true),
                    RestTimeInSeconds = table.Column<int>(type: "int", nullable: false),
                    Tempo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    DurationInSeconds = table.Column<int>(type: "int", nullable: true),
                    Distance = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutSessionDetails", x => x.Id);
                    table.CheckConstraint("CK_WorkoutSessionDetails_Distance", "[Distance] IS NULL OR [Distance] >= 0");
                    table.CheckConstraint("CK_WorkoutSessionDetails_DurationInSeconds", "[DurationInSeconds] IS NULL OR [DurationInSeconds] >= 0");
                    table.CheckConstraint("CK_WorkoutSessionDetails_Repetitions", "[Repetitions] IS NULL OR [Repetitions] > 0");
                    table.CheckConstraint("CK_WorkoutSessionDetails_RestTimeInSeconds", "[RestTimeInSeconds] >= 0");
                    table.CheckConstraint("CK_WorkoutSessionDetails_Sets", "[Sets] IS NULL OR [Sets] > 0");
                    table.CheckConstraint("CK_WorkoutSessionDetails_Weight", "[Weight] IS NULL OR [Weight] >= 0");
                    table.ForeignKey(
                        name: "FK_WorkoutSessionDetails_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutSessionDetails_WorkoutSessions_WorkoutSessionId",
                        column: x => x.WorkoutSessionId,
                        principalTable: "WorkoutSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutTemplateExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkoutTemplateId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    TargetSets = table.Column<int>(type: "int", nullable: true),
                    TargetReps = table.Column<int>(type: "int", nullable: true),
                    TargetWeight = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true),
                    RestTimeInSeconds = table.Column<int>(type: "int", nullable: false),
                    Tempo = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    TargetDistance = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: true),
                    TargetDurationInSeconds = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutTemplateExercises", x => x.Id);
                    table.CheckConstraint("CK_WorkoutTemplateExercise_Order_Positive", "[Order] > 0");
                    table.CheckConstraint("CK_WorkoutTemplateExercise_RestTimeInSeconds_NonNegative", "[RestTimeInSeconds] >= 0");
                    table.CheckConstraint("CK_WorkoutTemplateExercise_TargetDistance_NonNegative", "[TargetDistance] IS NULL OR [TargetDistance] >= 0");
                    table.CheckConstraint("CK_WorkoutTemplateExercise_TargetDurationInSeconds_NonNegative", "[TargetDurationInSeconds] IS NULL OR [TargetDurationInSeconds] >= 0");
                    table.CheckConstraint("CK_WorkoutTemplateExercise_TargetReps_NonNegative", "[TargetReps] IS NULL OR [TargetReps] >= 0");
                    table.CheckConstraint("CK_WorkoutTemplateExercise_TargetSets_NonNegative", "[TargetSets] IS NULL OR [TargetSets] >= 0");
                    table.CheckConstraint("CK_WorkoutTemplateExercise_TargetWeight_NonNegative", "[TargetWeight] IS NULL OR [TargetWeight] >= 0");
                    table.ForeignKey(
                        name: "FK_WorkoutTemplateExercises_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutTemplateExercises_WorkoutTemplates_WorkoutTemplateId",
                        column: x => x.WorkoutTemplateId,
                        principalTable: "WorkoutTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessionDetails_ExerciseId",
                table: "WorkoutSessionDetails",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessionDetails_WorkoutSessionId",
                table: "WorkoutSessionDetails",
                column: "WorkoutSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessions_UserId",
                table: "WorkoutSessions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutTemplateExercises_ExerciseId",
                table: "WorkoutTemplateExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutTemplateExercises_WorkoutTemplateId_Order",
                table: "WorkoutTemplateExercises",
                columns: new[] { "WorkoutTemplateId", "Order" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutTemplates_UserId",
                table: "WorkoutTemplates",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutSessionDetails");

            migrationBuilder.DropTable(
                name: "WorkoutTemplateExercises");

            migrationBuilder.DropTable(
                name: "WorkoutSessions");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "WorkoutTemplates");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");
        }
    }
}
