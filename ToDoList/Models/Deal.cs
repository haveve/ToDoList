using System.ComponentModel.DataAnnotations;
using ToDoList.ViewModel;

namespace ToDoList.Models
{
    public class Deal
    {
        public int Id {get; set;}
        [Required]
        public int CategoryId { get; set;}
        [MaxLength(55)]
        [Required]
        public string Name { get; set;}
        public bool IsComplete { get; set; } = false;
        public DateTime? DueDate {get; set;} 

        public Deal()
        {

        }


        public Deal(DealView dealView)
        {
            CategoryId = dealView.CategoryId;
            Name = dealView.Name;
            DueDate = dealView.DueDate;
            IsComplete = dealView.IsComplete;
        }
    }

}
