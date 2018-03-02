using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebAPIServer.Models;
using WebAPIServer.Services;

namespace WebAPIServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {           
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddRepoServices();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddCors();
            services.AddMvc();
        }

        
        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(builder => builder.AllowAnyOrigin());
            app.UseMvc();          
        }

       
    }
}
