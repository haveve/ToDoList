using GraphQL.Types;
using ToDoList.Graphql.Models;
using ToDoList.Models;
using ToDoList.ViewModel;
using ToDoList.Models.FileMethodClasses;
using static ToDoList.Models.DataMethod.DataMethod;
namespace ToDoList.Graphql.Query
{
    public class Query : ObjectGraphType
    {
        private List<Deal> SortDeals(List<Deal> _deals)
        {
            List<Deal> a = new();

            List<Deal> dealsWithDueDate = _deals.Where(d => d.DueDate != null && !d.IsComplete).OrderBy(d => d.DueDate).ToList();
            List<Deal> dealsWithOutDueDate = _deals.Where(d => d.DueDate == null && !d.IsComplete).ToList();
            List<Deal> dealsIsComplete = _deals.Where(d => d.IsComplete).ToList();

            a.AddRange(dealsWithDueDate);
            a.AddRange(dealsWithOutDueDate);
            a.AddRange(dealsIsComplete);

            return a;
        }


        public Query()
        {
            Name = "Query";
            Field<ToDoListGraphType>("getdeals")
                .Resolve(context =>
            {
                int fileMethodIndex = FromHead(context.RequestServices.GetService<IHttpContextAccessor>().HttpContext);
                var _appRepository = context.RequestServices.GetService<IFileMethod>();
                _appRepository.fileMethod(fileMethodIndex);

                var _categories = _appRepository.GetCategories();
                _categories.Insert(0, new Category { Id = -1, Name = "All Categories" });
                var _deals = SortDeals(_appRepository.GetDeals());

                var data = new ToDoListView();
                data.categories = _categories;
                data.deals = _deals;

                return data;

            });

        }
    }
}
