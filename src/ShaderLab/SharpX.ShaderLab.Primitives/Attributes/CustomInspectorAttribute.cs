// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.ShaderLab.Primitives.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class CustomInspectorAttribute : Attribute
{
    public CustomInspectorAttribute(Type t, params object[] parameters) { }

    public CustomInspectorAttribute(string str, params object[] parameters) { }
}