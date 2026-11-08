using Calculators.Standard;
using ProjectReal.NumberSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Calculators.EuclideanSpace;

/// <summary>
/// A calculator that understands matrices and can perform operations with them.
/// </summary>
/// <typeparam name="T">The type to calculate with.</typeparam>
public sealed partial class EuclideanSpaceCalculator<T> : Calculator
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
	#region Fields

	protected readonly FunctionToken<Matrix<T>>[] functionTokens = [];

	#endregion

	#region Constructors

	/// <summary>
	/// Constructs a calculator that understands matrices and can perform operations with them.
	/// </summary>
	public EuclideanSpaceCalculator(FunctionToken<Matrix<T>>[] functionTokens, StandardCalculator<T> standardCalculator) : base(
		[
			// Matrix
			new(MatrixHolder<T>.Regex(), value => new MatrixHolder<T>(new Matrix<T>(value[1..^1], standardCalculator))),
			// Function name
			new(Function<T>.Regex(), name => GetFunctionByName(functionTokens, name)),
			// Operators
			new(Add<T>.Regex(), _ => new Add<Matrix<T>>()),
			new(Subtract<T>.Regex(), _ => new Subtract<Matrix<T>>()),
			new(Multiply<T>.Regex(), _ => new Multiply<Matrix<T>>()),
			new(Divide<T>.Regex(), _ => new Divide<Matrix<T>>()),
			new(Modulo<T>.Regex(), _ => new Modulo<Matrix<T>>()),
			// Separators
			new(OpeningParenthesis.Regex(), _ => new OpeningParenthesis()),
			new(ClosingParenthesis<T>.Regex(), _ => new ClosingParenthesis<Matrix<T>>()),
			new(Coma<T>.Regex(), _ => new Coma<Matrix<T>>())
		]
	) => this.functionTokens = functionTokens;

	#endregion

	#region Protected methods

	protected override string[] GetFunctions() => [.. functionTokens.Select(t => t.Name)];

	#endregion

	#region Public methods

	public override List<(string Calculation, string State)> FullEvaluation(string input) => FullEvaluation(Evaluate<Matrix<T>>(input, this));

	#endregion
}