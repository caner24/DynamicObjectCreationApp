using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Validation.FluentValidation.Dynamic
{
    public class DeleteDynamicValidator : AbstractValidator<DeleteDynamicDataCommandRequest>
    {
        public DeleteDynamicValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id is required");
            RuleFor(x => x.ObjectName).NotNull().NotEmpty().WithMessage("Id is required");
        }
    }
}
