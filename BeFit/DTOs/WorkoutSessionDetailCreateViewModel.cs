using System.ComponentModel.DataAnnotations;

namespace BeFit.DTOs;

public class WorkoutSessionDetailCreateViewModel
{
    public WorkoutSessionDetailRequest Request { get; set; } = new();
    public List<ExerciseResponse> Exercises { get; set; } = new();
    public int WorkoutSessionId { get; set; }
}

