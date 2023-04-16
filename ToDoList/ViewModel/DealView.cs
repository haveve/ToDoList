using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.ViewModel
{
    public class DealView
    {
        [BindRequired]
        public int CategoryId { get; set; }
        [MaxLength(55)]
        [BindRequired]
        public string Name { get; set; }
        public bool IsComplete { get; set; } = false;
        public DateTime? DueDate { get; set; } = null;
    }
}
