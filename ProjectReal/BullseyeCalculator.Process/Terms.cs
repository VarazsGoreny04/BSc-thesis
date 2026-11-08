using Calculators;
using ProjectReal.Number;

namespace BullseyeCalculator.Process;

/// <summary>
/// Represents a pi value in an expression.
/// </summary>
public sealed class PI : Term<Rational>
{
	#region Constants

	/// <returns>The sign of the term.</returns>
	public const string SIGN = "pi";

	#endregion

	#region Constructors

	/// <summary>
	/// Constructs a <see cref="PI"/> object.
	/// </summary>
	public PI() { }

	#endregion

	#region Public methods

	public override Rational GetValue() => Rational.Pi();

	public override string ToStringByStep(ref int _) => SIGN;

	#endregion
}

/// <summary>
/// Represents an e number value in an expression.
/// </summary>
public sealed class E : Term<Rational>
{
	#region Constants

	/// <returns>The sign of the term.</returns>
	public const string SIGN = "e";

	#endregion

	#region Constructors

	/// <summary>
	/// Constructs an <see cref="E"/> number object.
	/// </summary>
	public E() { }

	#endregion

	#region Public methods

	public override Rational GetValue() => Rational.E();

	public override string ToStringByStep(ref int _) => SIGN;

	#endregion
}