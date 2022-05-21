// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Collections.Concurrent;
using System.Reflection;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpX.Composition.Interfaces;
using SharpX.Core;

namespace SharpX.Composition.CSharp;

public abstract class CompositeCSharpSyntaxVisitor<TResult> : CSharpSyntaxVisitor<TResult> where TResult : SyntaxNode
{
    private static readonly Func<TResult?, TResult?> EmptyVisit = t => t;

    // it is injected from compiler
    private readonly Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult?> _delegate1;
    private readonly Func<Microsoft.CodeAnalysis.SyntaxNode?, TResult?, TResult?> _delegate2;
    private readonly ConcurrentDictionary<Type, Func<TResult?, TResult?>> _emptyVisitors = new();
    private readonly ConcurrentDictionary<Type, MethodInfo> _visitors = new();

    public CompositeCSharpSyntaxVisitor(IBackendVisitorArgs<TResult> args)
    {
        _delegate1 = args.Delegate1;
        _delegate2 = args.Delegate2;
    }

    public override TResult? Visit(Microsoft.CodeAnalysis.SyntaxNode? node)
    {
        if (node != null)
        {
            var rewritten = ((CSharpSyntaxNode)node).Accept(this) ?? _delegate1.Invoke(node);
            rewritten = Accept(node, rewritten);
            rewritten = _delegate2.Invoke(node, rewritten);

            return rewritten;
        }

        return default;
    }

    public TResult? Visit(Microsoft.CodeAnalysis.SyntaxNode? node, TResult? newNode)
    {
        if (node != null)
            return Accept(node, newNode) ?? _delegate2.Invoke(node, newNode);

        return newNode;
    }

    private TResult? Accept(Microsoft.CodeAnalysis.SyntaxNode oldNode, TResult? newNode)
    {
        var t = oldNode.GetType();
        if (_visitors.ContainsKey(t))
            return (TResult?)_visitors[t].Invoke(this, new object?[] { oldNode, newNode });
        if (_emptyVisitors.ContainsKey(t))
            return _emptyVisitors[t].Invoke(newNode);

        var self = GetType();
        var visit = self.GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault(w => w.GetParameters().Length == 1 && w.GetParameters()[0].ParameterType == oldNode.GetType());
        if (visit == null)
            return newNode;

        var visitor = self.GetMethod(visit.Name, BindingFlags.Instance | BindingFlags.Public, new[] { visit.GetParameters()[0].ParameterType, typeof(TResult) });
        if (visitor != null)
        {
            _visitors.AddOrUpdate(t, visitor, (_, w) => w);
            return (TResult?)visitor.Invoke(this, new object?[] { oldNode, newNode });
        }

        _emptyVisitors.AddOrUpdate(t, EmptyVisit, (_, w) => w);
        return newNode;
    }


