namespace BeFit.Models;

public class WorkoutSession
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Notes { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;
    public ICollection<WorkoutSessionDetails> WorkoutSessionDetails { get; set; } = new List<WorkoutSessionDetails>();
}
