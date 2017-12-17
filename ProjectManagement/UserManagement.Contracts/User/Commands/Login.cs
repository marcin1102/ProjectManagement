using FluentValidation;
using ProjectManagement.Infrastructure.Primitives.Message;

namespace UserManagement.Contracts.User.Commands
{
    public class Login : ICommand
    {
        public Login(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; }
        public string Password { get; }

        public string GeneratedToken { get; set; }
    }

    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithErrorCode("400");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password cannot be null or empty")
                .WithErrorCode("400");
        }
    }
}
