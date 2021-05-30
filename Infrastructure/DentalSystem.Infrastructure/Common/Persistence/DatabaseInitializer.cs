using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalSystem.Entities.Identity;
using DentalSystem.Infrastructure.Migrations;
using DentalSystem.Application.Boundaries.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace DentalSystem.Infrastructure.Common.Persistence
{
    internal class DatabaseInitializer : IInitializer
    {
        private readonly DentalSystemDbContext db;
        private readonly IEnumerable<IInitialData> initialDataProviders;

        public DatabaseInitializer(
            DentalSystemDbContext db,
            IEnumerable<IInitialData> initialDataProviders)
        {
            this.db = db;
            this.initialDataProviders = initialDataProviders;
        }

        public async Task Initialize(CancellationToken cancellationToken)
        {
            var appliedMigrations = this.db.Database.GetAppliedMigrations();
            if (appliedMigrations.Any(m => m.EndsWith(nameof(Initial_Migration))))
            {
                var usersCount = await this.db.Set<User>().CountAsync();
                if (usersCount == 0)
                {
                    foreach (var initialDataProvider in this.initialDataProviders)
                    {
                        var applyData = await initialDataProvider.InitData(cancellationToken);

                        if (applyData)
                        {
                            var data = initialDataProvider.GetData();
                            this.db.AddRange(data);
                        }
                    }

                    this.db.SaveChanges();
                }
            }
        }
    }
}