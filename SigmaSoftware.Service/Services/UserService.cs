using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SigmaSoftware.Common.Dtos;
using SigmaSoftware.Common.Models;
using SigmaSoftware.Data.Entites;
using SigmaSoftware.Data.RepositoryContracts;
using SigmaSoftware.Service.ServiceContracts;
using StatusGeneric;

namespace SigmaSoftware.Service.Services;

public class UserService(IBaseRepository<User> userRepository, IMemoryCache memoryCache) : StatusGenericHandler, IUserService
{
    private readonly IMemoryCache _memoryCache = memoryCache;
    private const string CacheKey = "user";
    private readonly IBaseRepository<User> _userRepository = userRepository;
    public async Task AddUser(AddUserModel model)
    {
        try
        {
            using var transaction = await _userRepository.BeginTransactionAsync();
            var users = await GetAllUsers();
            var user = users.Where(u => u.Email == model.Email)
                .FirstOrDefault();

            bool isUpdated = await UpdateUserIfNotExist(user, model);
            if (isUpdated)
                return;
            var newUser = model.Adapt<User>();
            await _userRepository.Add(newUser);
            await _userRepository.SaveChanges();

            await _userRepository.CommitTransactionAsync();
        }
        catch (Exception e)
        {
            await _userRepository.RollbackTransactionAsync();
            throw new Exception(e.Message);
        }
    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await GetAllUsersFromCache();
        return users.Adapt<List<UserDto>>();
    }

    public async Task<UserDto?> GetUserById(Guid userId)
    {
        bool isThereUser = _memoryCache.TryGetValue(CacheKey, out User? user);
        if (!isThereUser)
        {
            user = await _userRepository.GetById(userId);
            _memoryCache.Set(CacheKey, user);
        }

        return user.Adapt<UserDto>();
    }

    public async Task UpdateUser(Guid userId, UpdateUserModel model)
    {
        try
        {
            using var transaction = await _userRepository.BeginTransactionAsync();
            var users = await GetAllUsersFromCache();
            var user = users?.Where(u => u.Id == userId)
                .FirstOrDefault();

            if (user is null)
            {
                AddError("User not found.");
                return;
            }

            var updatedUser = model.Adapt(user);

            _userRepository.Update(updatedUser);
            await _userRepository.SaveChanges();

            await _userRepository.CommitTransactionAsync();

        }
        catch (Exception e)
        {
            await _userRepository.RollbackTransactionAsync();
            throw new Exception(e.Message);
        }
    }

    private async Task<List<User>?> GetAllUsersFromCache()
    {
        bool isThereUser = _memoryCache.TryGetValue(CacheKey, out List<User>? user);
        if (!isThereUser)
        {
            user = _userRepository.GetAll().ToList();

            _memoryCache.Set(CacheKey, user);
            return user;
        }
        return user;
    }

    private async Task<bool> UpdateUserIfNotExist(UserDto? user, AddUserModel model)
    {
        if (user is not null)
        {
            var updatedUser = model.Adapt<UpdateUserModel>();
            await UpdateUser(user.Id, updatedUser);
            return true;
        }
        return false;
    }


}