using BeFit.DTOs;
using BeFit.Results;

namespace BeFit.Services.Interfaces;

public interface IWorkoutSessionService
{
    Task<Result<PaginatedList<WorkoutSessionResponse>>> GetUserWorkoutSessionsAsync(string userId, int pageNumber, int pageSize);
    Task<Result> UpdateWorkoutSessionAsync(string userId, int sessionId, WorkoutSessionRequest request);
    Task<Result> DeleteWorkoutSessionAsync(string userId, int sessionId);
    Task<Result> CreateWorkoutSessionAsync(string userId, WorkoutSessionRequest request);
    Task<Result<WorkoutSessionResponse>> GetWorkoutSessionByIdAsync(string userId, int sessionId);
}
