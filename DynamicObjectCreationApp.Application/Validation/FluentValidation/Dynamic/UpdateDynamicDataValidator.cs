using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Validation.FluentValidation.Dynamic
{
    public class UpdateDynamicDataValidator : AbstractValidator<UpdateDynamicDataCommandRequest>
    {
        public UpdateDynamicDataValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).NotNull().WithMessage("Id is required");
            RuleFor(x => x.ObjectName).NotNull().NotEmpty().WithMessage("ObjectName is required");
            RuleFor(x => x.Data).NotNull().NotEmpty().WithMessage("Data is required");
        }

    }
}
