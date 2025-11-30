using System.ComponentModel.DataAnnotations;

namespace BeFit.DTOs;

public record WorkoutSessionResponse
{
    public int Id { get; init; }
    
    [Display(Name = "Date")]
    public DateTime StartDate { get; init; }
    
    public DateTime EndDate { get; init; }
    
    [Display(Name = "Notes")]
    public string Notes { get; init; } = string.Empty;
}