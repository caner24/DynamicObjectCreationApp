using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Validation.FluentValidation.Dynamic
{
    public class AddDynamicDataValidator:AbstractValidator<AddDynamicDataCommandRequest>
    {
        public AddDynamicDataValidator()
        {
            RuleFor(x=>x.DyanmicObject).NotNull().NotNull().WithMessage("DynamicObject is required");
        }
    }
}
