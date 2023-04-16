using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
namespace ToDoList.ViewModel
{
    public class CategoryView
    {
        [MaxLength(55)]
        [BindRequired]
        public string Name { get; set; }
    }
}
