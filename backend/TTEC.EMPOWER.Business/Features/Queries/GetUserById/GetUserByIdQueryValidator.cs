using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTEC.EMPOWER.Business.Features.Queries.GetUserById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            RuleFor(x => x.EmployeeId).NotNull().NotEmpty().WithMessage("EmployeeId can not be empty")
                .GreaterThan(0).WithMessage("EmployeeId should be greater than 0");
        }
    }
}
