using BeFit.DTOs;
using BeFit.Results;

namespace BeFit.Services.Interfaces;

public interface IWorkoutSessionDetailsService
{
    Task<Result<PaginatedList<WorkoutSessionDetailResponse>>> GetWorkoutSessionDetailsAsync(int workoutSessionId);
    Task<Result<WorkoutSessionDetailResponse>> GetWorkoutSessionDetailsByIdAsync(int workoutSessionDetailId);
    Task<Result> AddWorkoutSessionDetailAsync(int workoutSessionId, WorkoutSessionDetailRequest workoutSessionDetailRequest);
    Task<Result> RemoveWorkoutSessionDetailAsync(int workoutSessionDetailId);
    Task<Result> UpdateWorkoutSessionDetailAsync(int workoutSessionDetailId, WorkoutSessionDetailRequest workoutSessionDetailRequest);
}
