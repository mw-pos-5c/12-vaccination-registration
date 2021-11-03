#region usings

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using VaccReg.Services;

using VaccRegDb;

#endregion

namespace VaccReg
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();
                app.UseRouting();
                app.UseAuthorization();
                app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<VaccRegContext>(options => options.UseSqlite(@"Data Source=Resources/vaccinations.db"));
            services.AddControllers();

            services.AddHostedService<VaccRegService>();
        }
    }

}