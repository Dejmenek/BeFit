namespace BeFit.DTOs;

public record WorkoutSessionResponse(int Id, DateTime StartDate, DateTime EndDate, string Notes);