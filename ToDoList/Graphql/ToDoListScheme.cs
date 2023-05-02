using GraphQL.Types;
namespace ToDoList.Graphql
{
    public class ToDoListScheme : Schema
    {
        public ToDoListScheme(IServiceProvider provider)
        : base(provider)
        {
            Query = provider.GetRequiredService<ToDoList.Graphql.Query.Query>();
            Mutation = provider.GetRequiredService<ToDoList.Graphql.Mutation.Mutation>();
        }
    }
}
