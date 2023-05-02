using GraphQL.Types;
using GraphQLParser.AST;
using System.ComponentModel.DataAnnotations;
using ToDoList.ViewModel;

namespace ToDoList.Models
{
    public class Deal
    {
        public int Id {get; set;}
        public int CategoryId { get; set;}
        public string Name { get; set;}
        public bool IsComplete { get; set; } = false;
        public DateTime? DueDate {get; set;} 

    }

}
