using Mapster;
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
        if (users is null || !users.Any())
        {
            AddError("No users found in the cache.");
            return;
        }

        var user = users.Where(u => u.Email == model.Email)
            .FirstOrDefault();

        if (user is not null)
        {
            var updatedUser = model.Adapt<UpdateUserModel>();
            await UpdateUser(user.Id, updatedUser);
        }

        var newUser = model.Adapt<User>();
        await _userRepository.Add(newUser);
        await _userRepository.SaveChanges();
    }

    public List<UserDto> GetAllUsers()
    {
        throw new NotImplementedException();
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

    public Task UpdateUser(Guid userId, UpdateUserModel model)
    {

        throw new NotImplementedException();

    }


}
