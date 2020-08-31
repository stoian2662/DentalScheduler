using System;
using Microsoft.AspNetCore.Identity;

namespace DentalScheduler.Entities
{
    public class Patient
    {
        public Guid Id { get; set; }

        public Guid ReferenceId { get; set; }

        public string IdentityUserId { get; set; }

        public virtual IdentityUser IdentityUser { get; set; }
    }
}