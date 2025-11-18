using BeFit.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BeFit.DTOs;

public record ExerciseRequest
{
    [Required]
    [StringLength(100)]
    public string Name { get; init; } = string.Empty;

    [Required]
    public ExerciseType Category { get; init; }

    [Required]
    public MuscleType TargetMuscle { get; init; }

    [Required]
    public Difficulty Difficulty { get; init; }

    [Required]
    [StringLength(500)]
    public string Instructions { get; init; } = string.Empty;

    [StringLength(200)]
    public string? Equipment { get; init; }
}