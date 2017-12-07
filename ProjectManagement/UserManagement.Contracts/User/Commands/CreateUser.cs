using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using FluentValidation;
using Infrastructure.Message;
using Newtonsoft.Json;
using UserManagement.Contracts.User.Enums;

namespace UserManagement.Contracts.User.Commands
{
    public class CreateUser : ICommand
    {
        public CreateUser(string firstName, string lastName, string email, string password, Role role)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Role = role;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Role Role { get; private set; }

        [JsonIgnore]
        public Guid CreatedId { get; set; }
    }

    public class CreateUserValidator : AbstractValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name cannot be null or empty")
                .WithErrorCode("400");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name cannot be null or empty")
                .WithErrorCode("400");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email cannot be null or empty")
                .EmailAddress()
                .WithMessage("Email adress is not valid")
                .WithErrorCode("400");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password cannot be null or empty")
                .MinimumLength(4)
                .WithMessage("Password too short")
                .WithErrorCode("400");
        }
    }

}
