using BeFit.Data;
using BeFit.DTOs;
using BeFit.Models;
using BeFit.Results;
using BeFit.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace BeFit.Services;

public class WorkoutTemplateExerciseService : IWorkoutTemplateExerciseService
{
    private readonly ApplicationDbContext _context;

    public WorkoutTemplateExerciseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<List<WorkoutTemplateExerciseResponse>>> GetWorkoutTemplateExercisesAsync(string userId, int workoutTemplateId)
    {
        try
        {
            var exercises = await _context.WorkoutTemplateExercises
                .Where(wte => wte.WorkoutTemplateId == workoutTemplateId && wte.WorkoutTemplate.UserId == userId)
                .AsNoTracking()
                .OrderBy(wte => wte.Order)
                .Select(wte => new WorkoutTemplateExerciseResponse(
                    wte.Id,
                    wte.WorkoutTemplateId,
                    wte.ExerciseId,
                    wte.Order,
                    wte.TargetSets,
                    wte.TargetReps,
                    wte.TargetWeight,
                    wte.RestTimeInSeconds,
                    wte.Tempo,
                    wte.TargetDistance,
                    wte.TargetDurationInSeconds
                ))
                .ToListAsync();

            return Result.Success(exercises);
        }
        catch (Exception)
        {
            return Result.Failure<List<WorkoutTemplateExerciseResponse>>(Error.General);
        }
    }

    public async Task<Result<List<WorkoutTemplateExerciseWithExerciseNameResponse>>> GetWorkoutTemplateExercisesWithNamesAsync(string userId, int workoutTemplateId)
    {
        try
        {
            var exercises = await _context.WorkoutTemplateExercises
                .Where(wte => wte.WorkoutTemplateId == workoutTemplateId && wte.WorkoutTemplate.UserId == userId)
                .AsNoTracking()
                .OrderBy(wte => wte.Order)
                .Select(wte => new WorkoutTemplateExerciseWithExerciseNameResponse(
                    wte.Id,
                    wte.WorkoutTemplateId,
                    wte.ExerciseId,
                    wte.Exercise.Name,
                    wte.Order,
                    wte.TargetSets,
                    wte.TargetReps,
                    wte.TargetWeight,
                    wte.RestTimeInSeconds,
                    wte.Tempo,
                    wte.TargetDistance,
                    wte.TargetDurationInSeconds
                ))
                .ToListAsync();

            return Result.Success(exercises);
        }
        catch (Exception)
        {
            return Result.Failure<List<WorkoutTemplateExerciseWithExerciseNameResponse>>(Error.General);
        }
    }

    public async Task<Result<WorkoutTemplateExerciseWithExerciseNameResponse>> GetWorkoutTemplateExerciseByIdAsync(string userId, int workoutTemplateExerciseId)
    {
        try
        {
            var exercise = await _context.WorkoutTemplateExercises
                .AsNoTracking()
                .Where(wte => wte.Id == workoutTemplateExerciseId && wte.WorkoutTemplate.UserId == userId)
                .Select(wte => new WorkoutTemplateExerciseWithExerciseNameResponse(
                    wte.Id,
                    wte.WorkoutTemplateId,
                    wte.ExerciseId,
                    wte.Exercise.Name,
                    wte.Order,
                    wte.TargetSets,
                    wte.TargetReps,
                    wte.TargetWeight,
                    wte.RestTimeInSeconds,
                    wte.Tempo,
                    wte.TargetDistance,
                    wte.TargetDurationInSeconds
                ))
                .FirstOrDefaultAsync();

            if (exercise == null)
                return Result.Failure<WorkoutTemplateExerciseWithExerciseNameResponse>(Error.NotFound("WorkoutTemplateExerciseNotFound", "Workout template exercise not found"));

            return Result.Success(exercise);
        }
        catch (Exception)
        {
            return Result.Failure<WorkoutTemplateExerciseWithExerciseNameResponse>(Error.General);
        }
    }

