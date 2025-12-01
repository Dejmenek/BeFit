using BeFit.DTOs;
using BeFit.Results;

namespace BeFit.Models.ViewModels;

public class WorkoutSessionDetailsViewModel
{
    public WorkoutSessionResponse Session { get; set; } = null!;
    public PaginatedList<WorkoutSessionDetailResponse> Details { get; set; } = null!;
}