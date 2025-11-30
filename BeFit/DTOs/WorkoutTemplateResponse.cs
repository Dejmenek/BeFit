namespace BeFit.DTOs;

public record WorkoutTemplateResponse(
    int Id,
    string Name,
    string Description,
    string Goals,
    DayOfWeek PreferredDay
);
