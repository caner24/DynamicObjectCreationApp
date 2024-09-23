using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
using DynamicObjectCreationApp.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Validation.FluentValidation.Dynamic
{
    public class AddDynamicObjectDtoValidator : AbstractValidator<AddDynamicDataCommandRequest>
    {
        public AddDynamicObjectDtoValidator()
        {
            RuleFor(x => x.TableName).NotNull().NotEmpty().WithMessage("TableName is required");
            RuleFor(x => x.ObjectDataJson).NotNull().NotEmpty().WithMessage("ObjectDataJson is required");

        }
    }
}
