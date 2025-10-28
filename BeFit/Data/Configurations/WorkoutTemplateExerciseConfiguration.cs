using BeFit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeFit.Data.Configurations;

internal sealed class WorkoutTemplateExerciseConfiguration : IEntityTypeConfiguration<WorkoutTemplateExercise>
{
    public void Configure(EntityTypeBuilder<WorkoutTemplateExercise> builder)
    {
        builder.HasKey(wte => wte.Id);

        builder.Property(wte => wte.Tempo)
            .HasMaxLength(32);

        builder.Property(wte => wte.TargetWeight)
            .HasPrecision(6, 2);

        builder.Property(wte => wte.TargetDistance)
            .HasPrecision(6, 2);

        builder.HasIndex(wte => new { wte.WorkoutTemplateId, wte.Order })
            .IsUnique();

        builder.ToTable(tb =>
        {
            tb.HasCheckConstraint(
                $"CK_WorkoutTemplateExercise_{nameof(WorkoutTemplateExercise.TargetSets)}_NonNegative",
                $"[{nameof(WorkoutTemplateExercise.TargetSets)}] IS NULL OR [{nameof(WorkoutTemplateExercise.TargetSets)}] >= 0");
            tb.HasCheckConstraint(
                $"CK_WorkoutTemplateExercise_{nameof(WorkoutTemplateExercise.TargetReps)}_NonNegative",
                $"[{nameof(WorkoutTemplateExercise.TargetReps)}] IS NULL OR [{nameof(WorkoutTemplateExercise.TargetReps)}] >= 0");
            tb.HasCheckConstraint(
                $"CK_WorkoutTemplateExercise_{nameof(WorkoutTemplateExercise.TargetWeight)}_NonNegative",
                $"[{nameof(WorkoutTemplateExercise.TargetWeight)}] IS NULL OR [{nameof(WorkoutTemplateExercise.TargetWeight)}] >= 0");
            tb.HasCheckConstraint(
                $"CK_WorkoutTemplateExercise_{nameof(WorkoutTemplateExercise.RestTimeInSeconds)}_NonNegative",
                $"[{nameof(WorkoutTemplateExercise.RestTimeInSeconds)}] >= 0");
            tb.HasCheckConstraint(
                $"CK_WorkoutTemplateExercise_{nameof(WorkoutTemplateExercise.TargetDistance)}_NonNegative",
                $"[{nameof(WorkoutTemplateExercise.TargetDistance)}] IS NULL OR [{nameof(WorkoutTemplateExercise.TargetDistance)}] >= 0");
            tb.HasCheckConstraint(
                $"CK_WorkoutTemplateExercise_{nameof(WorkoutTemplateExercise.TargetDurationInSeconds)}_NonNegative",
                $"[{nameof(WorkoutTemplateExercise.TargetDurationInSeconds)}] IS NULL OR [{nameof(WorkoutTemplateExercise.TargetDurationInSeconds)}] >= 0");
            tb.HasCheckConstraint(
                $"CK_WorkoutTemplateExercise_{nameof(WorkoutTemplateExercise.Order)}_Positive",
                $"[{nameof(WorkoutTemplateExercise.Order)}] > 0");
        });
    }
}
