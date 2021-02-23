//@QnSCodeCopy
//MdStart
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QnSTradingCompany.BlazorApp.Modules.SessionStorage;
using QnSTradingCompany.BlazorApp.Services;
using QnSTradingCompany.BlazorApp.Services.Modules.Authentication;
using QnSTradingCompany.BlazorApp.Services.Modules.Configuration;
using QnSTradingCompany.BlazorApp.Services.Modules.Language;
using Radzen;

namespace QnSTradingCompany.BlazorApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddProtectedBrowserStorage();

            services.AddScoped<IProtectedBrowserStorage, ProtectedLocalStorage>();
            services.AddScoped<IServiceAdapter, ServiceAdapter>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ITranslatorService, TranslatorService>();

            services.AddScoped<AuthenticationStateProviderService>();
            services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<AuthenticationStateProviderService>());
            services.AddScoped<Modules.App.AppState>();

            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
//MdEnd
