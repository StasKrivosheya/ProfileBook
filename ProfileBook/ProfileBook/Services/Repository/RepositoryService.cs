using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProfileBook.Models;
using SQLite;

namespace ProfileBook.Services.Repository
{
    public class RepositoryService : IRepositoryService
    {
        private readonly SQLiteAsyncConnection _database;

        public RepositoryService()
        {
            _database = new SQLiteAsyncConnection(
                Path.Combine(Constants.DatabasePath, Constants.DATABASE_NAME));
        }

        public async Task CreateTableAsync<T>() where T : IEntity, new()
        {
            await _database.CreateTableAsync<T>();
        }

        public async Task<List<T>> GetItemsAsync<T>() where T : IEntity, new()
        {
            return await _database.Table<T>().ToListAsync();
        }

        public async Task<T> GetItemAsync<T>(int id) where T : IEntity, new()
        {
            return await _database.GetAsync<T>(id);
        }

        public async Task<T> GetItemAsync<T>(Expression<Func<T, bool>> predicate) where T : IEntity, new()
        {
            return await _database.GetAsync<T>(predicate);
        }

        public async Task<int> InsertItemAsync<T>(T item) where T : IEntity, new()
        {
            int result;
            try
            {
                if (item.Id != 0)
                {
                    result = await _database.UpdateAsync(item);
                }
                else
                {
                    result = await _database.InsertAsync(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result = -1;
            }

            return result;
        }

        public async Task<int> UpdateItemAsync<T>(T item) where T : IEntity, new()
        {
            return await _database.UpdateAsync(item);
        }

        public async Task<int> DeleteItemAsync<T>(T item) where T : IEntity, new()
        {
            return await _database.DeleteAsync<T>(item);
        }
    }
}
