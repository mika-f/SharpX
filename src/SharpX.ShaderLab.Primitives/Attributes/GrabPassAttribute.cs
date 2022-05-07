// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;

namespace SharpX.ShaderLab.Library.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class GrabPassAttribute : Attribute
    {
        public GrabPassAttribute() { }

        public GrabPassAttribute(string name) { }
    }
}