using Dimensional.TinyReturns.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TinyReturns.Core;
using TinyReturns.Core.DataRepositories;
using TinyReturns.Core.PublicWebSite;
using TinyReturnsCore.Helpers;

namespace TinyReturnsCore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISystemLog, SystemLogNoOp>();
            services.AddSingleton<IInvestmentVehicleDataGateway, TinyReturnsDatabase>();
            services.AddSingleton<IReturnsSeriesDataGateway, TinyReturnsDatabase>();
            services.AddSingleton<IMonthlyReturnsDataGateway, TinyReturnsDatabase>();
            services.AddSingleton<IInvestmentVehicleReturnsRepository, InvestmentVehicleReturnsRepository>();
            services.AddTransient<PortfolioListPageAdapter>();
            services.AddSingleton<ITinyReturnsDatabaseSettings, WebSettings>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
