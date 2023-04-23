using System.Xml;
using System.Xml.Linq;
using ToDoList.Models.DapperClasses;
using ToDoList.Models.XmlStorageClasses;
using ToDoList.ViewModel;

namespace ToDoList.Models.FileMethodClasses
{
    public class FileMethod : IFileMethod
    {
        private int? _currentMethod;
        private IXmlStorageRepository _storageRepository;
        private IAppRepository _appRepository;
        public FileMethod(IXmlStorageRepository xDoc, IAppRepository repo)
        {
            _storageRepository = xDoc;
            _appRepository = repo;
        }

        public void fileMethod(int? Id)
        {
            _currentMethod = Id;
        }

        public List<Category> GetCategories() => _currentMethod switch
        {
            1 => _appRepository.GetCategories(),
            2 => _storageRepository.GetCategories(),
            _ => _appRepository.GetCategories(),
        };


        public List<Deal> GetDeals() => _currentMethod switch
        {
            1 => _appRepository.GetDeals(),
            2 => _storageRepository.GetDeals(),
            _ => _appRepository.GetDeals(),
        };

        public async Task AddDeal(DealView deal)
        {
            switch (_currentMethod)
            {
                case 1: await _appRepository.AddDeal(deal); break;
                case 2: await _storageRepository.AddDeal(deal); break;
                default: await _appRepository.AddDeal(deal); break;
            }
        }

        public async Task DeleteDeal(int Id)
        {
            switch (_currentMethod)
            {
                case 1: await _appRepository.DeleteDeal(Id); break;
                case 2: await _storageRepository.DeleteDeal(Id); break;
                default: await _appRepository.DeleteDeal(Id); break;
            }
        }

        public async Task UpdateDeal(Deal deal)
        {
            switch (_currentMethod)
            {
                case 1: await _appRepository.UpdateDeal(deal); break;
                case 2: await _storageRepository.UpdateDeal(deal); break;
                default: await _appRepository.UpdateDeal(deal); break;
            }

        }
        public async Task UpdateStateDeal(int Id, bool State)
        {
            switch (_currentMethod)
            {
                case 1: await _appRepository.UpdateStateDeal(Id,State); break;
                case 2: await _storageRepository.UpdateStateDeal(Id, State); break;
                default: await _appRepository.UpdateStateDeal(Id, State); break;
            }
        }

        public async Task AddCategory(CategoryView category)
        {
            switch (_currentMethod)
            {
                case 1: await _appRepository.AddCategory(category); break;
                case 2: await _storageRepository.AddCategory(category); break;
                default: await _appRepository.AddCategory(category); break;
            }
        }

    }
}
