using GraphQL.Types;
using ToDoList.ViewModel;

namespace ToDoList.Graphql.Models.InputModel
{
    public class DealViewInputType : InputObjectGraphType<DealView>
    {
        public DealViewInputType()
        {

            Field(d => d.Name, nullable: false);
            Field(d => d.IsComplete, nullable: false);
            Field(d => d.CategoryId, nullable: false);
            Field(d => d.DueDate, nullable: true);
        }
    }
}
