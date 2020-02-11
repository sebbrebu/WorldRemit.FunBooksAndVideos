using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorldRemit.FunBooksAndVideos.Api.BusinessRules;
using WorldRemit.FunBooksAndVideos.Api.Services;

namespace WorldRemit.FunBooksAndVideos.Api
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
            services.AddControllers();

            services.AddSingleton<ActivateMembershipRule>();
            services.AddSingleton<ProductShippingSlipRule>();

            services.AddSingleton<ICustomerAccountService, CustomerAccountService>();
            services.AddSingleton<IShippingService, ShippingService>();

            services.AddSingleton<IOrderService>(s =>
                new OrderService(new List<IBusinessRule>()
                    {
                        s.GetService<ActivateMembershipRule>(),
                        s.GetService<ProductShippingSlipRule>()
                        //Add further IBusinessRule implementations here
                    })
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
