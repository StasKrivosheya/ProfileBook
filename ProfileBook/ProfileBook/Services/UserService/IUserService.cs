using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProfileBook.Models;

namespace ProfileBook.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserModel>> GetItemsAsync();

        Task<UserModel> GetItemAsync(int id);
        Task<UserModel> GetItemAsync(Expression<Func<UserModel, bool>> predicate);

        Task<int> InsertItemAsync(UserModel item);
        Task<int> UpdateItemAsync(UserModel item);

        Task<int> DeleteItemAsync(UserModel item);
    }
}
