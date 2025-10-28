using BeFit.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeFit.Data.Configurations;

internal sealed class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
               .HasMaxLength(100);

        builder.Property(e => e.Instructions)
               .HasMaxLength(500);

        builder.Property(e => e.Equipment)
               .HasMaxLength(200);

        builder.Property(e => e.Category)
               .HasConversion<string>()
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(e => e.TargetMuscle)
               .HasConversion<string>()
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(e => e.Difficulty)
               .HasConversion<string>()
               .HasMaxLength(50)
               .IsRequired();
    }
}
