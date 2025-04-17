using Microsoft.Extensions.Caching.Memory;
using Moq;
using SigmaSoftware.Common.Models;
using SigmaSoftware.Data.Entites;
using SigmaSoftware.Data.RepositoryContracts;
using SigmaSoftware.Service.ServiceContracts;
using SigmaSoftware.Service.Services;
using Umbraco.Core;

namespace SigmaSoftware.Test.Repositories;

public class UserServiceTest
{
    private readonly Mock<IBaseRepository<User>> _mockRepo;
    private readonly IMemoryCache _memoryCache;
    private readonly IUserService _userService;

    public UserServiceTest()
    {
        _mockRepo = new Mock<IBaseRepository<User>>();

        var mockMemoryCache = new MemoryCache(new MemoryCacheOptions());
        _memoryCache = mockMemoryCache;

        _userService = new UserService(_mockRepo.Object, _memoryCache);
    }
    [Fact]
    public async Task Add()
    {
        var model = new AddUserModel
        {
        };

        await _userService.AddUser(model);

        _mockRepo.Verify(repo => repo.Add(It.IsAny<User>()), Times.Once);
    }
}
