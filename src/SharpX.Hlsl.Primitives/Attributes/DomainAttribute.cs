using SharpX.Hlsl.Primitives.Enums;

namespace SharpX.Hlsl.Primitives.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class DomainAttribute : Attribute
{
    public DomainAttribute(DomainParameters parameter) { }
}