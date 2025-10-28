using BeFit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeFit.Data.Configurations;

internal sealed class WorkoutSessionDetailsConfiguration : IEntityTypeConfiguration<WorkoutSessionDetails>
{
    public void Configure(EntityTypeBuilder<WorkoutSessionDetails> builder)
    {
        builder.ToTable(b =>
        {
            b.HasCheckConstraint(
                $"CK_WorkoutSessionDetails_{nameof(WorkoutSessionDetails.RestTimeInSeconds)}",
                $"[{nameof(WorkoutSessionDetails.RestTimeInSeconds)}] >= 0"
            );
            b.HasCheckConstraint(
                $"CK_WorkoutSessionDetails_{nameof(WorkoutSessionDetails.Sets)}",
                $"[{nameof(WorkoutSessionDetails.Sets)}] IS NULL OR [{nameof(WorkoutSessionDetails.Sets)}] > 0"
            );
            b.HasCheckConstraint(
                $"CK_WorkoutSessionDetails_{nameof(WorkoutSessionDetails.Repetitions)}",
                $"[{nameof(WorkoutSessionDetails.Repetitions)}] IS NULL OR [{nameof(WorkoutSessionDetails.Repetitions)}] > 0"
            );
            b.HasCheckConstraint(
                $"CK_WorkoutSessionDetails_{nameof(WorkoutSessionDetails.Weight)}",
                $"[{nameof(WorkoutSessionDetails.Weight)}] IS NULL OR [{nameof(WorkoutSessionDetails.Weight)}] >= 0"
            );
            b.HasCheckConstraint(
                $"CK_WorkoutSessionDetails_{nameof(WorkoutSessionDetails.DurationInSeconds)}",
                $"[{nameof(WorkoutSessionDetails.DurationInSeconds)}] IS NULL OR [{nameof(WorkoutSessionDetails.DurationInSeconds)}] >= 0"
            );
            b.HasCheckConstraint(
                $"CK_WorkoutSessionDetails_{nameof(WorkoutSessionDetails.Distance)}",
                $"[{nameof(WorkoutSessionDetails.Distance)}] IS NULL OR [{nameof(WorkoutSessionDetails.Distance)}] >= 0"
            );
        });

        builder.HasKey(wsd => wsd.Id);

        builder.Property(wsd => wsd.Weight)
            .HasPrecision(6, 2);

        builder.Property(wsd => wsd.Distance)
            .HasPrecision(6, 2);

        builder.Property(wsd => wsd.Tempo)
            .HasMaxLength(32);
    }
}
