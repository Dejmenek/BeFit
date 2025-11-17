using BeFit.Models.Enums;

namespace BeFit.DTOs;

public class ExerciseStatsResponse
{
    public string Name { get; set; }
    public ExerciseType Category { get; set; }
    public MuscleType TargetMuscle { get; set; }
    public int SessionsTrained { get; set; }
    public int TotalReps { get; set; }
    public decimal AverageWeight { get; set; }
    public decimal MaxWeight { get; set; }
}
