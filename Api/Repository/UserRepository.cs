using Api.Data;
using Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Api.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public User AddOrGetUser(string username)
        {
            User? userObj = null;
            bool userExists = _context.Users.Any(user => user.Username == username);

            if (!userExists)
            {
                _context.Users.Add(new User { Username = username, });
                _context.SaveChanges();
            }

            userObj = _context.Users.First(user => user.Username == username);
            return userObj;
        }

        public ICollection<User> GetUsers()
        {
            return [.. _context.Users.Include(b => b.Bookmarks).ThenInclude(r => r.Route).OrderBy(user => user.Username)];
        }
    }
}
