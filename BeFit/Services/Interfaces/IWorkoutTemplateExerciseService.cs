using BeFit.DTOs;
using BeFit.Models;
using BeFit.Results;

namespace BeFit.Services.Interfaces;

public interface IWorkoutTemplateExerciseService
{
    Task<Result<List<WorkoutTemplateExerciseResponse>>> GetWorkoutTemplateExercisesAsync(int workoutTemplateId);
    Task<Result<List<WorkoutTemplateExerciseWithExerciseNameResponse>>> GetWorkoutTemplateExercisesWithNamesAsync(int workoutTemplateId);
    Task<Result<WorkoutTemplateExerciseWithExerciseNameResponse>> GetWorkoutTemplateExerciseByIdAsync(int workoutTemplateExerciseId);
    Task<Result> AddWorkoutTemplateExerciseAsync(int workoutTemplateId, WorkoutTemplateExerciseRequest request);
    Task<Result> UpdateWorkoutTemplateExerciseAsync(int workoutTemplateExerciseId, WorkoutTemplateExerciseRequest request);
    Task<Result> RemoveWorkoutTemplateExerciseAsync(int workoutTemplateExerciseId);
}
