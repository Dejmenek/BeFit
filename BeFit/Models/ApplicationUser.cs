using Microsoft.AspNetCore.Identity;

namespace BeFit.Models;

public class ApplicationUser : IdentityUser
{
    public ICollection<WorkoutSession> WorkoutSessions { get; set; } = new List<WorkoutSession>();
    public ICollection<WorkoutTemplate> WorkoutTemplates { get; set; } = new List<WorkoutTemplate>();
}
