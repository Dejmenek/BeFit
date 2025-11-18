using BeFit.Models;
using BeFit.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeFit.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var adminRole = "Admin";
        if (!await roleManager.RoleExistsAsync(adminRole))
            await roleManager.CreateAsync(new IdentityRole(adminRole));

        var users = new[]
        {
            new { Email = "user1@example.com", Password = "Password123!", Role = "" },
            new { Email = "user2@example.com", Password = "Password123!", Role = "" },
            new { Email = "admin@example.com", Password = "Password123!", Role = adminRole }
        };

        var userIds = new List<string>();
        foreach (var u in users)
        {
            var user = await userManager.FindByEmailAsync(u.Email);
            if (user == null)
            {
                user = new ApplicationUser { UserName = u.Email, Email = u.Email, EmailConfirmed = true };
                await userManager.CreateAsync(user, u.Password);
            }
            if (!string.IsNullOrEmpty(u.Role) && !await userManager.IsInRoleAsync(user, u.Role))
                await userManager.AddToRoleAsync(user, u.Role);

            userIds.Add(user.Id);
        }

        if (!await db.Exercises.AnyAsync())
        {
            db.Exercises.AddRange(
                new Exercise { Name = "Push Up", Category = ExerciseType.Strength, TargetMuscle = MuscleType.Chest, Difficulty = Difficulty.Beginner, Instructions = "Start in plank position, lower body, push up." },
                new Exercise { Name = "Squat", Category = ExerciseType.Strength, TargetMuscle = MuscleType.Legs, Difficulty = Difficulty.Beginner, Instructions = "Stand, lower hips, return." },
                new Exercise { Name = "Plank", Category = ExerciseType.Strength, TargetMuscle = MuscleType.Core, Difficulty = Difficulty.Intermediate, Instructions = "Hold plank position." }
            );
            await db.SaveChangesAsync();
        }

        if (!await db.WorkoutTemplates.AnyAsync())
        {
            db.WorkoutTemplates.AddRange(
                new WorkoutTemplate { UserId = userIds[0], Name = "Beginner Strength", Description = "Simple strength workout.", Goals = "Build strength.", PreferredDay = DayOfWeek.Monday },
                new WorkoutTemplate { UserId = userIds[2], Name = "Core Focus", Description = "Core muscle workout.", Goals = "Improve core stability.", PreferredDay = DayOfWeek.Wednesday }
            );
            await db.SaveChangesAsync();
        }

        if (!await db.WorkoutTemplateExercises.AnyAsync())
        {
            db.WorkoutTemplateExercises.AddRange(
                new WorkoutTemplateExercise { WorkoutTemplateId = 1, ExerciseId = 1, Order = 1, TargetSets = 3, TargetReps = 10, RestTimeInSeconds = 60, Tempo = "2-1-2" },
                new WorkoutTemplateExercise { WorkoutTemplateId = 1, ExerciseId = 2, Order = 2, TargetSets = 3, TargetReps = 15, RestTimeInSeconds = 60, Tempo = "2-1-2" },
                new WorkoutTemplateExercise { WorkoutTemplateId = 2, ExerciseId = 3, Order = 1, TargetSets = 2, TargetDurationInSeconds = 60, RestTimeInSeconds = 90 }
            );
            await db.SaveChangesAsync();
        }

        if (!await db.WorkoutSessions.AnyAsync())
        {
            db.WorkoutSessions.AddRange(
                new WorkoutSession { UserId = userIds[0], StartDate = new DateTime(2025, 10, 30, 8, 0, 0), EndDate = new DateTime(2025, 10, 30, 8, 45, 0), Notes = "First session." },
                new WorkoutSession { UserId = userIds[1], StartDate = new DateTime(2025, 10, 31, 9, 0, 0), EndDate = new DateTime(2025, 10, 31, 9, 30, 0), Notes = "User2 session." },
                new WorkoutSession { UserId = userIds[2], StartDate = new DateTime(2025, 11, 1, 7, 0, 0), EndDate = new DateTime(2025, 11, 1, 7, 40, 0), Notes = "Admin session." }
            );
            await db.SaveChangesAsync();
        }

        if (!await db.WorkoutSessionDetails.AnyAsync())
        {
            db.WorkoutSessionDetails.AddRange(
                new WorkoutSessionDetails { WorkoutSessionId = 1, ExerciseId = 1, Sets = 3, Repetitions = 10, RestTimeInSeconds = 60, Tempo = "2-1-2" },
                new WorkoutSessionDetails { WorkoutSessionId = 1, ExerciseId = 2, Sets = 3, Repetitions = 15, RestTimeInSeconds = 60, Tempo = "2-1-2" },
                new WorkoutSessionDetails { WorkoutSessionId = 2, ExerciseId = 2, Sets = 2, Repetitions = 12, RestTimeInSeconds = 60, Tempo = "2-1-2" },
                new WorkoutSessionDetails { WorkoutSessionId = 3, ExerciseId = 3, Sets = 2, DurationInSeconds = 60, RestTimeInSeconds = 90 }
            );
            await db.SaveChangesAsync();
        }
    }
}
