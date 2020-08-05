using DentalScheduler.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalScheduler.DAL.Configurations
{
    public class TreatmentSessionDoctorCommitmentTable 
        : IEntityTypeConfiguration<TreatmentSessionDoctorCommitment>
    {
        public void Configure(EntityTypeBuilder<TreatmentSessionDoctorCommitment> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.TreatmentSession);
            builder.HasOne(e => e.DentalWorker);

            builder.Property(e => e.TreatmentSessionId).IsRequired();
            builder.Property(e => e.DentalWorkerId).IsRequired();

            builder.HasIndex(e => new { e.TreatmentSessionId, e.DentalWorkerId }).IsUnique();
        }
    }
}