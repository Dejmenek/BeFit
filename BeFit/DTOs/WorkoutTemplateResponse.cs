using System.ComponentModel.DataAnnotations;

namespace BeFit.DTOs;

public record WorkoutTemplateResponse
{
    public int Id { get; init; }
    
    [Display(Name = "Name")]
    public string Name { get; init; } = string.Empty;
    
    [Display(Name = "Description")]
    public string Description { get; init; } = string.Empty;
    
    [Display(Name = "Goals")]
    public string Goals { get; init; } = string.Empty;
    
    [Display(Name = "Preferred Day")]
    public DayOfWeek PreferredDay { get; init; }
}
