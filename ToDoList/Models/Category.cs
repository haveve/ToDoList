using System.ComponentModel.DataAnnotations;
using ToDoList.ViewModel;

namespace ToDoList.Models
{
    public class Category
    {
        public int  Id { get; set; }
        [MaxLength(55)]
        [Required]
        public string Name { get; set; }

        public Category()
        {

        }

        public Category(CategoryView categoryView)
        {
            Name = categoryView.Name;
        }
    }
}
