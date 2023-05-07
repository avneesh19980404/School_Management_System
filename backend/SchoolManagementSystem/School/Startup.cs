using School.Automapper;
using School.Common.Configurations;
using School.Common.Constants;
using School.Core.Data;
using School.Middlewares;
using School.Service.Auth;
using School.Service.Client;
using School.Service.Mail;
using School.Service.Partner;
using School.Service.Repository;
using School.Service.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace School
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
            services.Configure<AppSettings>(Configuration.GetSection(DbConnection.SECRET_KEY));
            services.Configure<MailSettings>(Configuration.GetSection(DbConnection.MAIL_SETTINGS));
            services.AddCors();
            services.AddControllers();
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString(DbConnection.DefaultConnection)));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IAccount), typeof(Account));
            services.AddScoped<IClient,Client>();
            services.AddScoped<IPartner, Partner>();
            services.AddScoped<IJwtAuth,JwtAuth>();
            services.AddSingleton<IMailService, MailService>();
            services.AddAutoMapper(typeof(ObjectMapper));
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

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
            app.UseMiddleware<JwtAuthentication>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
