// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;

using SharpX.ShaderLab.Library.Enum;

namespace SharpX.ShaderLab.Library.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class ShaderFeatureAttribute : ShaderPragmaAttribute
    {
        public ShaderFeatureAttribute(string value) : base("target", value) { }

        public ShaderFeatureAttribute(ShaderFeatures value) : base("target", null) { }
    }
}