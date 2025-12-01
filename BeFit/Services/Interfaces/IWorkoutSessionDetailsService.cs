using BeFit.DTOs;
using BeFit.Results;

namespace BeFit.Services.Interfaces;

public interface IWorkoutSessionDetailsService
{
    Task<Result<PaginatedList<WorkoutSessionDetailResponse>>> GetWorkoutSessionDetailsAsync(string userId, int workoutSessionId);
    Task<Result<WorkoutSessionDetailResponse>> GetWorkoutSessionDetailsByIdAsync(string userId, int workoutSessionDetailId);
    Task<Result> AddWorkoutSessionDetailAsync(string userId, int workoutSessionId, WorkoutSessionDetailRequest workoutSessionDetailRequest);
    Task<Result> RemoveWorkoutSessionDetailAsync(string userId, int workoutSessionDetailId);
    Task<Result> UpdateWorkoutSessionDetailAsync(string userId, int workoutSessionDetailId, WorkoutSessionDetailRequest workoutSessionDetailRequest);
}
