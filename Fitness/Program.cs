//using Fitness.Models;
using Fitness.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using static Fitness.Controllers.UserPaymentController;

namespace Fitness
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddSingleton<EmailService>();
            //builder.Services.AddSingleton<PdfService>();

            builder.Services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
           builder.Services.AddDbContext<ModelContext>(options => options.UseOracle(builder.Configuration.GetConnectionString("FitnessDBSystemConnection")));
			builder.Services.AddDistributedMemoryCache();
			builder.Services.AddDbContext<ModelContext>(options => 	options.UseSqlServer("FitnessDBSystemConnection")
		   .EnableSensitiveDataLogging());

			builder.Services.AddSession(options => {
				options.IdleTimeout = TimeSpan.FromMinutes(60);
				options.Cookie.HttpOnly = true;
				options.Cookie.IsEssential = true;
			});


			var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //builder.Services.AddDbContext<ModelContext>(options => options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
			app.UseSession();

			app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}