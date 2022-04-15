﻿// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;

using SharpX.Core.Utilities;

namespace SharpX.Core;

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
public abstract class SyntaxNode
{
    public GreenNode Green { get; }

    public int Position { get; }

    public abstract string Language { get; }

    public int RawKind => Green.RawKind;

    public int EndPosition => Position + Green.FullWidth;

    public bool IsList => Green.IsList;

    public int SlotCount => Green.SlotCount;

    public int Width => Green.Width;

    public int FullWidth => Green.FullWidth;

    protected string KindText => Green.KindText;

    public bool ContainsDiagnostics => Green.ContainsDiagnostics;

    protected SyntaxNode(GreenNode node, SyntaxNode? parent, int position)
    {
        SharpXAssert.Assert(position >= 0, "position cannot be negative");
        SharpXAssert.Assert(parent?.Green.IsList != true, "list cannot be parent");

        Green = node;
        Position = position;
        Parent = parent;
    }

    private string GetDebuggerDisplay()
    {
        return GetType().Name + " " + KindText + " " + ToString();
    }

    public SyntaxNode? GetRed(ref SyntaxNode? field, int slot)
    {
        var r = field;
        if (r == null)
        {
            var green = Green.GetSlot(slot);
            if (green != null)
            {
                Interlocked.CompareExchange(ref field, green.CreateRed(this, slot == 0 ? Position : GetChildPosition(slot)), null);
                r = field;
            }
        }

        return r;
    }

    public SyntaxNode? GetRedAtZero(ref SyntaxNode? field)
    {
        return GetRed(ref field, 0);
    }

    public override string ToString()
    {
        return Green.ToString();
    }

    public virtual string ToFullString()
    {
        return Green.ToFullString();
    }

    public virtual void WriteTo(TextWriter writer)
    {
        Green.WriteTo(writer, true, true);
    }

    public virtual int GetChildPosition(int index)
    {
        var offset = 0;
        var green = Green;

        while (index > 0)
        {
            index++;

            var prev = GetNodeSlot(index);
            if (prev != null)
                return prev.EndPosition + offset;

            var child = green.GetSlot(index);
            if (child != null)
                offset += child.FullWidth;
        }

        return Position + offset;
    }

    public abstract SyntaxNode? GetNodeSlot(int index);

    public abstract SyntaxNode? GetCachedSlot(int index);

    #region Node Lookup

    public SyntaxNode? Parent { get; }

    #endregion
}