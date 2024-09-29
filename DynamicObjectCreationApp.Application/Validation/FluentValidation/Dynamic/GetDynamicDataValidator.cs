using DynamicObjectCreationApp.Application.Dynamic.Queries.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Validation.FluentValidation.Dynamic
{
    public class GetDynamicDataValidator : AbstractValidator<GetDyanmicDataByIdQueryRequest>
    {
        public GetDynamicDataValidator()
        {
            RuleFor(x => x.ObjectName).NotNull().NotEmpty().WithMessage("ObjectName is required");
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required");
        }
    }
}
