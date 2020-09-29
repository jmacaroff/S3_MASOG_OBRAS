using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using EFDataAccessLibrary.DataAccess;
using Microsoft.Extensions.Options;

namespace MASOG_OBRAS
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
            //Se agrega el DB Context
            services.AddDbContext<ProductContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            ////Esto es por defecto, AddRazorRuntimeCompilation() permite modificar el html y ver los cambios sin cerrar navegador
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            services.AddSession(options => {
                options.Cookie.Name = ".MasogObras.Session";
                options.Cookie.IsEssential = true;
            });

            //services.AddDbContext<MASOG_OBRASContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("MASOG_OBRASContext")));
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
            app.UseSession();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}