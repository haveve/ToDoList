using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using System.Net;
using ToDoList.Models.DapperClasses;
using ToDoList.Models.FileMethodClasses;
using ToDoList.Models.XmlStorageClasses;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Anti Forgery and add MVC  
            builder.Services.AddMvc(options => { options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); });

            //Dapper
            builder.Services.AddSingleton<DapperConnectionProvider>();
            builder.Services.AddScoped<IAppRepository, AppRepository>();

            //XmlStorage
            builder.Services.AddScoped<IXmlStorageRepository, XmlStorageRepository>();

            //ManageFileMethod
            builder.Services.AddScoped<IFileMethod, FileMethod>();


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


            //Rewrite rout when user wanna Update Task state when that is in updating state
            var options = new RewriteOptions().AddRedirect(@"Home/GetTask/Home/UpdateDealState/(\d+)", "Home/UpdateDealState?Id=$1");
            app.UseRewriter(options);

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