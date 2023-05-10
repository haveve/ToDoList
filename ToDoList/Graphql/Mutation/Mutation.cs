using GraphQL;
using GraphQL.Types;
using ToDoList.Models;
using ToDoList.Models.FileMethodClasses;
using ToDoList.ViewModel;
using ToDoList.Graphql.Models.InputModel;
using static ToDoList.Models.DataMethod.DataMethod;
namespace ToDoList.Graphql.Mutation
{
    public class Mutation : ObjectGraphType
    {
        public Mutation()
        {
            Name = "Mutation";
            Field<StringGraphType>("CategoryAdd")
                .Argument<NonNullGraphType<StringGraphType>>("CategoryV")
                .ResolveAsync(async context =>
                {
                    int fileMethodIndex = FromHead(context.RequestServices.GetService<IHttpContextAccessor>().HttpContext);
                    var _appRepository = context.RequestServices.GetService<IFileMethod>();
                    _appRepository.fileMethod(fileMethodIndex);
                    var CategoryV = context.GetArgument<string>("CategoryV");
                    await _appRepository.AddCategory(new() { Name = CategoryV });
                    return "successfully";

                });

            Field<StringGraphType>("TaskAdd")
               .Argument<NonNullGraphType<DealViewInputType>>("DealV")
               .ResolveAsync(async context =>
               {
                   int fileMethodIndex = FromHead(context.RequestServices.GetService<IHttpContextAccessor>().HttpContext);
                   var _appRepository = context.RequestServices.GetService<IFileMethod>();
                   _appRepository.fileMethod(fileMethodIndex);
                   var DealV = context.GetArgument<DealView>("DealV");
                   if (DealV != null)
                       await _appRepository.AddDeal(DealV);
                   return "successfully";

               });

            Field<StringGraphType>("UpdateDeal")
               .Argument<NonNullGraphType<DealInputType>>("DealV")
               .ResolveAsync(async context =>
               {
                   int fileMethodIndex = FromHead(context.RequestServices.GetService<IHttpContextAccessor>().HttpContext);
                   var _appRepository = context.RequestServices.GetService<IFileMethod>();
                   _appRepository.fileMethod(fileMethodIndex);
                   var DealV = context.GetArgument<Deal>("DealV");
                   if (DealV != null)
                       await _appRepository.UpdateDeal(DealV);
                   return "successfully";

               });

            Field<StringGraphType>("DeleteDeal")
              .Argument<NonNullGraphType<IntGraphType>>("Id")
              .ResolveAsync(async context =>
              {
                  int fileMethodIndex = FromHead(context.RequestServices.GetService<IHttpContextAccessor>().HttpContext);
                  var _appRepository = context.RequestServices.GetService<IFileMethod>();
                  _appRepository.fileMethod(fileMethodIndex);
                  var Id = context.GetArgument<int>("Id");
                  await _appRepository.DeleteDeal(Id);
                  return "successfully";

              });


            Field<StringGraphType>("UpdateDealState")
              .Argument<NonNullGraphType<IntGraphType>>("Id")
              .ResolveAsync(async context =>
              {
                  int fileMethodIndex = FromHead(context.RequestServices.GetService<IHttpContextAccessor>().HttpContext);
                  var _appRepository = context.RequestServices.GetService<IFileMethod>();
                  _appRepository.fileMethod(fileMethodIndex);
                  var Id = context.GetArgument<int>("Id");
                  await _appRepository.DeleteDeal(Id);
                  Deal deal = _appRepository.GetDeals().First(d => d.Id == Id);
                  _appRepository.UpdateStateDeal(Id, !deal.IsComplete);
                  return "successfully";

              });

        }

    }
}
