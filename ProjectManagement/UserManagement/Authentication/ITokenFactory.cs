using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace UserManagement.Authentication
{
    public interface ITokenFactory
    {
        Task<AccessToken> Create(Guid userId);
    }

    public class TokenFactory : ITokenFactory
    {
        private readonly UserManagementContext db;

        public TokenFactory(UserManagementContext db)
        {
            this.db = db;
        }

        public async Task<AccessToken> Create(Guid userId)
        {
            var encryptor = new System.Security.Cryptography.Rfc2898DeriveBytes(Guid.NewGuid().ToString(), Guid.NewGuid().ToByteArray());
            var value = Convert.ToBase64String(encryptor.GetBytes(500));
            var token = new Token(value, userId);
            db.Tokens.Add(token);
            await db.SaveChangesAsync();
            return ToAccessToken(token);
        }

        public static AccessToken ToAccessToken(Token token)
        {
            return new AccessToken(token.Value, token.UserId);
        }
    }
}
