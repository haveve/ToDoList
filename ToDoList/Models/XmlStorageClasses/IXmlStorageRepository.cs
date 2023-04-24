using ToDoList.ViewModel;

namespace ToDoList.Models.XmlStorageClasses
{
    public interface IXmlStorageRepository
    {
        public List<Category> GetCategories();

        public List<Deal> GetDeals();

        public void AddDeal(DealView deal);

        public void DeleteDeal(int Id);

        public void UpdateDeal(Deal deal);

        public void UpdateStateDeal(int Id, bool state);

        public void AddCategory(CategoryView category);
    }
}
