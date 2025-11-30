using BeFit.Data;
using BeFit.DTOs;
using BeFit.Results;
using BeFit.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace BeFit.Services;

public class DashboardService : IDashboardService
{
    private readonly ApplicationDbContext _context;

    public DashboardService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<WorkoutStatsResponse>> GetWorkoutStatsAsync(string userId)
    {
        try
        {
            var sessions = await _context.WorkoutSessions
                .Where(ws => ws.UserId == userId)
                .ToListAsync();

            int totalWorkouts = sessions.Count;
            TimeSpan totalTimeSpent = sessions
                .Aggregate(TimeSpan.Zero, (sum, ws) => sum + (ws.EndDate - ws.StartDate));

            var dates = sessions
                .Select(ws => ws.StartDate.Date)
                .Distinct()
                .OrderByDescending(d => d)
                .ToList();

            int streak = 0;
            var today = DateTime.Today;
            foreach (var date in dates)
            {
                if (date == today.AddDays(-streak))
                    streak++;
                else
                    break;
            }

            return Result.Success(new WorkoutStatsResponse(
                    totalWorkouts,
                    streak,
                    totalTimeSpent
            ));
        }
        catch (Exception)
        {
            return Result.Failure<WorkoutStatsResponse>(Error.General);
        }
    }

    public async Task<Result<List<ExerciseStatsResponse>>> GetExerciseStatsAsync(string userId)
    {
        try
        {
            var details = await _context.WorkoutSessionDetails
                .Include(d => d.Exercise)
                .Include(d => d.WorkoutSession)
                .Where(d => d.WorkoutSession.UserId == userId)
                .ToListAsync();

            var stats = details
                .GroupBy(d => d.Exercise)
                .Select(g => new ExerciseStatsResponse
                {
                    Name = g.Key.Name,
                    Category = g.Key.Category,
                    TargetMuscle = g.Key.TargetMuscle,
                    SessionsTrained = g.Select(d => d.WorkoutSessionId).Distinct().Count(),
                    TotalReps = g.Sum(d => (d.Sets ?? 0) * (d.Repetitions ?? 0)),
                    AverageWeight = g.Where(d => d.Weight.HasValue).Any() ? g.Where(d => d.Weight.HasValue).Average(d => d.Weight.Value) : 0,
                    MaxWeight = g.Where(d => d.Weight.HasValue).Any() ? g.Where(d => d.Weight.HasValue).Max(d => d.Weight.Value) : 0
                })
                .ToList();

            return Result.Success(stats);
        }
        catch (Exception)
        {
            return Result.Failure<List<ExerciseStatsResponse>>(Error.General);
        }
    }

    public async Task<Result<List<WorkoutTemplateCalendarResponse>>> GetTrainingCalendarAsync(string userId)
    {
        try
        {
            var templates = await _context.WorkoutTemplates
                .Include(t => t.WorkoutTemplateExercises)
                .ThenInclude(te => te.Exercise)
                .Where(t => t.UserId == userId)
                .ToListAsync();

            var calendar = templates
                .Select(t => new WorkoutTemplateCalendarResponse
                {
                    TemplateName = t.Name,
                    DayOfWeek = t.PreferredDay,
                    ExerciseNames = t.WorkoutTemplateExercises.Select(te => te.Exercise.Name).ToList()
                })
                .OrderBy(t => t.DayOfWeek)
                .ToList();

            return Result.Success(calendar);
        }
        catch (Exception)
        {
            return Result.Failure<List<WorkoutTemplateCalendarResponse>>(Error.General);
        }
    }
}
