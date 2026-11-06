using ProjectReal.NumberSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace Calculators.Derivative;

public partial class DerivativeCalculator<T> : Calculator
where T :
	IComparisonOperators<T, T, bool>,
	IEqualityOperators<T, T, bool>,
	IUnaryNegationOperators<T, T>,
	IAdditionOperators<T, T, T>,
	ISubtractionOperators<T, T, T>,
	IMultiplyOperators<T, T, T>,
	IDivisionOperators<T, T, T>,
	IModulusOperators<T, T, T>,
	IPowerOperations<T, T, T>,
	IRootOperations<T, T, T>,
	IAdditiveIdentity<T, T>,
	IMultiplicativeIdentity<T, T>,
	IParsable<T>
{
	[GeneratedRegex(@"^'$")]
	protected static partial Regex DerivativeRegex();
	[GeneratedRegex(@"^x$")]
	protected static partial Regex XRegex();

	protected readonly FunctionToken<T>[] functionTokens;

	public DerivativeCalculator(FunctionToken<T>[] functionTokens) : base(
		[
			// Number
			new(NumberRegex(), value => new Number<T>(T.Parse(value, null))),
			// Function name
			new(FunctionNameRegex(), name => GetFunctionByName(functionTokens ?? [], name)),
			// Derivative
			new(DerivativeRegex(), _ => new Derivative<T>()),
			// Variables
			new(XRegex(), _ => new X<T>()),
			// Operators
			new(AddRegex(), _ => new Add<T>()),
			new(SubtractRegex(), _ => new Subtract<T>()),
			new(MultiplyRegex(), _ => new Multiply<T>()),
			new(DivideRegex(), _ => new Divide<T>()),
			new(PowerRegex(), _ => new Power<T>()),
			new(RootRegex(), _ => new Root<T>()),
			// Separators
			new(OpeningParenthesisRegex(), _ => new OpeningParenthesis()),
			new(ClosingParenthesisRegex(), _ => new ClosingParenthesis<T>()),
			new(ComaRegex(), _ => new Coma<T>())
		]
	) => this.functionTokens = functionTokens;

	#region Protected methods

	protected override string[] GetFunctions() => [.. functionTokens.Select(t => t.Name)];

	#endregion

	public override List<(string Calculation, string State)> FullEvaluation(string input)
	{
		ValueHolder<T> expressionTree = Derivative<T>.Simplify<X<T>>(Evaluate<T>(input, this));

		List<(string, string)> result = [];
		{
			int stepZero = 0;
			result.Add((expressionTree.ToStringByStep(ref stepZero), expressionTree.ToString()));
		}

		int step = 1;

		expressionTree.FullEvaluation(ref result, expressionTree, ref step);

		return result;
	}
}