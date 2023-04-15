using System.ComponentModel.DataAnnotations;

namespace ToDoList.ViewModel
{
    public class CategoryView
    {
        [MaxLength(55)]
        [Required]
        public string Name { get; set; }
    }
}
