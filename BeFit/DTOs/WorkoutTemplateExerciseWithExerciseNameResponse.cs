namespace BeFit.DTOs;

public record WorkoutTemplateExerciseWithExerciseNameResponse(
    int Id,
    int WorkoutTemplateId,
    int ExerciseId,
    string ExerciseName,
    int Order,
    int? TargetSets,
    int? TargetReps,
    decimal? TargetWeight,
    int RestTimeInSeconds,
    string? Tempo,
    decimal? TargetDistance,
    int? TargetDurationInSeconds
);

