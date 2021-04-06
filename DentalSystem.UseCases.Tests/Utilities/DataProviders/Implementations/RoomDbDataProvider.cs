using System.Threading.Tasks;
using DentalSystem.Entities;
using DentalSystem.Interfaces.Infrastructure.Common.Persistence;

namespace DentalSystem.UseCases.Tests.Utilities.DataProviders
{
    public class RoomDbDataProvider : IRoomDbDataProvider
    {
        public RoomDbDataProvider(
            IGenericRepository<Room> repository,
            IUnitOfWork uoW)
        {
            Repository = repository;
            UoW = uoW;
        }

        public IGenericRepository<Room> Repository { get; }

        public IUnitOfWork UoW { get; }

        public async Task<Room> ProvideRoomAsync(string name)
        {
            var room = new Room()
            {
                Name = name
            };

            await Repository.AddAsync(room);

            await UoW.SaveAsync();

            return room;
        }
    }
}