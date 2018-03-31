﻿using RCM.Domain.Commands.DuplicataCommands;

namespace RCM.Domain.Validators.DuplicataCommandValidators
{
    public class RemoveDuplicataCommandValidator : DuplicataCommandValidator<RemoveDuplicataCommand>
    {
        public RemoveDuplicataCommandValidator() 
        {
            ValidateId();
        }
    }
}
