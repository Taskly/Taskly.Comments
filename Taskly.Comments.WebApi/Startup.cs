using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Taskly.Comments.Application;

namespace Taskly.Comments.WebApi
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
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddDbContext<CommentsDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerDocumentation();
            services.AddControllers()
                .AddJsonOptions(opts => { opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CommentsDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwaggerDocumentation();
            app.UseExceptionMiddleware();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            // WARN! Not working with migrations.
            dbContext.Database.EnsureCreated();
        }
    }
}
