using ProjectReal.NumberSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Calculators.Standard;

/// <summary>
/// A calculator that understands the basic numbers, operations and trigonometric functions.
/// </summary>
/// <typeparam name="T">The type to calculate with.</typeparam>
public sealed class StandardCalculator<T> : Calculator
where T :
	IAdditiveIdentity<T, T>,
	IAdditionOperators<T, T, T>,
	ISubtractionOperators<T, T, T>,
	IUnaryNegationOperators<T, T>,
	IMultiplyOperators<T, T, T>,
	IDivisionOperators<T, T, T>,
	IModulusOperators<T, T, T>,
	IPowerOperations<T, T, T>,
	IRootOperations<T, T, T>,
	IParsable<T>
{
	#region Fields

	private readonly FunctionToken<T>[] functionTokens = [];

	#endregion

	#region Constructors

	/// <summary>
	/// Constructs a calculator that understands the basic numbers, operations and trigonometric functions.
	/// </summary>
	public StandardCalculator(FunctionToken<T>[] functionTokens) : base(
		[
			// Number
			new(Number<T>.Regex(), value => new Number<T>(T.Parse(value, null))),
			// Function name
			new(Function<T>.Regex(), name => GetFunctionByName(functionTokens, name)),
			// Operators
			new(Add<T>.Regex(), _ => new Add<T>()),
			new(Subtract<T>.Regex(), _ => new Subtract<T>()),
			new(Multiply<T>.Regex(), _ => new Multiply<T>()),
			new(Divide<T>.Regex(), _ => new Divide<T>()),
			new(Modulo<T>.Regex(), _ => new Modulo<T>()),
			new(Power<T>.Regex(), _ => new Power<T>()),
			new(Root<T>.Regex(), _ => new Root<T>()),
			// Separators
			new(OpeningParenthesis.Regex(), _ => new OpeningParenthesis()),
			new(ClosingParenthesis<T>.Regex(), _ => new ClosingParenthesis<T>()),
			new(Coma<T>.Regex(), _ => new Coma<T>())
		]
	) => this.functionTokens = functionTokens;

	#endregion

	#region Protected methods

	protected override string[] GetFunctions() => [.. functionTokens.Select(t => t.Name)];

	#endregion

	#region Public methods

	public override List<(string Calculation, string State)> FullEvaluation(string input) => FullEvaluation(Evaluate<T>(input, this));

	#endregion
}