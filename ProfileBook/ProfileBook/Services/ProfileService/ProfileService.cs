using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProfileBook.Models;
using ProfileBook.Services.Repository;

namespace ProfileBook.Services.ProfileService
{
    class ProfileService : IProfileService
    {
        private readonly IRepository _repository;

        public ProfileService(IRepository repository)
        {
            _repository = repository;

            _repository.CreateTableAsync<ProfileModel>();
        }

        #region --- Interface Implementation ---

        public Task<List<ProfileModel>> GetItemsAsync()
        {
            return _repository.GetItemsAsync<ProfileModel>();
        }

        public Task<ProfileModel> GetItemAsync(int id)
        {
            return _repository.GetItemAsync<ProfileModel>(id);
        }

        public Task<ProfileModel> GetItemAsync(Expression<Func<ProfileModel, bool>> predicate)
        {
            return _repository.GetItemAsync(predicate);
        }

        public Task<int> InsertItemAsync(ProfileModel item)
        {
            return _repository.InsertItemAsync(item);
        }

        public Task<int> UpdateItemAsync(ProfileModel item)
        {
            return _repository.UpdateItemAsync(item);
        }

        public Task<int> DeleteItemAsync(ProfileModel item)
        {
            return _repository.DeleteItemAsync(item);
        }

        #endregion
    }
}
