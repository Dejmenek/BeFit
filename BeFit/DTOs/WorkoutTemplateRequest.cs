using System.ComponentModel.DataAnnotations;

namespace BeFit.DTOs;

public record WorkoutTemplateRequest
{
    [Required]
    [StringLength(100)]
    [Display(Name = "Name")]
    public string Name { get; init; } = string.Empty;

    [Required]
    [StringLength(500)]
    [Display(Name = "Description")]
    public string Description { get; init; } = string.Empty;

    [Required]
    [StringLength(200)]
    [Display(Name = "Goals")]
    public string Goals { get; init; } = string.Empty;

    [Required]
    [Display(Name = "Preferred Day")]
    public DayOfWeek PreferredDay { get; init; }
}
