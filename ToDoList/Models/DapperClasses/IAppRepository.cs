using ToDoList.ViewModel;

namespace ToDoList.Models.DapperClasses
{
    public interface IAppRepository
    {
        public List<Category> GetCategories();

        public List<Deal> GetDeals();

        public Task AddDeal(DealView deal);

        public Task DeleteDeal(int Id);

        public Task UpdateDeal(Deal deal);

        public Task UpdateStateDeal(int Id, bool state);

        public Task AddCategory(CategoryView category);

    }
}
