using Dapper;
using System.Net.NetworkInformation;
using System;
using ToDoList.ViewModel;

namespace ToDoList.Models.DapperClasses
{
    public class AppRepository:IAppRepository
    {
        private readonly DapperConnectionProvider _dapperProvider;

        public AppRepository(DapperConnectionProvider dapperProvider)
        {
            _dapperProvider = dapperProvider;
        }
        public List<Category> GetCategories()
        {
            string query = "SELECT * FROM Category";
            using var connect = _dapperProvider.Connect();
            var categories = connect.Query<Category>(query);
            return categories.ToList();
        }

        public List<Deal> GetDeals()
        {
            string query = "SELECT * FROM Deal";
            using var connect = _dapperProvider.Connect();
            var deals = connect.Query<Deal>(query);
            return deals.ToList();
        }

        public async Task AddDeal(DealView deal) 
        {
            string query = "INSERT INTO Deal (CategoryId,Name,IsComplete,DueDate) VALUES(@CategoryId,@Name,@IsComplete,@DueDate)";
            using var connect = _dapperProvider.Connect();
            await connect.ExecuteAsync(query, deal);
        }

        public async Task DeleteDeal(int Id) 
        {
            string query = $"DELETE FROM Deal WHERE Id = {Id}";
            using var connect = _dapperProvider.Connect();
            await connect.ExecuteAsync(query);
        }

        public async Task UpdateDeal(Deal deal)
        {
            string query = @"UPDATE [Deal] SET CategoryId = @CategoryId, [Name] = @Name ,IsComplete = @IsComplete ,DueDate = @DueDate WHERE Id = @Id";
            using var connect = _dapperProvider.Connect();
            await connect.ExecuteAsync(query, deal);
        } 
        public async Task UpdateStateDeal(int Id, bool State)
        {
            string query = @"UPDATE Deal SET IsComplete = @State WHERE Id = @Id";
            using var connect = _dapperProvider.Connect();
            await connect.ExecuteAsync(query, new {Id,State});
        }

        public async Task AddCategory(CategoryView category)
        {
            string query = $"INSERT INTO Category (Name) VALUES('{category.Name}')";
            using var connect = _dapperProvider.Connect();
            await connect.ExecuteAsync(query);
        }

    }
}
