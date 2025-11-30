using BeFit.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BeFit.DTOs;

public record ExerciseRequest
{
    [Required]
    [StringLength(100)]
    [Display(Name = "Exercise Name")]
    public string Name { get; init; } = string.Empty;

    [Required]
    [Display(Name = "Category")]
    public ExerciseType Category { get; init; }

    [Required]
    [Display(Name = "Target Muscle")]
    public MuscleType TargetMuscle { get; init; }

    [Required]
    [Display(Name = "Difficulty Level")]
    public Difficulty Difficulty { get; init; }

    [Required]
    [StringLength(500)]
    [Display(Name = "Instructions")]
    public string Instructions { get; init; } = string.Empty;

    [StringLength(200)]
    [Display(Name = "Equipment")]
    public string? Equipment { get; init; }
}