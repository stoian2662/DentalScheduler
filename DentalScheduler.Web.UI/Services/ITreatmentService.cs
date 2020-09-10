using System.Collections.Generic;
using System.Threading.Tasks;
using DentalScheduler.Web.UI.Models;

namespace DentalScheduler.Web.UI.Services
{
    public interface ITreatmentService
    {
        Task<IList<TreatmentDropDownViewModel>> GetTreatmentsAsync();
    }
}