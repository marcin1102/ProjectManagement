using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Contracts.User.Enums;
using UserManagement.Hashing;
using UserManagement.User.Model;

namespace UserManagement.User.Services
{
    public interface IUserFactory
    {
        Model.User Create(string firstName, string lastName, string email, string password, Role role);
    }

    public class UserFactory : IUserFactory
    {
        private readonly IHashingService hashingService;

        public UserFactory(IHashingService hashingService)
        {
            this.hashingService = hashingService;
        }

        public Model.User Create(string firstName, string lastName, string email, string password, Role role)
        {
            var hashedPassword = hashingService.GeneratePasswordHash(password);
            var user = new Model.User(Guid.NewGuid(), firstName, lastName, email, hashedPassword, role);
            user.Created();
            return user;
        }
    }
}
