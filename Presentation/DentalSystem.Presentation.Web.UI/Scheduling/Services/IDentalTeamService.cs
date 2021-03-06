using System.Collections.Generic;
using System.Threading.Tasks;
using DentalSystem.Presentation.Web.UI.Models;

namespace DentalSystem.Presentation.Web.UI.Scheduling.Services
{
    public interface IDentalTeamService
    {
        Task<IList<DentalTeamDropDownViewModel>> GetDentalTeamsDropDownListAsync();
    }
}