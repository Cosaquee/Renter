using Models.Dtos.User;
using Services.UserServices.Results;

namespace Services.UserServices.Interfaces
{
    public interface IUserManagementService
    {
        UserCreationResult CreateUser(CreateUserDto createUserDto);
    }
}