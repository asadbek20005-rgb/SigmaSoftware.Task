using SigmaSoftware.Common.Dtos;
using SigmaSoftware.Common.Models;
using StatusGeneric;

namespace SigmaSoftware.Service.ServiceContracts;

public interface IUserService : IStatusGeneric
{
    Task<List<UserDto>> GetAllUsers();
    Task<UserDto?> GetUserById(Guid userId);
    Task AddUser(AddUserModel model);
    Task UpdateUser(Guid userId,UpdateUserModel model);
}
