using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using DentalScheduler.Web.UI.Models;
using Microsoft.Extensions.Options;

namespace DentalScheduler.Web.UI.Services
{
    public class TreatmentService : BaseDataService, ITreatmentService
    {
        public TreatmentService(
                HttpClient httpClient, 
                IOptions<AppSettings> appSettings,
                ILocalStorageService localStorage)
            : base(httpClient, localStorage)
        {
            AppSettings = appSettings.Value;

            HttpClient.BaseAddress = new Uri($"{AppSettings.ApiBaseAddress}odata/");
        }

        public AppSettings AppSettings { get; }

        public async Task<IList<TreatmentDropDownViewModel>> GetTreatmentsAsync()
        {
            await SetAccessTokenAsync();

            var result = await ODataClient
                .For<TreatmentDropDownViewModel>("Treatment")
                .FindEntriesAsync();

            return result.ToList();
        }
    }
}