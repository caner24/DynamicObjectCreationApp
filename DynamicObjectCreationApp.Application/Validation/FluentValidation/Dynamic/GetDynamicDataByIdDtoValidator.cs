using DynamicObjectCreationApp.Application.Dynamic.Queries.Request;
using DynamicObjectCreationApp.Entity.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Validation.FluentValidation.Dynamic
{
    public class GetDynamicDataByIdDtoValidator : AbstractValidator<GetDynamicDataByIdQueryRequest>
    {
        public GetDynamicDataByIdDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Id is required");
        }
    }
}
