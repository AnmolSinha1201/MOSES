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
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="MosesParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.3")]
[System.CLSCompliant(false)]
public interface IMosesListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by the <c>functionFetch</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionFetch([NotNull] MosesParser.FunctionFetchContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>functionFetch</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionFetch([NotNull] MosesParser.FunctionFetchContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>unaryVar</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterUnaryVar([NotNull] MosesParser.UnaryVarContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>unaryVar</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitUnaryVar([NotNull] MosesParser.UnaryVarContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>expBitwise</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpBitwise([NotNull] MosesParser.ExpBitwiseContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expBitwise</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpBitwise([NotNull] MosesParser.ExpBitwiseContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>expOR</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpOR([NotNull] MosesParser.ExpORContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expOR</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpOR([NotNull] MosesParser.ExpORContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>expMulDivMod</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpMulDivMod([NotNull] MosesParser.ExpMulDivModContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expMulDivMod</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpMulDivMod([NotNull] MosesParser.ExpMulDivModContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>constantExp</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterConstantExp([NotNull] MosesParser.ConstantExpContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>constantExp</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitConstantExp([NotNull] MosesParser.ConstantExpContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>variableFetch</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariableFetch([NotNull] MosesParser.VariableFetchContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>variableFetch</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariableFetch([NotNull] MosesParser.VariableFetchContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>variableAssign</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariableAssign([NotNull] MosesParser.VariableAssignContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>variableAssign</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariableAssign([NotNull] MosesParser.VariableAssignContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>expAND</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpAND([NotNull] MosesParser.ExpANDContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expAND</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpAND([NotNull] MosesParser.ExpANDContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>expOpPow</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpOpPow([NotNull] MosesParser.ExpOpPowContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expOpPow</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpOpPow([NotNull] MosesParser.ExpOpPowContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>newClassObject</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNewClassObject([NotNull] MosesParser.NewClassObjectContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>newClassObject</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNewClassObject([NotNull] MosesParser.NewClassObjectContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>expCompare</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpCompare([NotNull] MosesParser.ExpCompareContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expCompare</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpCompare([NotNull] MosesParser.ExpCompareContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>expConcat</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpConcat([NotNull] MosesParser.ExpConcatContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expConcat</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpConcat([NotNull] MosesParser.ExpConcatContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>expAddSub</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpAddSub([NotNull] MosesParser.ExpAddSubContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>expAddSub</c>
	/// labeled alternative in <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpAddSub([NotNull] MosesParser.ExpAddSubContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>number</c>
	/// labeled alternative in <see cref="MosesParser.constExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumber([NotNull] MosesParser.NumberContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>number</c>
	/// labeled alternative in <see cref="MosesParser.constExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumber([NotNull] MosesParser.NumberContext context);

	/// <summary>
	/// Enter a parse tree produced by the <c>string</c>
	/// labeled alternative in <see cref="MosesParser.constExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterString([NotNull] MosesParser.StringContext context);
	/// <summary>
	/// Exit a parse tree produced by the <c>string</c>
	/// labeled alternative in <see cref="MosesParser.constExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitString([NotNull] MosesParser.StringContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.chunk"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterChunk([NotNull] MosesParser.ChunkContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.chunk"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitChunk([NotNull] MosesParser.ChunkContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterBlock([NotNull] MosesParser.BlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.block"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitBlock([NotNull] MosesParser.BlockContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.innerfunctionBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterInnerfunctionBlock([NotNull] MosesParser.InnerfunctionBlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.innerfunctionBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitInnerfunctionBlock([NotNull] MosesParser.InnerfunctionBlockContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.loopBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLoopBlock([NotNull] MosesParser.LoopBlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.loopBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLoopBlock([NotNull] MosesParser.LoopBlockContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.classBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterClassBlock([NotNull] MosesParser.ClassBlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.classBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitClassBlock([NotNull] MosesParser.ClassBlockContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.localConstVarAssign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLocalConstVarAssign([NotNull] MosesParser.LocalConstVarAssignContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.localConstVarAssign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLocalConstVarAssign([NotNull] MosesParser.LocalConstVarAssignContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.classDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterClassDecl([NotNull] MosesParser.ClassDeclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.classDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitClassDecl([NotNull] MosesParser.ClassDeclContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.functionDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionDecl([NotNull] MosesParser.FunctionDeclContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.functionDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionDecl([NotNull] MosesParser.FunctionDeclContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.whileLoop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWhileLoop([NotNull] MosesParser.WhileLoopContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.whileLoop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWhileLoop([NotNull] MosesParser.WhileLoopContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.loop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLoop([NotNull] MosesParser.LoopContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.loop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLoop([NotNull] MosesParser.LoopContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.loops"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLoops([NotNull] MosesParser.LoopsContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.loops"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLoops([NotNull] MosesParser.LoopsContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.varAssign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVarAssign([NotNull] MosesParser.VarAssignContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.varAssign"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVarAssign([NotNull] MosesParser.VarAssignContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.functionDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionDef([NotNull] MosesParser.FunctionDefContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.functionDef"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionDef([NotNull] MosesParser.FunctionDefContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.functionParameterList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionParameterList([NotNull] MosesParser.FunctionParameterListContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.functionParameterList"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionParameterList([NotNull] MosesParser.FunctionParameterListContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.functionParameterDefault"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionParameterDefault([NotNull] MosesParser.FunctionParameterDefaultContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.functionParameterDefault"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionParameterDefault([NotNull] MosesParser.FunctionParameterDefaultContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.functionParameterNoDefault"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionParameterNoDefault([NotNull] MosesParser.FunctionParameterNoDefaultContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.functionParameterNoDefault"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionParameterNoDefault([NotNull] MosesParser.FunctionParameterNoDefaultContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.functionPrameterVariadic"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionPrameterVariadic([NotNull] MosesParser.FunctionPrameterVariadicContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.functionPrameterVariadic"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionPrameterVariadic([NotNull] MosesParser.FunctionPrameterVariadicContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.variadic"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariadic([NotNull] MosesParser.VariadicContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.variadic"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariadic([NotNull] MosesParser.VariadicContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.ref"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRef([NotNull] MosesParser.RefContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.ref"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRef([NotNull] MosesParser.RefContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.functionBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionBody([NotNull] MosesParser.FunctionBodyContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.functionBody"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionBody([NotNull] MosesParser.FunctionBodyContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.segmentBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSegmentBlock([NotNull] MosesParser.SegmentBlockContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.segmentBlock"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSegmentBlock([NotNull] MosesParser.SegmentBlockContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.this"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterThis([NotNull] MosesParser.ThisContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.this"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitThis([NotNull] MosesParser.ThisContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.complexFunctionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterComplexFunctionCall([NotNull] MosesParser.ComplexFunctionCallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.complexFunctionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitComplexFunctionCall([NotNull] MosesParser.ComplexFunctionCallContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.complexVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterComplexVariable([NotNull] MosesParser.ComplexVariableContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.complexVariable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitComplexVariable([NotNull] MosesParser.ComplexVariableContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.variableOrFunction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVariableOrFunction([NotNull] MosesParser.VariableOrFunctionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.variableOrFunction"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVariableOrFunction([NotNull] MosesParser.VariableOrFunctionContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.var"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterVar([NotNull] MosesParser.VarContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.var"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitVar([NotNull] MosesParser.VarContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterFunctionCall([NotNull] MosesParser.FunctionCallContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.functionCall"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitFunctionCall([NotNull] MosesParser.FunctionCallContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExp([NotNull] MosesParser.ExpContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.exp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExp([NotNull] MosesParser.ExpContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.constExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterConstExp([NotNull] MosesParser.ConstExpContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.constExp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitConstExp([NotNull] MosesParser.ConstExpContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.operatorOr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorOr([NotNull] MosesParser.OperatorOrContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.operatorOr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorOr([NotNull] MosesParser.OperatorOrContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.operatorAnd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorAnd([NotNull] MosesParser.OperatorAndContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.operatorAnd"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorAnd([NotNull] MosesParser.OperatorAndContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.operatorComparison"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorComparison([NotNull] MosesParser.OperatorComparisonContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.operatorComparison"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorComparison([NotNull] MosesParser.OperatorComparisonContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.operatorPower"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorPower([NotNull] MosesParser.OperatorPowerContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.operatorPower"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorPower([NotNull] MosesParser.OperatorPowerContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.operatorUnary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorUnary([NotNull] MosesParser.OperatorUnaryContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.operatorUnary"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorUnary([NotNull] MosesParser.OperatorUnaryContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.operatorAddSub"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorAddSub([NotNull] MosesParser.OperatorAddSubContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.operatorAddSub"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorAddSub([NotNull] MosesParser.OperatorAddSubContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.operatorMulDivMod"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorMulDivMod([NotNull] MosesParser.OperatorMulDivModContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.operatorMulDivMod"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorMulDivMod([NotNull] MosesParser.OperatorMulDivModContext context);

	/// <summary>
	/// Enter a parse tree produced by <see cref="MosesParser.operatorBitwise"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterOperatorBitwise([NotNull] MosesParser.OperatorBitwiseContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="MosesParser.operatorBitwise"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitOperatorBitwise([NotNull] MosesParser.OperatorBitwiseContext context);
}
} // namespace MOSES
