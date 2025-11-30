using BeFit.Data;
using BeFit.DTOs;
using BeFit.Models;
using BeFit.Results;
using BeFit.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace BeFit.Services;

public class ExerciseService : IExerciseService
{
    private readonly ApplicationDbContext _context;

    public ExerciseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> CreateExerciseAsync(ExerciseRequest exerciseRequest)
    {
        try
        {
            var exercise = new Exercise
            {
                Name = exerciseRequest.Name,
                Category = exerciseRequest.Category,
                TargetMuscle = exerciseRequest.TargetMuscle,
                Difficulty = exerciseRequest.Difficulty,
                Instructions = exerciseRequest.Instructions,
                Equipment = exerciseRequest.Equipment
            };

            _context.Exercises.Add(exercise);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(Error.General);
        }
    }

    public async Task<Result<PaginatedList<ExerciseResponse>>> GetExercisesAsync(int pageNumber, int pageSize)
    {
        try
        {
            var query = _context.Exercises.AsNoTracking();

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderBy(e => e.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new ExerciseResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    Category = e.Category,
                    TargetMuscle = e.TargetMuscle,
                    Difficulty = e.Difficulty,
                    Instructions = e.Instructions,
                    Equipment = e.Equipment
                })
                .ToListAsync();

            var paginatedList = new PaginatedList<ExerciseResponse>(items, totalItems, pageNumber, pageSize);

            return Result.Success(paginatedList);
        }
        catch (Exception)
        {
            return Result.Failure<PaginatedList<ExerciseResponse>>(Error.General);
        }
    }

    public async Task<Result<List<ExerciseResponse>>> GetAllExercisesAsync()
    {
        try
        {
            var exercises = await _context.Exercises
                .AsNoTracking()
                .OrderBy(e => e.Name)
                .Select(e => new ExerciseResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    Category = e.Category,
                    TargetMuscle = e.TargetMuscle,
                    Difficulty = e.Difficulty,
                    Instructions = e.Instructions,
                    Equipment = e.Equipment
                })
                .ToListAsync();

            return Result.Success(exercises);
        }
        catch (Exception)
        {
            return Result.Failure<List<ExerciseResponse>>(Error.General);
        }
    }

    public async Task<Result> RemoveSingleExerciseAsync(int exerciseId)
    {
        try
        {
            var exercise = await _context.Exercises.FindAsync(exerciseId);
            if (exercise == null)
                return Result.Failure(Error.NotFound("ExerciseNotFound", "Exercise not found"));

            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(Error.General);
        }
    }

    public async Task<Result> UpdateExerciseAsync(int exerciseId, ExerciseRequest exerciseRequest)
    {
        try
        {
            var exercise = await _context.Exercises.FindAsync(exerciseId);
            if (exercise == null)
                return Result.Failure(Error.NotFound("ExerciseNotFound", "Exercise not found"));

            exercise.Name = exerciseRequest.Name;
            exercise.Category = exerciseRequest.Category;
            exercise.TargetMuscle = exerciseRequest.TargetMuscle;
            exercise.Difficulty = exerciseRequest.Difficulty;
            exercise.Instructions = exerciseRequest.Instructions;
            exercise.Equipment = exerciseRequest.Equipment;

            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(Error.General);
        }
    }
}
