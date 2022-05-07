// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;

namespace SharpX.ShaderLab.Library.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class ToggleAttribute : Attribute
    {
        public ToggleAttribute() { }

        public ToggleAttribute(string @ref) { }

        public ToggleAttribute(Type t) { }
    }
}