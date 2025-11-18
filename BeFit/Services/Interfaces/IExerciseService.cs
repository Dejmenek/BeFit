using BeFit.DTOs;
using BeFit.Results;

namespace BeFit.Services.Interfaces;

public interface IExerciseService
{
    Task<Result<PaginatedList<ExerciseResponse>>> GetExercisesAsync(int pageNumber, int pageSize);
    Task<Result> RemoveSingleExerciseAsync(int exerciseId);
    Task<Result> CreateExerciseAsync(ExerciseRequest exerciseRequest);
    Task<Result> UpdateExerciseAsync(int exerciseId, ExerciseRequest exerciseRequest);
}
