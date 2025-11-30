using BeFit.Data;
using BeFit.DTOs;
using BeFit.Models;
using BeFit.Results;
using BeFit.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace BeFit.Services;

public class WorkoutSessionDetailsService : IWorkoutSessionDetailsService
{
    private readonly ApplicationDbContext _context;

    public WorkoutSessionDetailsService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> AddWorkoutSessionDetailAsync(int workoutSessionId, WorkoutSessionDetailRequest workoutSessionDetailRequest)
    {
        try
        {
            var sessionExists = await _context.WorkoutSessions.AnyAsync(ws => ws.Id == workoutSessionId);
            if (!sessionExists)
                return Result.Failure(Error.NotFound("WorkoutSessionNotFound", "Workout session not found"));

            var exerciseExists = await _context.Exercises.AnyAsync(e => e.Id == workoutSessionDetailRequest.ExerciseId);
            if (!exerciseExists)
                return Result.Failure(Error.NotFound("ExerciseNotFound", "Exercise not found"));

            var detail = new WorkoutSessionDetails
            {
                WorkoutSessionId = workoutSessionId,
                ExerciseId = workoutSessionDetailRequest.ExerciseId,
                Sets = workoutSessionDetailRequest.Sets,
                Repetitions = workoutSessionDetailRequest.Repetitions,
                Weight = workoutSessionDetailRequest.Weight,
                RestTimeInSeconds = workoutSessionDetailRequest.RestTimeInSeconds,
                Tempo = workoutSessionDetailRequest.Tempo,
                DurationInSeconds = workoutSessionDetailRequest.DurationInSeconds,
                Distance = workoutSessionDetailRequest.Distance
            };

            _context.WorkoutSessionDetails.Add(detail);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(Error.General);
        }
    }

    public async Task<Result<PaginatedList<WorkoutSessionDetailResponse>>> GetWorkoutSessionDetailsAsync(int workoutSessionId)
    {
        try
        {
            var query = _context.WorkoutSessionDetails
                .AsNoTracking()
                .Where(d => d.WorkoutSessionId == workoutSessionId);

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderBy(d => d.Id)
                .Select(d => new WorkoutSessionDetailResponse
                {
                    Id = d.Id,
                    WorkoutSessionId = d.WorkoutSessionId,
                    ExerciseId = d.ExerciseId,
                    ExerciseName = d.Exercise.Name,
                    Sets = d.Sets,
                    Repetitions = d.Repetitions,
                    Weight = d.Weight,
                    RestTimeInSeconds = d.RestTimeInSeconds,
                    Tempo = d.Tempo,
                    DurationInSeconds = d.DurationInSeconds,
                    Distance = d.Distance
                })
                .ToListAsync();

            var paginatedList = new PaginatedList<WorkoutSessionDetailResponse>(items, totalItems, 1, totalItems);

            return Result.Success(paginatedList);
        }
        catch (Exception)
        {
            return Result.Failure<PaginatedList<WorkoutSessionDetailResponse>>(Error.General);
        }
    }

    public async Task<Result<WorkoutSessionDetailResponse>> GetWorkoutSessionDetailsByIdAsync(int workoutSessionDetailId)
    {
        try
        {
            var detail = await _context.WorkoutSessionDetails
                .AsNoTracking()
                .Where(d => d.Id == workoutSessionDetailId)
                .Select(d => new WorkoutSessionDetailResponse
                {
                    Id = d.Id,
                    WorkoutSessionId = d.WorkoutSessionId,
                    ExerciseId = d.ExerciseId,
                    ExerciseName = d.Exercise.Name,
                    Sets = d.Sets,
                    Repetitions = d.Repetitions,
                    Weight = d.Weight,
                    RestTimeInSeconds = d.RestTimeInSeconds,
                    Tempo = d.Tempo,
                    DurationInSeconds = d.DurationInSeconds,
                    Distance = d.Distance
                })
                .FirstOrDefaultAsync();

            if (detail == null)
                return Result.Failure<WorkoutSessionDetailResponse>(Error.NotFound("WorkoutSessionDetailNotFound", "Workout session detail not found"));

            return Result.Success(detail);
        }
        catch (Exception)
        {
            return Result.Failure<WorkoutSessionDetailResponse>(Error.General);
        }
    }

    public async Task<Result> RemoveWorkoutSessionDetailAsync(int workoutSessionDetailId)
    {
        try
        {
            var detail = await _context.WorkoutSessionDetails.FindAsync(workoutSessionDetailId);
            if (detail == null)
                return Result.Failure(Error.NotFound("WorkoutSessionDetailNotFound", "Workout session detail not found"));

            _context.WorkoutSessionDetails.Remove(detail);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(Error.General);
        }
    }

    public async Task<Result> UpdateWorkoutSessionDetailAsync(int workoutSessionDetailId, WorkoutSessionDetailRequest workoutSessionDetailRequest)
    {
        try
        {
            var detail = await _context.WorkoutSessionDetails.FindAsync(workoutSessionDetailId);
            if (detail == null)
                return Result.Failure(Error.NotFound("WorkoutSessionDetailNotFound", "Workout session detail not found"));

            var exerciseExists = await _context.Exercises.AnyAsync(e => e.Id == workoutSessionDetailRequest.ExerciseId);
            if (!exerciseExists)
                return Result.Failure(Error.NotFound("ExerciseNotFound", "Exercise not found"));

            detail.ExerciseId = workoutSessionDetailRequest.ExerciseId;
            detail.Sets = workoutSessionDetailRequest.Sets;
            detail.Repetitions = workoutSessionDetailRequest.Repetitions;
            detail.Weight = workoutSessionDetailRequest.Weight;
            detail.RestTimeInSeconds = workoutSessionDetailRequest.RestTimeInSeconds;
            detail.Tempo = workoutSessionDetailRequest.Tempo;
            detail.DurationInSeconds = workoutSessionDetailRequest.DurationInSeconds;
            detail.Distance = workoutSessionDetailRequest.Distance;

            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(Error.General);
        }
    }
}
