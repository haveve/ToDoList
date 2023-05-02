using GraphQL.Types;
using ToDoList.Models;

namespace ToDoList.Graphql.Models
{
    public class DealGraghType : ObjectGraphType<Deal>
    {
        public DealGraghType()
        {
            Field(d => d.Name, nullable: false);
            Field(d => d.Id, nullable: false);
            Field(d => d.IsComplete, nullable: false);
            Field(d => d.CategoryId, nullable: false);
            Field(d => d.DueDate, nullable: true);
        }
    }
}
