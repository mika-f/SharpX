// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;

namespace SharpX.Core;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.SyntaxNode" />
/// </summary>
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

    #region Node Lookup

    public SyntaxNode? Parent { get; }

    #endregion

    protected SyntaxNode(GreenNode node, SyntaxNode? parent, int position)
    {
        Contract.Assert(position >= 0, "position cannot be negative");
        Contract.Assert(parent?.Green.IsList != true, "list cannot be parent");

        Green = node;
        Position = position;
        Parent = parent;
    }

    private string GetDebuggerDisplay()
    {
        return GetType().Name + " " + KindText + " " + ToString();
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
        Green.WriteTo(writer);
    }

    #region Syntax

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

    public T? GetRed<T>(ref T? field, int slot) where T : SyntaxNode
    {
        var r = field;
        if (r == null)
        {
            var green = Green.GetSlot(slot);
            if (green != null)
            {
                Interlocked.CompareExchange(ref field, (T)green.CreateRed(this, slot == 0 ? Position : GetChildPosition(slot)), null);
                r = field;
            }
        }

        return r;
    }

    public T? GetRedAtZero<T>(ref T? field) where T : SyntaxNode
    {
        return GetRed(ref field, 0);
    }

    #endregion

    #region Slots

    public virtual int GetChildPosition(int index)
    {
        var offset = 0;
        var green = Green;

        while (index > 0)
        {
            index--;

            var prev = GetCachedSlot(index);
            if (prev != null)
                return prev.EndPosition + offset;

            var child = green.GetSlot(index);
            if (child != null)
                offset += child.FullWidth;
        }

        return Position + offset;
    }

    public abstract SyntaxNode? GetNodeSlot(int index);

    public SyntaxNode GetRequiredNodeSlot(int index)
    {
        var node = GetNodeSlot(index);
        Contract.AssertNotNull(node);

        return node;
    }

    public abstract SyntaxNode? GetCachedSlot(int index);

    public int GetChildIndex(int slot)
    {
        var index = 0;
        for (var i = 0; i < slot; i++)
        {
            var item = Green.GetSlot(i);
            if (item != null)
            {
                if (item.IsList)
                    index += item.SlotCount;
                else
                    index++;
            }
        }

        return index;
    }

    #endregion
}