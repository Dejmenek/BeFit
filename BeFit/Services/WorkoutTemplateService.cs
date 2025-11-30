using BeFit.Data;
using BeFit.DTOs;
using BeFit.Models;
using BeFit.Results;
using BeFit.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace BeFit.Services;

public class WorkoutTemplateService : IWorkoutTemplateService
{
    private readonly ApplicationDbContext _context;

    public WorkoutTemplateService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> CreateWorkoutTemplateAsync(string userId, WorkoutTemplateRequest request)
    {
        try
        {
            var workoutTemplate = new WorkoutTemplate
            {
                UserId = userId,
                Name = request.Name,
                Description = request.Description,
                Goals = request.Goals,
                PreferredDay = request.PreferredDay
            };

            _context.WorkoutTemplates.Add(workoutTemplate);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(Error.General);
        }
    }

    public async Task<Result<PaginatedList<WorkoutTemplateResponse>>> GetUserWorkoutTemplatesAsync(string userId, int pageNumber, int pageSize)
    {
        try
        {
            var query = _context.WorkoutTemplates
                .Where(wt => wt.UserId == userId)
                .AsNoTracking();

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderBy(wt => wt.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(wt => new WorkoutTemplateResponse
                {
                    Id = wt.Id,
                    Name = wt.Name,
                    Description = wt.Description,
                    Goals = wt.Goals,
                    PreferredDay = wt.PreferredDay
                })
                .ToListAsync();

            var paginatedList = new PaginatedList<WorkoutTemplateResponse>(items, totalItems, pageNumber, pageSize);

            return Result.Success(paginatedList);
        }
        catch (Exception)
        {
            return Result.Failure<PaginatedList<WorkoutTemplateResponse>>(Error.General);
        }
    }

    public async Task<Result<WorkoutTemplateResponse>> GetWorkoutTemplateByIdAsync(int id)
    {
        try
        {
            var workoutTemplate = await _context.WorkoutTemplates
                .AsNoTracking()
                .FirstOrDefaultAsync(wt => wt.Id == id);

            if (workoutTemplate == null)
                return Result.Failure<WorkoutTemplateResponse>(Error.NotFound("WorkoutTemplateNotFound", "Workout template not found"));

            var response = new WorkoutTemplateResponse
            {
                Id = workoutTemplate.Id,
                Name = workoutTemplate.Name,
                Description = workoutTemplate.Description,
                Goals = workoutTemplate.Goals,
                PreferredDay = workoutTemplate.PreferredDay
            };

            return Result.Success(response);
        }
        catch (Exception)
        {
            return Result.Failure<WorkoutTemplateResponse>(Error.General);
        }
    }

    public async Task<Result> UpdateWorkoutTemplateAsync(int id, WorkoutTemplateRequest request)
    {
        try
        {
            var workoutTemplate = await _context.WorkoutTemplates
                .FirstOrDefaultAsync(wt => wt.Id == id);

            if (workoutTemplate == null)
                return Result.Failure(Error.NotFound("WorkoutTemplateNotFound", "Workout template not found"));

            workoutTemplate.Name = request.Name;
            workoutTemplate.Description = request.Description;
            workoutTemplate.Goals = request.Goals;
            workoutTemplate.PreferredDay = request.PreferredDay;

            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(Error.General);
        }
    }

    public async Task<Result> DeleteWorkoutTemplateAsync(int id)
    {
        try
        {
            var workoutTemplate = await _context.WorkoutTemplates
                .FirstOrDefaultAsync(wt => wt.Id == id);

            if (workoutTemplate == null)
                return Result.Failure(Error.NotFound("WorkoutTemplateNotFound", "Workout template not found"));

            _context.WorkoutTemplates.Remove(workoutTemplate);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(Error.General);
        }
    }
}
