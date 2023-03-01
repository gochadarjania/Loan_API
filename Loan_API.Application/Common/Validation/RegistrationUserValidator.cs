using FluentValidation;
using Loan_API.Application.Login.Commands;
using Loan_API.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_API.Application.Common.Validation
{
  public class RegistrationUserValidator : AbstractValidator<RegistrationUser.Command>
  {
    LoanDbContext _db;
    public RegistrationUserValidator(LoanDbContext db)
    {
      _db = db;

      RuleFor(c => c.FirstName).NotEmpty().MaximumLength(25);
      RuleFor(c => c.LastName).NotEmpty().MaximumLength(25);
      RuleFor(c => c.UserName)
          .NotEmpty()
          .MaximumLength(25)
          .EmailAddress()
          .Must(userName =>
          {
            return !_db.Users.Any(x => x.UserName == userName);
          }).WithMessage("is already taken");
      RuleFor(c => c.Age).NotEmpty().
           Must(age => { return (age > 12 && age < 90); }).WithMessage("Correct your {PropertyName} ");
      RuleFor(c => c).NotEmpty().Must(c =>
      {
        if (c.Password == c.FirstName || c.Password == c.LastName || c.Password == c.UserName || c.Password.Length < 7)
        {
          return false;
        }
        return true;
      }).WithMessage("Password : Please choose a more secure password. It should be longer than 6 characters, unique to you, and difficult for others to guess.");

    }
  }
}
