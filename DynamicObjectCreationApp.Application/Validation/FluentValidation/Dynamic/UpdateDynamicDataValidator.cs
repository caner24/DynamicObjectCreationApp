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
    public class UpdateDynamicDataValidator : AbstractValidator<UpdateDynamicDataCommandRequest>
    {
        public UpdateDynamicDataValidator()
        {
            RuleFor(x => x.TableName).NotNull().NotEmpty().WithMessage("TableName is required");
            RuleFor(x=>x.ObjectDataJson).NotNull().NotEmpty().WithMessage("ObjectDataJson is required");
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id is required");
        }

    }
}
