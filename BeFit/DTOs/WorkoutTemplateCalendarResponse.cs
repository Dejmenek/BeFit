namespace BeFit.DTOs;

public record WorkoutTemplateCalendarResponse
{
    public string TemplateName { get; init; } = string.Empty;
    public DayOfWeek DayOfWeek { get; init; }
    public List<string> ExerciseNames { get; init; } = new();
}
