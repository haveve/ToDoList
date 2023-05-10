using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using System.Net;
using ToDoList.Models.DapperClasses;
using ToDoList.Models.FileMethodClasses;
using ToDoList.Models.XmlStorageClasses;
using GraphQL;
using Microsoft.AspNetCore.Builder;
using GraphQL.Introspection;
using ToDoList.Models;
using Newtonsoft.Json.Linq;
using ToDoList.ViewModel;
using GraphQL.Types;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using GraphQL.MicrosoftDI;
using ToDoList.Graphql;
using ToDoList.Graphql.Models.ErrorHandleProvider;

namespace ToDoList
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Dapper
            builder.Services.AddSingleton<DapperConnectionProvider>();
            builder.Services.AddScoped<IAppRepository, AppRepository>();

            //XmlStorage
            builder.Services.AddScoped<IXmlStorageRepository, XmlStorageRepository>();

            //ManageFileMethod
            builder.Services.AddScoped<IFileMethod, FileMethod>();


            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSingleton<ISchema, ToDoListScheme>(services => new ToDoListScheme(new SelfActivatingServiceProvider(services)));

            builder.Services.AddGraphQL(c => c.AddSystemTextJson()
                                              .AddErrorInfoProvider<ErrorHandleProvider>());


            var app = builder.Build();

            app.UseHttpsRedirection();
            app.UseGraphQL();
            app.UseGraphQLAltair();

            app.Run();
        }
    }

}