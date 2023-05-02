using GraphQL.Types;
using ToDoList.ViewModel;

namespace ToDoList.Graphql.Models
{


    public class DealViewGraghType : ObjectGraphType<DealView> 
    {
        public DealViewGraghType()
        {

            Field(d => d.Name, nullable: false);
            Field(d => d.IsComplete, nullable: false);
            Field(d => d.CategoryId, nullable: false);
            Field(d => d.DueDate, nullable: true);
        }
    }
}
