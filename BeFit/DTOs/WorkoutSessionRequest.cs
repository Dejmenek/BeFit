using System.ComponentModel.DataAnnotations;

namespace BeFit.DTOs;

public record WorkoutSessionRequest : IValidatableObject
{
    [Display(Name = "Start Date & Time")]
    public DateTime StartDate { get; init; }
    
    [Display(Name = "End Date & Time")]
    public DateTime EndDate { get; init; }
    
    [Display(Name = "Notes")]
    public string Notes { get; init; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (EndDate <= StartDate)
        {
            yield return new ValidationResult(
                "End date cannot be before start date.",
                new[] { nameof(EndDate) }
            );
        }

        if (EndDate > StartDate.AddDays(1))
        {
            yield return new ValidationResult(
                "End date must be no more than one day after the start date.",
                new[] { nameof(EndDate) }
            );
        }
    }
}