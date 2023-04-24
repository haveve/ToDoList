using ToDoList.ViewModel;
using System.Xml.Linq;
using System.Xml;
namespace ToDoList.Models.XmlStorageClasses
{
    public class XmlStorageRepository:IXmlStorageRepository
    {
        private XDocument categories;
        private XDocument deals;

        public XmlStorageRepository() 
        {
            categories = XDocument.Load(@"XmlStorage/Categories.xml");
            deals = XDocument.Load(@"XmlStorage/Deals.xml");

        }

        public List<Category> GetCategories()
        {
           var c =  categories.Element("categories")?.Elements("category")?
                .Select(c => new Category 
                {Id =int.Parse(c!.Element("id")!.Value),
                Name = c!.Element("name")!.Value }).ToList();   
            
            return c == null ? new() : c;
        }

        public List<Deal> GetDeals()
        {
            var d = deals.Element("deals")?.Elements("deal")
                .Select(c => new Deal
                {
                    Id = int.Parse(c!.Element("id")!.Value),
                    Name = c!.Element("name")!.Value,
                    CategoryId = int.Parse(c!.Element("categoryId")!.Value),
                    IsComplete = bool.Parse(c!.Element("isComplete")!.Value),
                    DueDate = c!.Element("dueDate")!.Value == "NULL" ? null : DateTime.Parse(c!.Element("dueDate")!.Value)

                })?.ToList();

            return d == null ? new() : d;
        }

        public  void AddDeal(DealView deal)
        {
            var previusId = deals.Element("deals")?.Elements("deal")?.LastOrDefault()?.Element("id")?.Value;
            var currentId = Convert.ToInt32(previusId)+1;

            deals.Element("deals")!.Add(new XElement("deal",
                new XElement("id", currentId),
                new XElement("name", deal.Name),
                new XElement("dueDate",deal.DueDate == null?"NULL":deal.DueDate),
                new XElement("isComplete",deal.IsComplete),
                new XElement("categoryId",deal.CategoryId)));
            deals.Save(@"XmlStorage/Deals.xml");

        }

        public  void DeleteDeal(int Id)
        {

                deals.Element("deals")!.Elements("deal")?.FirstOrDefault(d => int.Parse(d.Element("id")!.Value) == Id)?.Remove();
            
            deals.Save(@"XmlStorage/Deals.xml");
        }

        public  void UpdateDeal(Deal deal)
        {
           var element =  deals.Element("deals")!.Elements("deal")?.FirstOrDefault(d => int.Parse(d.Element("id")!.Value) == deal.Id);
            if (element != null)
            {
                element.Element("name")!.Value = deal.Name;
                element.Element("categoryId")!.Value = deal.CategoryId.ToString();
                    element.Element("dueDate")!.Value = deal!.DueDate == null?"NULL":deal!.DueDate!.ToString()!;
                element.Element("isComplete")!.Value = deal.IsComplete.ToString();
            deals.Save(@"XmlStorage/Deals.xml");

            }
        }
        public void UpdateStateDeal(int Id, bool State)
        {
            var element = deals.Element("deals")!.Elements("deal")?.FirstOrDefault(d => int.Parse(d.Element("id")!.Value) == Id);
            if (element != null)
            {
                element.Element("isComplete")!.Value = State.ToString();
                deals.Save(@"XmlStorage/Deals.xml");

            }
        }

        public void  AddCategory(CategoryView category)
        {
            var previusId = categories.Element("categories")?.Elements("category")?.LastOrDefault()?.Element("id")?.Value;
            var currentId = Convert.ToInt32(previusId) + 1;

            categories.Element("categories")!.Add(new XElement("category",
                new XElement("id", currentId),
                new XElement("name", category.Name)));
            categories.Save(@"XmlStorage/Categories.xml");
        }



    }
}
