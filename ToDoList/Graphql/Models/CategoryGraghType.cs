using GraphQL.Types;
using ToDoList.Models;

namespace ToDoList.Graphql.Models
{
    public class CategoryGraghType : ObjectGraphType<Category>
    {
        public CategoryGraghType()
        {
            Field(d => d.Name, nullable: false);
            Field(d => d.Id, nullable: false);
        }
    }
}
