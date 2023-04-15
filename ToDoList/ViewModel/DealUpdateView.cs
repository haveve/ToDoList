using System.ComponentModel.DataAnnotations;
using ToDoList.Models;

namespace ToDoList.ViewModel
{
    public class DealUpdateView
    {
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [MaxLength(55)]
        [Required]
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public DateTime? DueDate { get; set; }

        public DealUpdateView() { }

        public DealUpdateView(Deal deal)
        {
            Id = deal.Id;
            Name = deal.Name;
            CategoryId = deal.CategoryId;
            IsComplete = deal.IsComplete;
            DueDate = deal.DueDate;
        }
    }
}
