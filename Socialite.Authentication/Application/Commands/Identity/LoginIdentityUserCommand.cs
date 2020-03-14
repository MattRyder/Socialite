using System.ComponentModel.DataAnnotations;
using MediatR;
using Socialite.Authentication.Application.Responses;

namespace Socialite.Authentication.Application.Commands.Identity
{
    public class LoginIdentityUserCommand : IRequest<CommandResponse>
    {
        [Required]
        [EmailAddress]
        public string Email { get; private set; }

        [Required]
        public string Password { get; private set; }

        [Required]
        public string Return { get; private set; }

        public LoginIdentityUserCommand(string email, string password, string returnurl)
        {
            Email = email;

            Password = password;

            Return = returnurl;
        }
    }
}
