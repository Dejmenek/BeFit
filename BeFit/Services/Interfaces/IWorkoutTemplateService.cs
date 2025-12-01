using BeFit.DTOs;
using BeFit.Models;
using BeFit.Results;

namespace BeFit.Services.Interfaces;

public interface IWorkoutTemplateService
{
    Task<Result> CreateWorkoutTemplateAsync(string userId, WorkoutTemplateRequest request);
    Task<Result<PaginatedList<WorkoutTemplateResponse>>> GetUserWorkoutTemplatesAsync(string userId, int pageNumber, int pageSize);
    Task<Result<WorkoutTemplateResponse>> GetWorkoutTemplateByIdAsync(string userId, int id);
    Task<Result> UpdateWorkoutTemplateAsync(string userId, int id, WorkoutTemplateRequest request);
    Task<Result> DeleteWorkoutTemplateAsync(string userId, int id);
}
