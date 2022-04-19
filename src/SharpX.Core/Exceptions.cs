// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Core;

public static class Exceptions
{
    public static Exception Unreachable => new InvalidOperationException("This program location is thought to be unreachable.");
}