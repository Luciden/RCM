﻿using RCM.Domain.Validators.DuplicataCommandValidators;
using System;

namespace RCM.Domain.Commands.DuplicataCommands
{
    public class RemoveDuplicataCommand : DuplicataCommand
    {
        public RemoveDuplicataCommand(Guid id) 
        {
            Id = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveDuplicataCommandValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
