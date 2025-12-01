using BeFit.DTOs;

namespace BeFit.Models.ViewModels;

public class WorkoutSessionDetailDetailsViewModel
{
    public WorkoutSessionDetailResponse Detail { get; set; } = null!;
    public int WorkoutSessionId { get; set; }
}