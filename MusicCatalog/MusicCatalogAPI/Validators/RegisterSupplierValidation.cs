using FluentValidation;
using MusicCatalogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicCatalogAPI.Validators
{
    public class RegisterSupplierValidation : AbstractValidator<RegisterSupplierDto>
    {
        public RegisterSupplierValidation(AppDbContext dbContext)
        {
            RuleFor(u => u.Username).NotEmpty();
            RuleFor(u => u.Username).Custom((value, context) =>
            {
                var loginAlreadyExist = dbContext.Users.Any(user => user.Username.Equals(value));
                if (loginAlreadyExist)
                    context.AddFailure("username", "That username is taken");
            });

            RuleFor(u => u.Password).MinimumLength(4);
            RuleFor(u => u.Password).Equal(u => u.ConfirmPassword);

            RuleFor(u => u.Name).NotEmpty();
        }
    }
}
