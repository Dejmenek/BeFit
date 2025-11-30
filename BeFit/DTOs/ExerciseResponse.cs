using BeFit.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BeFit.DTOs;

public record ExerciseResponse
{
    public int Id { get; init; }
    
    [Display(Name = "Exercise Name")]
    public string Name { get; init; } = string.Empty;
    
    [Display(Name = "Category")]
    public ExerciseType Category { get; init; }
    
    [Display(Name = "Target Muscle")]
    public MuscleType TargetMuscle { get; init; }
    
    [Display(Name = "Difficulty")]
    public Difficulty Difficulty { get; init; }
    
    [Display(Name = "Instructions")]
    public string Instructions { get; init; } = string.Empty;
    
    [Display(Name = "Equipment")]
    public string? Equipment { get; init; }
}