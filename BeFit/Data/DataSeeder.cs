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

        var pushUpExercise = await db.Exercises.FirstOrDefaultAsync(e => e.Name == "Push Up");
        if (pushUpExercise == null)
        {
            pushUpExercise = new Exercise { Name = "Push Up", Category = ExerciseType.Strength, TargetMuscle = MuscleType.Chest, Difficulty = Difficulty.Beginner, Instructions = "Start in plank position, lower body, push up." };
            db.Exercises.Add(pushUpExercise);
        }

        var squatExercise = await db.Exercises.FirstOrDefaultAsync(e => e.Name == "Squat");
        if (squatExercise == null)
        {
            squatExercise = new Exercise { Name = "Squat", Category = ExerciseType.Strength, TargetMuscle = MuscleType.Legs, Difficulty = Difficulty.Beginner, Instructions = "Stand, lower hips, return." };
            db.Exercises.Add(squatExercise);
        }

        var plankExercise = await db.Exercises.FirstOrDefaultAsync(e => e.Name == "Plank");
        if (plankExercise == null)
        {
            plankExercise = new Exercise { Name = "Plank", Category = ExerciseType.Strength, TargetMuscle = MuscleType.Core, Difficulty = Difficulty.Intermediate, Instructions = "Hold plank position." };
            db.Exercises.Add(plankExercise);
        }
        await db.SaveChangesAsync();

        var beginnerStrengthTemplate = await db.WorkoutTemplates.FirstOrDefaultAsync(wt => wt.Name == "Beginner Strength" && wt.UserId == userIds[0]);
        if (beginnerStrengthTemplate == null)
        {
            beginnerStrengthTemplate = new WorkoutTemplate { UserId = userIds[0], Name = "Beginner Strength", Description = "Simple strength workout.", Goals = "Build strength.", PreferredDay = DayOfWeek.Monday };
            db.WorkoutTemplates.Add(beginnerStrengthTemplate);
        }

        var coreFocusTemplate = await db.WorkoutTemplates.FirstOrDefaultAsync(wt => wt.Name == "Core Focus" && wt.UserId == userIds[2]);
        if (coreFocusTemplate == null)
        {
            coreFocusTemplate = new WorkoutTemplate { UserId = userIds[2], Name = "Core Focus", Description = "Core muscle workout.", Goals = "Improve core stability.", PreferredDay = DayOfWeek.Wednesday };
            db.WorkoutTemplates.Add(coreFocusTemplate);
        }
        await db.SaveChangesAsync();

        if (!await db.WorkoutTemplateExercises.AnyAsync(wte => wte.WorkoutTemplateId == beginnerStrengthTemplate.Id && wte.ExerciseId == pushUpExercise.Id))
        {
            db.WorkoutTemplateExercises.Add(new WorkoutTemplateExercise { WorkoutTemplateId = beginnerStrengthTemplate.Id, ExerciseId = pushUpExercise.Id, Order = 1, TargetSets = 3, TargetReps = 10, RestTimeInSeconds = 60, Tempo = "2-1-2" });
        }
        if (!await db.WorkoutTemplateExercises.AnyAsync(wte => wte.WorkoutTemplateId == beginnerStrengthTemplate.Id && wte.ExerciseId == squatExercise.Id))
        {
            db.WorkoutTemplateExercises.Add(new WorkoutTemplateExercise { WorkoutTemplateId = beginnerStrengthTemplate.Id, ExerciseId = squatExercise.Id, Order = 2, TargetSets = 3, TargetReps = 15, RestTimeInSeconds = 60, Tempo = "2-1-2" });
        }
        if (!await db.WorkoutTemplateExercises.AnyAsync(wte => wte.WorkoutTemplateId == coreFocusTemplate.Id && wte.ExerciseId == plankExercise.Id))
        {
            db.WorkoutTemplateExercises.Add(new WorkoutTemplateExercise { WorkoutTemplateId = coreFocusTemplate.Id, ExerciseId = plankExercise.Id, Order = 1, TargetSets = 2, TargetDurationInSeconds = 60, RestTimeInSeconds = 90 });
        }
        await db.SaveChangesAsync();

        var session1 = await db.WorkoutSessions.FirstOrDefaultAsync(ws => ws.UserId == userIds[0] && ws.StartDate == new DateTime(2025, 10, 30, 8, 0, 0));
        if (session1 == null)
        {
            session1 = new WorkoutSession { UserId = userIds[0], StartDate = new DateTime(2025, 10, 30, 8, 0, 0), EndDate = new DateTime(2025, 10, 30, 8, 45, 0), Notes = "First session." };
            db.WorkoutSessions.Add(session1);
        }

        var session2 = await db.WorkoutSessions.FirstOrDefaultAsync(ws => ws.UserId == userIds[1] && ws.StartDate == new DateTime(2025, 10, 31, 9, 0, 0));
        if (session2 == null)
        {
            session2 = new WorkoutSession { UserId = userIds[1], StartDate = new DateTime(2025, 10, 31, 9, 0, 0), EndDate = new DateTime(2025, 10, 31, 9, 30, 0), Notes = "User2 session." };
            db.WorkoutSessions.Add(session2);
        }

        var session3 = await db.WorkoutSessions.FirstOrDefaultAsync(ws => ws.UserId == userIds[2] && ws.StartDate == new DateTime(2025, 11, 1, 7, 0, 0));
        if (session3 == null)
        {
            session3 = new WorkoutSession { UserId = userIds[2], StartDate = new DateTime(2025, 11, 1, 7, 0, 0), EndDate = new DateTime(2025, 11, 1, 7, 40, 0), Notes = "Admin session." };
            db.WorkoutSessions.Add(session3);
        }
        await db.SaveChangesAsync();

        if (!await db.WorkoutSessionDetails.AnyAsync(wsd => wsd.WorkoutSessionId == session1.Id && wsd.ExerciseId == pushUpExercise.Id))
        {
            db.WorkoutSessionDetails.Add(new WorkoutSessionDetails { WorkoutSessionId = session1.Id, ExerciseId = pushUpExercise.Id, Sets = 3, Repetitions = 10, RestTimeInSeconds = 60, Tempo = "2-1-2" });
        }
        if (!await db.WorkoutSessionDetails.AnyAsync(wsd => wsd.WorkoutSessionId == session1.Id && wsd.ExerciseId == squatExercise.Id))
        {
            db.WorkoutSessionDetails.Add(new WorkoutSessionDetails { WorkoutSessionId = session1.Id, ExerciseId = squatExercise.Id, Sets = 3, Repetitions = 15, RestTimeInSeconds = 60, Tempo = "2-1-2" });
        }
        if (!await db.WorkoutSessionDetails.AnyAsync(wsd => wsd.WorkoutSessionId == session2.Id && wsd.ExerciseId == squatExercise.Id))
        {
            db.WorkoutSessionDetails.Add(new WorkoutSessionDetails { WorkoutSessionId = session2.Id, ExerciseId = squatExercise.Id, Sets = 2, Repetitions = 12, RestTimeInSeconds = 60, Tempo = "2-1-2" });
        }
        if (!await db.WorkoutSessionDetails.AnyAsync(wsd => wsd.WorkoutSessionId == session3.Id && wsd.ExerciseId == plankExercise.Id))
        {
            db.WorkoutSessionDetails.Add(new WorkoutSessionDetails { WorkoutSessionId = session3.Id, ExerciseId = plankExercise.Id, Sets = 2, DurationInSeconds = 60, RestTimeInSeconds = 90 });
        }
        await db.SaveChangesAsync();
    }
}
