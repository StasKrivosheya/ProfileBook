using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProfileBook.Models;

namespace ProfileBook.Services.ProfileService
{
    public interface IProfileService
    {
        Task<List<ProfileModel>> GetItemsAsync();
        Task<List<ProfileModel>> GetItemsAsync(Expression<Func<ProfileModel, bool>> predicate);

        Task<ProfileModel> GetItemAsync(int id);
        Task<ProfileModel> GetItemAsync(Expression<Func<ProfileModel, bool>> predicate);

        Task<int> InsertItemAsync(ProfileModel item);
        Task<int> UpdateItemAsync(ProfileModel item);

        Task<int> DeleteItemAsync(ProfileModel item);
    }
}
