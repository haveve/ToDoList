using ToDoList.Models;

namespace ToDoList.ViewModel
{
    public class ToDoListView
    {
        public List<Category> Categories { get; set; }

        public List<Deal> Deals { get; set; }

        public DealView DealV { get; set; }

        public CategoryView CategoryV { get; set; }

        public CategorySortView CategorySort { get; set; }

        public DealUpdateView DealUpdate { get; set; }
    }
}
