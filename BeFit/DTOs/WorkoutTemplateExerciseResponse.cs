namespace BeFit.DTOs;

public record WorkoutTemplateExerciseResponse(
    int Id,
    int WorkoutTemplateId,
    int ExerciseId,
    int Order,
    int? TargetSets,
    int? TargetReps,
    decimal? TargetWeight,
    int RestTimeInSeconds,
    string? Tempo,
    decimal? TargetDistance,
    int? TargetDurationInSeconds
);
