using AutoMapper;
using Loan_API.Application.Login.Commands;
using Loan_API.Application.RoleUser.Commands;
using Loan_API.Application.RoleUser.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_API.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.User, RegistrationUser.Command>();
            CreateMap<RegistrationUser.Command, Domain.User>();

            CreateMap<CreateLoan.Command, Domain.Loan>();
            CreateMap<UpdateLoanById.Command, Domain.Loan>();

        }
    }
}
