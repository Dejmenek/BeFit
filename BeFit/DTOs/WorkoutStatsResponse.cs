namespace BeFit.DTOs;

public record WorkoutStatsResponse(
    int TotalWorkouts,
    int Streak,
    TimeSpan TotalTimeSpent
);
