using System.ComponentModel.DataAnnotations;

namespace BeFit.DTOs;

public record WorkoutTemplateRequest
{
    [Required]
    [StringLength(100)]
    public string Name { get; init; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string Description { get; init; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Goals { get; init; } = string.Empty;

    [Required]
    public DayOfWeek PreferredDay { get; init; }
}
