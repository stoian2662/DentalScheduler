using System;
using DentalSystem.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace DentalSystem.Entities.Scheduling
{
    public class Patient
    {
        public Guid Id { get; set; }

        public Guid ReferenceId { get; set; }

        public string IdentityUserId { get; set; }

        public virtual User IdentityUser { get; set; }
    }
}