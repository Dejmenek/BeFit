using BeFit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeFit.Data.Configurations;

internal sealed class WorkoutSessionConfiguration : IEntityTypeConfiguration<WorkoutSession>
{
    public void Configure(EntityTypeBuilder<WorkoutSession> builder)
    {
        builder.ToTable(b => b.HasCheckConstraint(
            "CK_WorkoutSession_Dates",
            $"[{nameof(WorkoutSession.EndDate)}] >= [{nameof(WorkoutSession.StartDate)}]")

        );
        builder.Property(ws => ws.Notes)
               .HasMaxLength(500);

        builder.Property(ws => ws.StartDate)
               .HasColumnType("smalldatetime");

        builder.Property(ws => ws.EndDate)
               .HasColumnType("smalldatetime");

        builder.HasKey(ws => ws.Id);
    }
}
