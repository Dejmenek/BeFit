namespace BeFit.Models;

public class WorkoutTemplate
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Goals { get; set; } = string.Empty;
    public DayOfWeek PreferredDay { get; set; }

    public ApplicationUser User { get; set; } = null!;
    public ICollection<WorkoutTemplateExercise> WorkoutTemplateExercises { get; set; } = new List<WorkoutTemplateExercise>();
}
