using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace UserManagement.Authentication
{
    public class AuthTokenStore
    {
        internal ICollection<AccessToken> Tokens = new List<AccessToken>();

        public void AddToken(AccessToken token)
        {
            Tokens.Add(token);
        }

        public bool IsTokenActive(string token)
        {
            var accessToken = Tokens.SingleOrDefault(x => x.Value.Equals(token));
            if (accessToken == null)
                return false;
            if (accessToken.LastlyUsed.Date != DateTimeOffset.UtcNow.Date)
                return false;

            return accessToken.LastlyUsed.TimeOfDay >= DateTimeOffset.UtcNow.TimeOfDay.Subtract(new TimeSpan(0, 15, 0));
        }

        public void InitializeWithValuesFromDatabase(IEnumerable<AccessToken> tokens)
        {
            Tokens = tokens.ToList();
        }

        public void RemoveToken(AccessToken token)
        {
            Tokens.Remove(token);
        }

        public Guid GetUserByToken(string value)
        {
            var token = Tokens.Single(x => x.Value.Equals(value));
            token.Update();
            return token.UserId;
        }

        public AccessToken GetToken(string token)
        {
            return Tokens.Single(x => x.Value.Equals(token));
        }
    }

    public class AccessToken
    {
        public AccessToken(string value, Guid userId)
        {
            Value = value;
            UserId = userId;
            LastlyUsed = DateTime.UtcNow;
        }

        public string Value { get; private set; }
        public Guid UserId { get; private set; }
        public DateTimeOffset LastlyUsed { get; private set; }

        public void Update()
        {
            LastlyUsed = DateTimeOffset.UtcNow;
        }
    }
}