    public virtual TResult? VisitIdentifierName(IdentifierNameSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitQualifiedName(QualifiedNameSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitGenericName(GenericNameSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitTypeArgumentList(TypeArgumentListSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitAliasQualifiedName(AliasQualifiedNameSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitPredefinedType(PredefinedTypeSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitArrayType(ArrayTypeSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitArrayRankSpecifier(ArrayRankSpecifierSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitPointerType(PointerTypeSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitFunctionPointerType(FunctionPointerTypeSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitFunctionPointerParameterList(FunctionPointerParameterListSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitFunctionPointerCallingConvention(FunctionPointerCallingConventionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitFunctionPointerUnmanagedCallingConventionList(FunctionPointerUnmanagedCallingConventionListSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitFunctionPointerUnmanagedCallingConvention(FunctionPointerUnmanagedCallingConventionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitNullableType(NullableTypeSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitTupleType(TupleTypeSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitTupleElement(TupleElementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitOmittedTypeArgument(OmittedTypeArgumentSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitRefType(RefTypeSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitParenthesizedExpression(ParenthesizedExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitTupleExpression(TupleExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitPrefixUnaryExpression(PrefixUnaryExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitAwaitExpression(AwaitExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitPostfixUnaryExpression(PostfixUnaryExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitMemberAccessExpression(MemberAccessExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitConditionalAccessExpression(ConditionalAccessExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitMemberBindingExpression(MemberBindingExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitElementBindingExpression(ElementBindingExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitRangeExpression(RangeExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitImplicitElementAccess(ImplicitElementAccessSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitBinaryExpression(BinaryExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitAssignmentExpression(AssignmentExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitConditionalExpression(ConditionalExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitThisExpression(ThisExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitBaseExpression(BaseExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitLiteralExpression(LiteralExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitMakeRefExpression(MakeRefExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitRefTypeExpression(RefTypeExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitRefValueExpression(RefValueExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitCheckedExpression(CheckedExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitDefaultExpression(DefaultExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitTypeOfExpression(TypeOfExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitSizeOfExpression(SizeOfExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitInvocationExpression(InvocationExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitElementAccessExpression(ElementAccessExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitArgumentList(ArgumentListSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitBracketedArgumentList(BracketedArgumentListSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitArgument(ArgumentSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitExpressionColon(ExpressionColonSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitNameColon(NameColonSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitDeclarationExpression(DeclarationExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitCastExpression(CastExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitAnonymousMethodExpression(AnonymousMethodExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitSimpleLambdaExpression(SimpleLambdaExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitRefExpression(RefExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitParenthesizedLambdaExpression(ParenthesizedLambdaExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitInitializerExpression(InitializerExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitImplicitObjectCreationExpression(ImplicitObjectCreationExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitObjectCreationExpression(ObjectCreationExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitWithExpression(WithExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitAnonymousObjectMemberDeclarator(AnonymousObjectMemberDeclaratorSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitAnonymousObjectCreationExpression(AnonymousObjectCreationExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitArrayCreationExpression(ArrayCreationExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitImplicitArrayCreationExpression(ImplicitArrayCreationExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitStackAllocArrayCreationExpression(StackAllocArrayCreationExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitImplicitStackAllocArrayCreationExpression(ImplicitStackAllocArrayCreationExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitQueryExpression(QueryExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitQueryBody(QueryBodySyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitFromClause(FromClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitLetClause(LetClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitJoinClause(JoinClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitJoinIntoClause(JoinIntoClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitWhereClause(WhereClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitOrderByClause(OrderByClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitOrdering(OrderingSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitSelectClause(SelectClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitGroupClause(GroupClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitQueryContinuation(QueryContinuationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitOmittedArraySizeExpression(OmittedArraySizeExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitInterpolatedStringExpression(InterpolatedStringExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitIsPatternExpression(IsPatternExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitThrowExpression(ThrowExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitWhenClause(WhenClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitDiscardPattern(DiscardPatternSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitDeclarationPattern(DeclarationPatternSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitVarPattern(VarPatternSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitRecursivePattern(RecursivePatternSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitPositionalPatternClause(PositionalPatternClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitPropertyPatternClause(PropertyPatternClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitSubpattern(SubpatternSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitConstantPattern(ConstantPatternSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitParenthesizedPattern(ParenthesizedPatternSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitRelationalPattern(RelationalPatternSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitTypePattern(TypePatternSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitBinaryPattern(BinaryPatternSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitUnaryPattern(UnaryPatternSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitListPattern(ListPatternSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitSlicePattern(SlicePatternSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitInterpolatedStringText(InterpolatedStringTextSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitInterpolation(InterpolationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitInterpolationAlignmentClause(InterpolationAlignmentClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitInterpolationFormatClause(InterpolationFormatClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitGlobalStatement(GlobalStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitBlock(BlockSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitLocalFunctionStatement(LocalFunctionStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitLocalDeclarationStatement(LocalDeclarationStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitVariableDeclaration(VariableDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitVariableDeclarator(VariableDeclaratorSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitEqualsValueClause(EqualsValueClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitSingleVariableDesignation(SingleVariableDesignationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitDiscardDesignation(DiscardDesignationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitParenthesizedVariableDesignation(ParenthesizedVariableDesignationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitExpressionStatement(ExpressionStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitEmptyStatement(EmptyStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitLabeledStatement(LabeledStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitGotoStatement(GotoStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitBreakStatement(BreakStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitContinueStatement(ContinueStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitReturnStatement(ReturnStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitThrowStatement(ThrowStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitYieldStatement(YieldStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitWhileStatement(WhileStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitDoStatement(DoStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitForStatement(ForStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitForEachStatement(ForEachStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitForEachVariableStatement(ForEachVariableStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitUsingStatement(UsingStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitFixedStatement(FixedStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitCheckedStatement(CheckedStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitUnsafeStatement(UnsafeStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitLockStatement(LockStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitIfStatement(IfStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitElseClause(ElseClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitSwitchStatement(SwitchStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitSwitchSection(SwitchSectionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitCasePatternSwitchLabel(CasePatternSwitchLabelSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitCaseSwitchLabel(CaseSwitchLabelSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitDefaultSwitchLabel(DefaultSwitchLabelSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitSwitchExpression(SwitchExpressionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitSwitchExpressionArm(SwitchExpressionArmSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitTryStatement(TryStatementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitCatchClause(CatchClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitCatchDeclaration(CatchDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitCatchFilterClause(CatchFilterClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitFinallyClause(FinallyClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitCompilationUnit(CompilationUnitSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitExternAliasDirective(ExternAliasDirectiveSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitUsingDirective(UsingDirectiveSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitNamespaceDeclaration(NamespaceDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitAttributeList(AttributeListSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitAttributeTargetSpecifier(AttributeTargetSpecifierSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitAttribute(AttributeSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitAttributeArgumentList(AttributeArgumentListSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitAttributeArgument(AttributeArgumentSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitNameEquals(NameEqualsSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitTypeParameterList(TypeParameterListSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitTypeParameter(TypeParameterSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitClassDeclaration(ClassDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitStructDeclaration(StructDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitInterfaceDeclaration(InterfaceDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitRecordDeclaration(RecordDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitEnumDeclaration(EnumDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitDelegateDeclaration(DelegateDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitEnumMemberDeclaration(EnumMemberDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitBaseList(BaseListSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitSimpleBaseType(SimpleBaseTypeSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitPrimaryConstructorBaseType(PrimaryConstructorBaseTypeSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitTypeParameterConstraintClause(TypeParameterConstraintClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitConstructorConstraint(ConstructorConstraintSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitClassOrStructConstraint(ClassOrStructConstraintSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitTypeConstraint(TypeConstraintSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitDefaultConstraint(DefaultConstraintSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitFieldDeclaration(FieldDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitEventFieldDeclaration(EventFieldDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitExplicitInterfaceSpecifier(ExplicitInterfaceSpecifierSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitMethodDeclaration(MethodDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitOperatorDeclaration(OperatorDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitConstructorDeclaration(ConstructorDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitConstructorInitializer(ConstructorInitializerSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitDestructorDeclaration(DestructorDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitPropertyDeclaration(PropertyDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitArrowExpressionClause(ArrowExpressionClauseSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitEventDeclaration(EventDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitIndexerDeclaration(IndexerDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitAccessorList(AccessorListSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitAccessorDeclaration(AccessorDeclarationSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitParameterList(ParameterListSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitBracketedParameterList(BracketedParameterListSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitParameter(ParameterSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitFunctionPointerParameter(FunctionPointerParameterSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitIncompleteMember(IncompleteMemberSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitSkippedTokensTrivia(SkippedTokensTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitDocumentationCommentTrivia(DocumentationCommentTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitTypeCref(TypeCrefSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitQualifiedCref(QualifiedCrefSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitNameMemberCref(NameMemberCrefSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitIndexerMemberCref(IndexerMemberCrefSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitOperatorMemberCref(OperatorMemberCrefSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitConversionOperatorMemberCref(ConversionOperatorMemberCrefSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitCrefParameterList(CrefParameterListSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitCrefBracketedParameterList(CrefBracketedParameterListSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitCrefParameter(CrefParameterSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitXmlElement(XmlElementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitXmlElementStartTag(XmlElementStartTagSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitXmlElementEndTag(XmlElementEndTagSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitXmlEmptyElement(XmlEmptyElementSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitXmlName(XmlNameSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitXmlPrefix(XmlPrefixSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitXmlTextAttribute(XmlTextAttributeSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitXmlCrefAttribute(XmlCrefAttributeSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitXmlNameAttribute(XmlNameAttributeSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitXmlText(XmlTextSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitXmlCDataSection(XmlCDataSectionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitXmlProcessingInstruction(XmlProcessingInstructionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitXmlComment(XmlCommentSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitIfDirectiveTrivia(IfDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitElifDirectiveTrivia(ElifDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitElseDirectiveTrivia(ElseDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitEndIfDirectiveTrivia(EndIfDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitRegionDirectiveTrivia(RegionDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitEndRegionDirectiveTrivia(EndRegionDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitErrorDirectiveTrivia(ErrorDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitWarningDirectiveTrivia(WarningDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitBadDirectiveTrivia(BadDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitDefineDirectiveTrivia(DefineDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitUndefDirectiveTrivia(UndefDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitLineDirectiveTrivia(LineDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitLineDirectivePosition(LineDirectivePositionSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitLineSpanDirectiveTrivia(LineSpanDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitPragmaWarningDirectiveTrivia(PragmaWarningDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitPragmaChecksumDirectiveTrivia(PragmaChecksumDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitReferenceDirectiveTrivia(ReferenceDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitLoadDirectiveTrivia(LoadDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitShebangDirectiveTrivia(ShebangDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }


    public virtual TResult? VisitNullableDirectiveTrivia(NullableDirectiveTriviaSyntax oldNode, TResult? newNode)
    {
        return newNode;
    }
}