    public async Task<Result> AddWorkoutTemplateExerciseAsync(string userId, int workoutTemplateId, WorkoutTemplateExerciseRequest request)
    {
        try
        {
            var templateExistsForUser = await _context.WorkoutTemplates
                .AnyAsync(wt => wt.Id == workoutTemplateId && wt.UserId == userId);
            if (!templateExistsForUser)
                return Result.Failure(Error.NotFound("WorkoutTemplateNotFound", "Workout template not found"));

            var exerciseExists = await _context.Exercises.AnyAsync(e => e.Id == request.ExerciseId);
            if (!exerciseExists)
                return Result.Failure(Error.NotFound("ExerciseNotFound", "Exercise not found"));

            var orderExists = await _context.WorkoutTemplateExercises
                .AnyAsync(wte => wte.WorkoutTemplateId == workoutTemplateId && wte.Order == request.Order);
            if (orderExists)
                return Result.Failure(Error.Validation("OrderAlreadyExists", "An exercise with this order already exists for this template"));

            var exercise = new WorkoutTemplateExercise
            {
                WorkoutTemplateId = workoutTemplateId,
                ExerciseId = request.ExerciseId,
                Order = request.Order,
                TargetSets = request.TargetSets,
                TargetReps = request.TargetReps,
                TargetWeight = request.TargetWeight,
                RestTimeInSeconds = request.RestTimeInSeconds,
                Tempo = request.Tempo,
                TargetDistance = request.TargetDistance,
                TargetDurationInSeconds = request.TargetDurationInSeconds
            };

            _context.WorkoutTemplateExercises.Add(exercise);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(Error.General);
        }
    }

    public async Task<Result> UpdateWorkoutTemplateExerciseAsync(string userId, int workoutTemplateExerciseId, WorkoutTemplateExerciseRequest request)
    {
        try
        {
            var exercise = await _context.WorkoutTemplateExercises
                .Include(wte => wte.WorkoutTemplate)
                .FirstOrDefaultAsync(wte => wte.Id == workoutTemplateExerciseId && wte.WorkoutTemplate.UserId == userId);
            if (exercise == null)
                return Result.Failure(Error.NotFound("WorkoutTemplateExerciseNotFound", "Workout template exercise not found"));

            var exerciseExists = await _context.Exercises.AnyAsync(e => e.Id == request.ExerciseId);
            if (!exerciseExists)
                return Result.Failure(Error.NotFound("ExerciseNotFound", "Exercise not found"));

            var orderExists = await _context.WorkoutTemplateExercises
                .AnyAsync(wte => wte.WorkoutTemplateId == request.WorkoutTemplateId 
                    && wte.Order == request.Order 
                    && wte.Id != workoutTemplateExerciseId);
            if (orderExists)
                return Result.Failure(Error.Validation("OrderAlreadyExists", "An exercise with this order already exists for this template"));

            exercise.ExerciseId = request.ExerciseId;
            exercise.Order = request.Order;
            exercise.TargetSets = request.TargetSets;
            exercise.TargetReps = request.TargetReps;
            exercise.TargetWeight = request.TargetWeight;
            exercise.RestTimeInSeconds = request.RestTimeInSeconds;
            exercise.Tempo = request.Tempo;
            exercise.TargetDistance = request.TargetDistance;
            exercise.TargetDurationInSeconds = request.TargetDurationInSeconds;

            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(Error.General);
        }
    }

    public async Task<Result> RemoveWorkoutTemplateExerciseAsync(string userId, int workoutTemplateExerciseId)
    {
        try
        {
            var exercise = await _context.WorkoutTemplateExercises
                .Include(wte => wte.WorkoutTemplate)
                .FirstOrDefaultAsync(wte => wte.Id == workoutTemplateExerciseId && wte.WorkoutTemplate.UserId == userId);
            if (exercise == null)
                return Result.Failure(Error.NotFound("WorkoutTemplateExerciseNotFound", "Workout template exercise not found"));

            _context.WorkoutTemplateExercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(Error.General);
        }
    }
}
