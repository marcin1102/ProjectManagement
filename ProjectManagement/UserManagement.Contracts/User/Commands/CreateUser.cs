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
        public CreateUser(string firstName, string lastName, string email, Role role)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Role = role;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
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
        }
    }

}
