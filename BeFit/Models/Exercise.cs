using BeFit.Models.Enums;

namespace BeFit.Models;

public class Exercise
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ExerciseType Category { get; set; }
    public MuscleType TargetMuscle { get; set; }
    public Difficulty Difficulty { get; set; }
    public string Instructions { get; set; } = string.Empty;
    public string? Equipment { get; set; }

    public ICollection<WorkoutSessionDetails> WorkoutSessionDetails { get; set; } = new List<WorkoutSessionDetails>();
    public ICollection<WorkoutTemplateExercise> WorkoutTemplateExercises { get; set; } = new List<WorkoutTemplateExercise>();
}
