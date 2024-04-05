using Api.Models;

namespace Api.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User? AddOrGetUser(User user);
    }
}
