using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace UserManagement.Authentication
{

    public static class AuthTokenStore
    {
        internal static ICollection<Token> Tokens = new List<Token>();

        public static void AddToken(string token, Guid userId)
        {
            Tokens.Add(new Token(token, userId));
        }

        public static bool DoesTokenIsActive(string token)
        {
            return Tokens.Any(x => x.Value.Equals(token));
        }

        public static void InitializeWithValuesFromDatabase(IEnumerable<Token> tokens)
        {
            Tokens = tokens.ToList();
        }

        public static void RemoveToken(Token token)
        {
            Tokens.Remove(token);
        }

        public static Guid GetUserIsByToken(string value)
        {
            var token = Tokens.Single(x => x.Value.Equals(value));
            token.Update();
            return token.UserId;
        }
    }

    public class Token
    {
        public Token(string value, Guid userId)
        {
            Value = value;
            UserId = userId;
            LastlyUsed = DateTime.UtcNow;
        }

        public string Value { get; private set; }
        public Guid UserId { get; private set; }
        public DateTime LastlyUsed { get; private set; }

        public void Update()
        {
            LastlyUsed = DateTime.UtcNow;
        }
    }
}
