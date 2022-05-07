// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;

using SharpX.ShaderLab.Library.Enum;

namespace SharpX.ShaderLab.Library.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = true)]
    public sealed class ShaderTagAttribute : Attribute
    {
        public ShaderTagAttribute(ShaderTags tag, object value) { }

        public ShaderTagAttribute(string tag, object value) { }
    }
}