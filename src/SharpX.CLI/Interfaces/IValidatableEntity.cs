﻿// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.CLI.Interfaces;

public interface IValidatableEntity
{
    bool Validate(out List<IErrorMessage> errors);
}