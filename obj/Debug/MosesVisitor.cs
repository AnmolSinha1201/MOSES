//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from C:\Users\Nick\Documents\Visual Studio 2015\Projects\MOSES\Moses.g4 by ANTLR 4.5.3

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

namespace MOSES {
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="MosesParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.3")]
[System.CLSCompliant(false)]
public interface IMosesVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by the <c>functionFetch</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionFetch([NotNull] MosesParser.FunctionFetchContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>unaryVar</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnaryVar([NotNull] MosesParser.UnaryVarContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>expBitwise</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpBitwise([NotNull] MosesParser.ExpBitwiseContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>expOR</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpOR([NotNull] MosesParser.ExpORContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>expMulDivMod</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpMulDivMod([NotNull] MosesParser.ExpMulDivModContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>constantExp</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstantExp([NotNull] MosesParser.ConstantExpContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>variableFetch</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableFetch([NotNull] MosesParser.VariableFetchContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>variableAssign</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableAssign([NotNull] MosesParser.VariableAssignContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>expAND</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpAND([NotNull] MosesParser.ExpANDContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>expOpPow</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpOpPow([NotNull] MosesParser.ExpOpPowContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>unaryIncrDecr</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnaryIncrDecr([NotNull] MosesParser.UnaryIncrDecrContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>newClassObject</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNewClassObject([NotNull] MosesParser.NewClassObjectContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>expCompare</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpCompare([NotNull] MosesParser.ExpCompareContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>expConcat</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpConcat([NotNull] MosesParser.ExpConcatContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>expAddSub</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExpAddSub([NotNull] MosesParser.ExpAddSubContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>number</c>
	/// labeled alternative in <see cref="MosesParser.constExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNumber([NotNull] MosesParser.NumberContext context);

	/// <summary>
	/// Visit a parse tree produced by the <c>string</c>
	/// labeled alternative in <see cref="MosesParser.constExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitString([NotNull] MosesParser.StringContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.chunk"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitChunk([NotNull] MosesParser.ChunkContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBlock([NotNull] MosesParser.BlockContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.innerfunctionBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInnerfunctionBlock([NotNull] MosesParser.InnerfunctionBlockContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.tryCatchFinally"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTryCatchFinally([NotNull] MosesParser.TryCatchFinallyContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.ifElseLadder"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIfElseLadder([NotNull] MosesParser.IfElseLadderContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.returnBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitReturnBlock([NotNull] MosesParser.ReturnBlockContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.loopBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLoopBlock([NotNull] MosesParser.LoopBlockContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.classBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClassBlock([NotNull] MosesParser.ClassBlockContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.localConstVarAssign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLocalConstVarAssign([NotNull] MosesParser.LocalConstVarAssignContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.classDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitClassDecl([NotNull] MosesParser.ClassDeclContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.functionDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionDecl([NotNull] MosesParser.FunctionDeclContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.whileLoop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitWhileLoop([NotNull] MosesParser.WhileLoopContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.loop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLoop([NotNull] MosesParser.LoopContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.loopParse"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLoopParse([NotNull] MosesParser.LoopParseContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.loops"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLoops([NotNull] MosesParser.LoopsContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.varAssign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVarAssign([NotNull] MosesParser.VarAssignContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.functionDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionDef([NotNull] MosesParser.FunctionDefContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.functionParameterList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionParameterList([NotNull] MosesParser.FunctionParameterListContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.functionParameterDefault"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionParameterDefault([NotNull] MosesParser.FunctionParameterDefaultContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.functionParameterNoDefault"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionParameterNoDefault([NotNull] MosesParser.FunctionParameterNoDefaultContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.functionPrameterVariadic"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionPrameterVariadic([NotNull] MosesParser.FunctionPrameterVariadicContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.variadic"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariadic([NotNull] MosesParser.VariadicContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.ref"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRef([NotNull] MosesParser.RefContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.functionBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionBody([NotNull] MosesParser.FunctionBodyContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.segmentBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSegmentBlock([NotNull] MosesParser.SegmentBlockContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.this"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitThis([NotNull] MosesParser.ThisContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.complexFunctionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComplexFunctionCall([NotNull] MosesParser.ComplexFunctionCallContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.complexVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitComplexVariable([NotNull] MosesParser.ComplexVariableContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.variableOrFunction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariableOrFunction([NotNull] MosesParser.VariableOrFunctionContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.var"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVar([NotNull] MosesParser.VarContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFunctionCall([NotNull] MosesParser.FunctionCallContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.newInstance"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNewInstance([NotNull] MosesParser.NewInstanceContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExp([NotNull] MosesParser.ExpContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.unaryOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnaryOp([NotNull] MosesParser.UnaryOpContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.constExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstExp([NotNull] MosesParser.ConstExpContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.prePostIncrDecr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPrePostIncrDecr([NotNull] MosesParser.PrePostIncrDecrContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.preIncrDecr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPreIncrDecr([NotNull] MosesParser.PreIncrDecrContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.postIncrDecr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPostIncrDecr([NotNull] MosesParser.PostIncrDecrContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.incrDecr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIncrDecr([NotNull] MosesParser.IncrDecrContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.operatorOr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperatorOr([NotNull] MosesParser.OperatorOrContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.operatorAnd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperatorAnd([NotNull] MosesParser.OperatorAndContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.operatorComparison"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperatorComparison([NotNull] MosesParser.OperatorComparisonContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.operatorPower"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperatorPower([NotNull] MosesParser.OperatorPowerContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.operatorUnary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperatorUnary([NotNull] MosesParser.OperatorUnaryContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.operatorAddSub"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperatorAddSub([NotNull] MosesParser.OperatorAddSubContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.operatorMulDivMod"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperatorMulDivMod([NotNull] MosesParser.OperatorMulDivModContext context);

	/// <summary>
	/// Visit a parse tree produced by <see cref="MosesParser.operatorBitwise"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitOperatorBitwise([NotNull] MosesParser.OperatorBitwiseContext context);
}
} // namespace MOSES
