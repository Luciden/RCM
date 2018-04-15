﻿using RCM.Domain.Core.Commands;
using RCM.Domain.Core.Errors;
using RCM.Domain.Core.MediatorServices;
using RCM.Domain.Core.Models;
using RCM.Domain.Helpers;
using RCM.Domain.UnitOfWork;
using System.Threading.Tasks;

namespace RCM.Domain.CommandHandlers
{
    public abstract class CommandHandler<TModel> where TModel : Entity<TModel>
    {
        protected readonly IMediatorHandler _mediator;
        protected readonly IUnitOfWork _unitOfWork;
        protected CommandResult _commandResponse;

        public CommandHandler(IMediatorHandler mediator, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;

            _commandResponse = new CommandResult();
        }

        protected bool Commit()
        {
            var commitResult = _unitOfWork.Commit();
            
            if (commitResult.Success)
                return true;
            else
            {
                foreach (var error in commitResult.Errors)
                {
                    _commandResponse.AddError(new CommandError("Commit Error", error?.InnerException?.Message ?? error.Message));
                }
            }

            return false;
        }
        
        protected void NotifyRequestErrors(Command command)
        {
            foreach (var error in command.ValidationResult.Errors)
            {
                _commandResponse.AddError(new CommandError(error.ErrorCode, error.ErrorMessage));
            }
        }

        protected void NotifyRequestError(string errorMessage, string errorCode = null)
        {
            _commandResponse.AddError(new CommandError(errorCode ?? NotificationMessageConstants.UndefinedError, errorMessage));
        }

        protected Task<CommandResult> Response()
        {
            return Task.FromResult(_commandResponse);
        }
    }
}
