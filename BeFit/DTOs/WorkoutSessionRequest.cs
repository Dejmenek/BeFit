using System.ComponentModel.DataAnnotations;

namespace BeFit.DTOs;

public record WorkoutSessionRequest : IValidatableObject
{
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
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