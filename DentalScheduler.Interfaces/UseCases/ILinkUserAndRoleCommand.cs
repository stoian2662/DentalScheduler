using System.Threading.Tasks;
using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.Models.Output.Common;

namespace DentalScheduler.Interfaces.UseCases
{
    public interface ILinkUserAndRoleCommand
    {
        Task<IResult<IMessageOutput>> ExecuteAsync(ILinkUserAndRoleInput userInput);
    }
}