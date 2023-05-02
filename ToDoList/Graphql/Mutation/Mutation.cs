using GraphQL;
using GraphQL.Types;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Graphql.Models;
using ToDoList.Models;
using ToDoList.Models.FileMethodClasses;
using ToDoList.ViewModel;
using ToDoList.Graphql.Models.InputModel;
namespace ToDoList.Graphql.Mutation
{
    public class Mutation : ObjectGraphType
    {
        public Mutation() 
        {
            Name = "Mutation";
            Field<StringGraphType>("SortTask")
                .Argument<NonNullGraphType<IntGraphType>>("CategorySort")
                .Resolve(context =>
                {
                    var HttpContext = context.RequestServices.GetService<IHttpContextAccessor>().HttpContext;
                    var CategorySort = context.GetArgument<int>("CategorySort");
                    HttpContext.Response.Cookies.Append("CategorySort", CategorySort.ToString());
                    var a = "successful";
                    return a;
                });

            Field<StringGraphType>("CategoryAdd")
                .Argument<NonNullGraphType<StringGraphType>>("CategoryV")
                .ResolveAsync(async context =>
                {
                    var _appRepository = context.RequestServices.GetService<IFileMethod>();
                    var CategoryV = context.GetArgument<string>("CategoryV");
                        await _appRepository.AddCategory(new() { Name = CategoryV });
                        return "successfully";
                   
                });

            Field<StringGraphType>("TaskAdd")
               .Argument<NonNullGraphType<DealViewInputType>>("DealV")
               .ResolveAsync(async context =>
               {
                   var _appRepository = context.RequestServices.GetService<IFileMethod>();
                   var DealV = context.GetArgument<DealView>("DealV");
                   await _appRepository.AddDeal(DealV);
                   return "successfully";

               });

            Field<StringGraphType>("UpdateDeal")
               .Argument<NonNullGraphType<DealInputType>>("DealV")
               .ResolveAsync(async context =>
               {
                   var _appRepository = context.RequestServices.GetService<IFileMethod>();
                   var DealV = context.GetArgument<Deal>("DealV");
                   await _appRepository.UpdateDeal(DealV);
                   return "successfully";

               });

            Field<StringGraphType>("DeleteDeal")
              .Argument<NonNullGraphType<IntGraphType>>("Id")
              .ResolveAsync(async context =>
              {
                  var _appRepository = context.RequestServices.GetService<IFileMethod>();
                  var Id = context.GetArgument<int>("Id");
                  await _appRepository.DeleteDeal(Id);
                  return "successfully";

              });


            Field<StringGraphType>("UpdateDealState")
              .Argument<NonNullGraphType<IntGraphType>>("Id")
              .ResolveAsync(async context =>
              {
                  var _appRepository = context.RequestServices.GetService<IFileMethod>();
                  var Id = context.GetArgument<int>("Id");
                  await _appRepository.DeleteDeal(Id);
                  Deal deal = _appRepository.GetDeals().First(d => d.Id == Id);
                  _appRepository.UpdateStateDeal(Id, !deal.IsComplete);
                  return "successfully";

              });

            Field<StringGraphType>("FileMethod")
             .Argument<NonNullGraphType<IntGraphType>>("Id")
             .Resolve(context =>
             {
                 var HttpContext = context.RequestServices.GetService<IHttpContextAccessor>().HttpContext;
                 var Id = context.GetArgument<int>("Id");
                 HttpContext.Response.Cookies.Append("FileMethod", Id.ToString());
                 HttpContext.Response.Cookies.Delete("CategorySort");
                 return "successfully";

             });

        }

    }
}
