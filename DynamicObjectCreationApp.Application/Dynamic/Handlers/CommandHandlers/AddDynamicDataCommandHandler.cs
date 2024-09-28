using AutoMapper;
using DynamicObjectCreationApp.Application.Dynamic.Commands.Request;
using DynamicObjectCreationApp.Entity;
using DynamicObjectCreationApp.Infracture.Abstract;
using DynamicObjectCreationApp.Infracture.Concrete;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicObjectCreationApp.Application.Dynamic.Handlers.CommandHandlers
{
    public class AddDynamicDataCommandHandler : IRequestHandler<AddDynamicDataCommandRequest, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AddDynamicDataCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Result<string>> Handle(AddDynamicDataCommandRequest transaction, CancellationToken cancellationToken)
        {
            var executionStrategy = _unitOfWork.DynamicContext.Database.CreateExecutionStrategy();

            return await executionStrategy.ExecuteAsync(async () =>
            {
                try
                {
                    //await _unitOfWork.BeginTransactionAsync();

                    foreach (var kvp in transaction.DyanmicObject)
                    {
                        var objectName = kvp.Key;
                        var dataList = kvp.Value;

                        foreach (var data in dataList)
                        {
                            await _unitOfWork.DynamicRepository.CreateAsync(objectName, data);
                        }
                    }
                    //await _unitOfWork.DynamicContext.SaveChangesAsync();
                    //await _unitOfWork.CommitAsync(); 
                    return Result.Ok("Created!"); 
                }
                catch (Exception ex)
                {
                    //await _unitOfWork.RollbackAsync(); 
                    return Result.Fail<string>(ex.Message);
                }
            });
        }

    }
}
