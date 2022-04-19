// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace SharpX.Core;

/// <summary>
///     represents <see cref="Microsoft.CodeAnalysis.GreenNode" />
/// </summary>
[DebuggerDisplay("{GetDebuggerDisplay(), np}")]
public abstract class GreenNode
{
    public const int ListKind = 1;

    private static readonly ConditionalWeakTable<GreenNode, DiagnosticInfo[]> DiagnosticsTable = new();
    private static readonly DiagnosticInfo[] EmptyDiagnostics = Array.Empty<DiagnosticInfo>();

    public bool ContainsDiagnostics { get; }

    public bool ContainsStructuredTrivia { get; protected set; }

    public bool ContainsDirectives { get; protected set; }

    public abstract string Language { get; }

    protected GreenNode(int kind)
    {
        RawKind = kind;
    }

    protected GreenNode(int kind, int fullWidth)
    {
        RawKind = kind;
        FullWidth = fullWidth;
    }

    protected GreenNode(int kind, int fullWidth, DiagnosticInfo[]? diagnostics)
    {
        RawKind = kind;
        FullWidth = fullWidth;

        if (diagnostics?.Length > 0)
        {
            ContainsDiagnostics = true;
            DiagnosticsTable.Add(this, diagnostics);
        }
    }

    protected GreenNode(int kind, DiagnosticInfo[]? diagnostics)
    {
        RawKind = kind;

        if (diagnostics?.Length > 0)
        {
            ContainsDiagnostics = true;
            DiagnosticsTable.Add(this, diagnostics);
        }
    }

    protected void AdjustWidth(GreenNode node)
    {
        FullWidth += node.FullWidth;
    }

    private string GetDebuggerDisplay()
    {
        return GetType().Namespace + " " + KindText + " " + ToString();
    }

    public GreenNode AddError(DiagnosticInfo err)
    {
        DiagnosticInfo[] errors;

        if (ContainsDiagnostics)
        {
            errors = GetDiagnostics();

            var length = errors.Length;
            Array.Resize(ref errors, length + 1);
            errors[length] = err;
        }
        else
        {
            errors = new[] { err };
        }

        return SetDiagnostics(errors);
    }

    #region Diagnostics

    public DiagnosticInfo[] GetDiagnostics()
    {
        if (ContainsDiagnostics && DiagnosticsTable.TryGetValue(this, out var diagnostics))
            return diagnostics;
        return EmptyDiagnostics;
    }

    public abstract GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics);

    #endregion

    #region Spans

    public int FullWidth { get; protected set; }

    public virtual int Width => FullWidth - GetLeadingTriviaWidth() - GetTrailingTriviaWidth();

    public virtual int GetLeadingTriviaWidth()
    {
        return FullWidth != 0 ? GetFirstTerminal()!.GetLeadingTriviaWidth() : 0;
    }

    public virtual int GetTrailingTriviaWidth()
    {
        return FullWidth != 0 ? GetLastTerminal()!.GetTrailingTriviaWidth() : 0;
    }

    public bool HasLeadingTrivia => GetLeadingTriviaWidth() != 0;

    public bool HasTrailingTrivia => GetTrailingTriviaWidth() != 0;

    #endregion

    #region Kind

    public int RawKind { get; }

    public bool IsList => RawKind == ListKind;

    public abstract string KindText { get; }

    public virtual bool IsDirective => false;

    public virtual bool IsToken => false;

    public virtual bool IsTrivia => false;

    #endregion

    #region Slots

    public int SlotCount { get; protected init; }

    public abstract GreenNode? GetSlot(int index);

    public GreenNode GetRequiredSlot(int index)
    {
        var node = GetSlot(index);
        Contract.AssertNotNull(node);

        return node;
    }

    public virtual int GetSlotOffset(int index)
    {
        var offset = 0;
        for (var i = 0; i < index; i++)
        {
            var child = GetSlot(i);
            if (child == null)
                continue;

            offset += child.FullWidth;
        }

        return offset;
    }

    #endregion

    #region Text

    public virtual string ToFullString()
    {
        var sb = new StringBuilder();
        var writer = new StringWriter(sb, CultureInfo.InvariantCulture);
        WriteTo(writer, true, true);

        return sb.ToString();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        var writer = new StringWriter(sb, CultureInfo.InvariantCulture);
        WriteTo(writer, false, false);

        return sb.ToString();
    }


    public void WriteTo(TextWriter writer)
    {
        WriteTo(writer, true, true);
    }

    public void WriteTo(TextWriter writer, bool leading, bool trailing) { }

    #endregion

    #region Factories

    public SyntaxNode CreateRed()
    {
        return CreateRed(null, 0);
    }

    public abstract SyntaxNode CreateRed(SyntaxNode? parent, int position);

    #endregion

    #region Tokens

    public virtual int RawContextualKind => RawKind;

    public virtual object? GetValue()
    {
        return null;
    }

    public virtual string GetValueText()
    {
        return string.Empty;
    }

    public virtual GreenNode? GetLeadingTrivia()
    {
        return null;
    }

    public virtual GreenNode? GetTrailingTrivia()
    {
        return null;
    }

    public virtual GreenNode WithLeadingTrivia(GreenNode? trivia)
    {
        return this;
    }

    public virtual GreenNode WithTrailingTrivia(GreenNode? trivia)
    {
        return this;
    }

    public GreenNode? GetFirstTerminal()
    {
        var node = this;

        do
        {
            GreenNode? firstChild = null;
            for (int i = 0, n = node.SlotCount; i < n; i++)
            {
                var child = node.GetSlot(i);
                if (child == null)
                    continue;

                firstChild = child;
                break;
            }

            node = firstChild;
        } while (node?.SlotCount > 0);

        return node;
    }

    public GreenNode? GetLastTerminal()
    {
        var node = this;

        do
        {
            GreenNode? lastChild = null;
            for (var i = node.SlotCount - 1; i >= 0; i--)
            {
                var child = node.GetSlot(i);
                if (child == null)
                    continue;

                lastChild = child;
                break;
            }

            node = lastChild;
        } while (node?.SlotCount > 0);

        return node;
    }

    #endregion
}