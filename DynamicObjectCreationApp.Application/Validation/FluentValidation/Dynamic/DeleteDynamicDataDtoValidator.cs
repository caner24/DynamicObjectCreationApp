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
    public class DeleteDynamicDataDtoValidator : AbstractValidator<DeleteDynamicDataCommandRequest>
    {
        public DeleteDynamicDataDtoValidator()
        {
            RuleFor(x => x.TableName).NotEmpty().NotNull().WithMessage("TableName is required");
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id is required");
        }
    }
}
