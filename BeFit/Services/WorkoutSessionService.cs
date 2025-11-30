using BeFit.Data;
using BeFit.DTOs;
using BeFit.Models;
using BeFit.Results;
using BeFit.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace BeFit.Services;

public class WorkoutSessionService : IWorkoutSessionService
{
    private readonly ApplicationDbContext _context;

    public WorkoutSessionService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> CreateWorkoutSessionAsync(string userId, WorkoutSessionRequest request)
    {
        try
        {
            var session = new WorkoutSession
            {
                UserId = userId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                Notes = request.Notes
            };

            _context.WorkoutSessions.Add(session);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(Error.General);
        }
    }

    public async Task<Result> DeleteWorkoutSessionAsync(int sessionId)
    {
        try
        {
            var session = await _context.WorkoutSessions
                .FirstOrDefaultAsync(ws => ws.Id == sessionId);

            if (session == null)
                return Result.Failure(Error.NotFound("WorkoutSessionNotFound", "Workout session not found"));

            _context.WorkoutSessions.Remove(session);
            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(Error.General);
        }
    }

    public async Task<Result<PaginatedList<WorkoutSessionResponse>>> GetUserWorkoutSessionsAsync(string userId, int pageNumber, int pageSize)
    {
        try
        {
            var query = _context.WorkoutSessions
                .Where(ws => ws.UserId == userId)
                .AsNoTracking();

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderByDescending(ws => ws.StartDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(ws => new WorkoutSessionResponse
                (
                    ws.Id,
                    ws.StartDate,
                    ws.EndDate,
                    ws.Notes
                ))
                .ToListAsync();

            var paginatedList = new PaginatedList<WorkoutSessionResponse>(items, totalItems, pageNumber, pageSize);

            return Result.Success(paginatedList);
        }
        catch (Exception)
        {
            return Result.Failure<PaginatedList<WorkoutSessionResponse>>(Error.General);
        }
    }

    public async Task<Result> UpdateWorkoutSessionAsync(int sessionId, WorkoutSessionRequest request)
    {
        try
        {
            var session = await _context.WorkoutSessions
                .FirstOrDefaultAsync(ws => ws.Id == sessionId);

            if (session == null)
                return Result.Failure(Error.NotFound("WorkoutSessionNotFound", "Workout session not found"));

            session.StartDate = request.StartDate;
            session.EndDate = request.EndDate;
            session.Notes = request.Notes;

            await _context.SaveChangesAsync();

            return Result.Success();
        }
        catch (Exception)
        {
            return Result.Failure(Error.General);
        }
    }

    public async Task<Result<WorkoutSessionResponse>> GetWorkoutSessionByIdAsync(int sessionId)
    {
        try
        {
            var session = await _context.WorkoutSessions
                .AsNoTracking()
                .FirstOrDefaultAsync(ws => ws.Id == sessionId);

            if (session == null)
                return Result.Failure<WorkoutSessionResponse>(Error.NotFound("WorkoutSessionNotFound", "Workout session not found"));

            var response = new WorkoutSessionResponse
            (
                session.Id,
                session.StartDate,
                session.EndDate,
                session.Notes
            );

            return Result.Success(response);
        }
        catch (Exception)
        {
            return Result.Failure<WorkoutSessionResponse>(Error.General);
        }
    }
}
