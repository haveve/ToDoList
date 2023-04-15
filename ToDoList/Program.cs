using Microsoft.AspNetCore.Mvc;
using System.Net;
using ToDoList.Models.DapperClasses;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //Anti Forgery  
            builder.Services.AddMvc(options => { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); });

            //Dapper
            builder.Services.AddSingleton<DapperConnectionProvider>();
            builder.Services.AddScoped<IAppRepository, AppRepository>();

            builder.Services.AddDistributedMemoryCache();// add IDistributedMemoryCache
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });  // add session


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();   // add midlware for work with session

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{Id?}",
            defaults : new
            {
                controller = "Home",
                action = "GetTask"
             }
            );

            app.Run();
        }
    }
}