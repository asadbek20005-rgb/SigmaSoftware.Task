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
        bool areThereUsers = _memoryCache.TryGetValue(CacheKey, out List<User>? users);
        if (!areThereUsers)
        {
            users = _userRepository.GetAll().ToList();
            _memoryCache.Set(CacheKey, users);
        }


        var user = users?.Where(u => u.Email == model.Email)
            .FirstOrDefault();

        if (user is not null)
        {
            var updatedUser = model.Adapt<UpdateUserModel>();
            await UpdateUser(user.Id, updatedUser);
            return;
        }

        var newUser = model.Adapt<User>();
        await _userRepository.Add(newUser);
        await _userRepository.SaveChanges();
    }

    public async Task<List<UserDto>> GetAllUsers()
    
    {
        bool areThereUsers = _memoryCache.TryGetValue(CacheKey, out List<User>? users);
        if (!areThereUsers)
        {
            users = await _userRepository.GetAll().ToListAsync();
            _memoryCache.Set(CacheKey, users);
        }

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

        var user = await _userRepository.GetById(userId);
        if (user is null)
        {
            AddError("User not found.");
            return;
        }

        var updatedUser = model.Adapt(user);
        _userRepository.Update(updatedUser);
        await _userRepository.SaveChanges();
        //bool isThereUser = _memoryCache.TryGetValue(CacheKey, out User? user);

        //if (!isThereUser)
        //{
        //    user = await _userRepository.GetById(userId);
        //    _memoryCache.Set(CacheKey, user);
        //}
        //if (user is null)
        //{
        //    AddError("User not found.");
        //    return;
        //}

        //_userRepository.Update(user);
        //await _userRepository.SaveChanges();
    }
}