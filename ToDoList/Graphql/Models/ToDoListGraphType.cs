using GraphQL.Types;
using ToDoList.ViewModel;

namespace ToDoList.Graphql.Models
{
    public class ToDoListGraphType : ObjectGraphType<ToDoListView>
    {
        public ToDoListGraphType()
        {
            Field(d => d.categories, type: typeof(ListGraphType<CategoryGraghType>));
            Field(d => d.deals, type: typeof(ListGraphType<DealGraghType>));

        }
    }
}
