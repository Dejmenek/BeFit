using BeFit.DTOs;
using BeFit.Models;
using BeFit.Results;

namespace BeFit.Services.Interfaces;

public interface IWorkoutTemplateExerciseService
{
    Task<Result<List<WorkoutTemplateExerciseResponse>>> GetWorkoutTemplateExercisesAsync(string userId, int workoutTemplateId);
    Task<Result<List<WorkoutTemplateExerciseWithExerciseNameResponse>>> GetWorkoutTemplateExercisesWithNamesAsync(string userId, int workoutTemplateId);
    Task<Result<WorkoutTemplateExerciseWithExerciseNameResponse>> GetWorkoutTemplateExerciseByIdAsync(string userId, int workoutTemplateExerciseId);
    Task<Result> AddWorkoutTemplateExerciseAsync(string userId, int workoutTemplateId, WorkoutTemplateExerciseRequest request);
    Task<Result> UpdateWorkoutTemplateExerciseAsync(string userId, int workoutTemplateExerciseId, WorkoutTemplateExerciseRequest request);
    Task<Result> RemoveWorkoutTemplateExerciseAsync(string userId, int workoutTemplateExerciseId);
}
