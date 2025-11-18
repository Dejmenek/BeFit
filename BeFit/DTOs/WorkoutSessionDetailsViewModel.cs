using BeFit.Results;

namespace BeFit.DTOs;

public class WorkoutSessionDetailsViewModel
{
    public WorkoutSessionResponse Session { get; set; } = null!;
    public PaginatedList<WorkoutSessionDetailResponse> Details { get; set; } = null!;
}

