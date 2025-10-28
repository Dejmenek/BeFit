using BeFit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeFit.Data.Configurations;

internal sealed class WorkoutTemplateConfiguration : IEntityTypeConfiguration<WorkoutTemplate>
{
    public void Configure(EntityTypeBuilder<WorkoutTemplate> builder)
    {
        builder.HasKey(wt => wt.Id);

        builder.Property(wt => wt.Name)
               .HasMaxLength(100);

        builder.Property(wt => wt.Description)
               .HasMaxLength(500);

        builder.Property(wt => wt.Goals)
               .HasMaxLength(250);

        builder.Property(wt => wt.PreferredDay)
               .HasConversion<string>()
               .HasMaxLength(15)
               .IsRequired();
    }
}
