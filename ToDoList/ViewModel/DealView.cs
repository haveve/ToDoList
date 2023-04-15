using System.ComponentModel.DataAnnotations;

namespace ToDoList.ViewModel
{
    public class DealView
    {
        [Required]
        public int CategoryId { get; set; }
        [MaxLength(55)]
        [Required]
        public string Name { get; set; }
        public bool IsComplete { get; set; } = false;
        public DateTime? DueDate { get; set; } = null;
    }
}
