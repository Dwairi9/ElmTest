using ElmBookShelf.Application.Common;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace ElmBookShelf.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddLogging();

            // Add Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            services.AddApplication(connectionString);

            services.AddLogging(loggingBuilder =>
            loggingBuilder.AddSerilog(dispose: true));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors(options => { 
                options.AllowAnyHeader();
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
            });

            app.UseRouting();
            app.UseAuthorization();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller}/{action=Index}/{id?}");
            //});

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            MapperProfile profile = new MapperProfile();
            profile.mapping();
        }
    }
}
