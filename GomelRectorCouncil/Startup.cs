﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
//using MySQL.Data.EntityFrameworkCore.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GomelRectorCouncil.Data;
using GomelRectorCouncil.Models;
using GomelRectorCouncil.Services;
using GomelRectorCouncil.Settings;
using GomelRectorCouncil.Middleware;
using Microsoft.AspNetCore.Identity;

namespace GomelRectorCouncil
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // Этот метод вызывается во время выполнения. Используйте этот метод для добавления сервисов в контейнер..
        public void ConfigureServices(IServiceCollection services)
        {
            // Добавьте службы инфраструктуры.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<CouncilDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<CouncilDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("CouncilConnectionSQL")));
           //services.AddDbContext<CouncilDbContext>(options =>
           //     options.UseMySQL(Configuration.GetConnectionString("CouncilConnectionMysql")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


            //добавление сессии
            services.AddDistributedMemoryCache();
            services.AddSession();


            //Добавление сервиса конфигураций
            services.AddSingleton<IConfiguration>(Configuration);

            // Чтение настроек электронной почты
            services.Configure<EmailConfig>(Configuration.GetSection("Email"));
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();

            //Использование MVC
            services.AddControllersWithViews();
            services.AddRazorPages();


        }

        // Этот метод вызывается во время выполнения. Используйте этот метод для настройки конвейера HTTP-запросов.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            // использование статических файлов
            app.UseStaticFiles();

            // использование Identity
            app.UseAuthentication();


            // добавляем поддержку сессий
            app.UseSession();

            // добавляем компонента miidleware по инициализации базы данных по университетам
            // демонстрационный пример - вряд ли стоит так делать в реальном приложении :)
            app.UseDbInitializer();

            app.UseRouting();

            // использование Identity
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}




        
       