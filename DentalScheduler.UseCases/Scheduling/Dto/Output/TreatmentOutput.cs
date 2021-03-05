using System;
using DentalScheduler.Interfaces.UseCases.Scheduling.Dto.Output;

namespace DentalScheduler.UseCases.Scheduling.Dto.Output
{
    public class TreatmentOutput : ITreatmentOutput
    {
        public Guid ReferenceId { get; set; }

        public string Name { get; set; }

        public int DurationInMinutes { get; set; }
    }
}