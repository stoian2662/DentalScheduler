using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using DentalScheduler.Config.DI;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;
using DentalScheduler.Web.UI.Services;
using Radzen;
using DentalScheduler.Config.Mappings;

namespace DentalScheduler.Web.UI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Simple.OData.Client.V4Adapter.Reference();

            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var appSettingSection = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppSettings>(
                (appSettings) => appSettings.ApiBaseAddress = appSettingSection.GetSection("ApiBaseAddress").Value
            );

            RegisterServices(builder.Services);

            builder.Services.RegisterDependencies();

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<AuthenticationStateProvider, ApplicationAuthenticationStateProvider>();
            builder.Services.AddScoped<ApplicationAuthenticationStateProvider, ApplicationAuthenticationStateProvider>();
            
            builder.Services.AddTransient(sp => new HttpClient());

            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();

            builder.RootComponents.Add<App>("app");

            await builder.Build().RunAsync();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IScheduleService, ScheduleService>();
            services.AddTransient<IDentalTeamService, DentalTeamService>();
        }
    }
}
