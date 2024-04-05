using Api.Data;
using Api.Interfaces;
using Api.Models;

namespace Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public User? AddOrGetUser(User newUser)
        {
            User? userObj = null;
            bool userExists = _context.Users.Any(user => user.Username == newUser.Username);

            if (!userExists)
            {
                _context.Users.Add(newUser);
                _context.SaveChanges();
            }

            userObj = _context.Users.FirstOrDefault(user => user.Username == newUser.Username);
            return userObj;
        }

        public ICollection<User> GetUsers()
        {
            return [.. _context.Users.OrderBy(user  => user.Username)];
        }
    }
}
