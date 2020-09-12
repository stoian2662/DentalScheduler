using DentalScheduler.DAL.Helpers;
using DentalScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalScheduler.DAL.Configurations
{
    public class TreatmentSessionTable : IEntityTypeConfiguration<TreatmentSession>
    {
        public void Configure(EntityTypeBuilder<TreatmentSession> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.ReferenceId).HasValueGenerator<ReferenceIdGenerator>();

            builder.HasOne(e => e.Patient);
            builder.HasOne(e => e.DentalTeam);
            builder.HasOne(e => e.Treatment);

            builder.Property(e => e.PatientId).IsRequired();
            builder.Property(e => e.DentalTeamId).IsRequired();
            builder.Property(e => e.TreatmentId).IsRequired();
            builder.Property(e => e.Start).IsRequired();
            builder.Property(e => e.End).IsRequired();

            builder.Property(e => e.Status).HasDefaultValue(TreatmentSessionStatus.Requested);

            builder.HasIndex(e => e.ReferenceId).IsUnique();
            builder.HasIndex(e => new { e.PatientId, e.Start, e.End }).IsUnique();
            builder.HasIndex(e => new { e.DentalTeamId, e.Start, e.End }).IsUnique();
        }
    }
}