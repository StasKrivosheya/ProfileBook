using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProfileBook.Models;

namespace ProfileBook.Services.Repository
{
    public interface IRepositoryService
    {
        Task CreateTableAsync<T>() where T : IEntity, new();
        
        Task<List<T>> GetItemsAsync<T>() where T : IEntity, new();

        Task<T> GetItemAsync<T>(int id) where T : IEntity, new();
        Task<T> GetItemAsync<T>(Expression<Func<T, bool>> predicate) where T : IEntity, new();

        Task<int> InsertItemAsync<T>(T item) where T : IEntity, new();
        Task<int> UpdateItemAsync<T>(T item) where T : IEntity, new();

        Task<int> DeleteItemAsync<T>(T item) where T : IEntity, new();
    }
}
