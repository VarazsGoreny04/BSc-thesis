using Calculators;
using ProjectReal.Number;

namespace BullseyeCalculator.Process;

/// <summary>
/// Represents an absolute value function in an expression.
/// </summary>
public sealed class Abs : Function<Rational>
{
	#region Constants

	/// <returns>The sign of the function.</returns>
	public const string SIGN = "abs";

	#endregion

	#region Properties

	/// <summary>
	/// Gets or sets the parameter of the function.
	/// </summary>
	public ValueHolder<Rational> Parameter
	{
		get => parameters[0];
		set => parameters[0] = value;
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Constructs an <see cref="Abs"/> function with the parameter set to <see langword="null"/>.
	/// </summary>
	public Abs() : base([null!]) { }

	/// <summary>
	/// Constructs an <see cref="Abs"/> function with the <paramref name="parameter"/> value.
	/// </summary>
	/// <param name="parameter">The parameter of the function.</param>
	public Abs(ValueHolder<Rational> parameter) : base([parameter]) { }

	#endregion

	#region Protected methods

	protected override Rational CalculateValue() => Rational.Abs(Parameter.GetValue());

	#endregion

	#region Public methods

	public override string Sign() => SIGN;

	#endregion
}

/// <summary>
/// Represents a floor function in an expression.
/// </summary>
public sealed class Floor : Function<Rational>
{
	#region Constants

	/// <returns>The sign of the function.</returns>
	public const string SIGN = "floor";

	#endregion

	#region Properties

	/// <summary>
	/// Gets or sets the parameter of the function.
	/// </summary>
	public ValueHolder<Rational> Parameter
	{
		get => parameters[0];
		set => parameters[0] = value;
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Constructs a <see cref="Floor"/> function with the parameter set to <see langword="null"/>.
	/// </summary>
	public Floor() : base([null!]) { }

	/// <summary>
	/// Constructs a <see cref="Floor"/> function with the <paramref name="parameter"/> value.
	/// </summary>
	/// <param name="parameter">The parameter of the function.</param>
	public Floor(ValueHolder<Rational> parameter) : base([parameter]) { }

	#endregion

	#region Protected methods

	protected override Rational CalculateValue() => Rational.RoundDown(Parameter.GetValue());

	#endregion

	#region Public methods

	public override string Sign() => SIGN;

	#endregion
}

/// <summary>
/// Represents a round function in an expression.
/// </summary>
public sealed class Round : Function<Rational>
{
	#region Constants

	/// <returns>The sign of the function.</returns>
	public const string SIGN = "round";

	#endregion

	#region Properties

	/// <summary>
	/// Gets or sets the parameter of the function.
	/// </summary>
	public ValueHolder<Rational> Parameter
	{
		get => parameters[0];
		set => parameters[0] = value;
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Constructs a <see cref="Round"/> function with the parameter set to <see langword="null"/>.
	/// </summary>
	public Round() : base([null!]) { }

	/// <summary>
	/// Constructs a <see cref="Round"/> function with the <paramref name="parameter"/> value.
	/// </summary>
	/// <param name="parameter">The parameter of the function.</param>
	public Round(ValueHolder<Rational> parameter) : base([parameter]) { }

	#endregion

	#region Protected methods

	protected override Rational CalculateValue() => Rational.Round(Parameter.GetValue());

	#endregion

	#region Public methods

	public override string Sign() => SIGN;

	#endregion
}

/// <summary>
/// Represents a ceiling function in an expression.
/// </summary>
public sealed class Ceiling : Function<Rational>
{
	#region Constants

	/// <returns>The sign of the function.</returns>
	public const string SIGN = "ceiling";

	#endregion

	#region Properties

	/// <summary>
	/// Gets or sets the parameter of the function.
	/// </summary>
	public ValueHolder<Rational> Parameter
	{
		get => parameters[0];
		set => parameters[0] = value;
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Constructs a <see cref="Ceiling"/> function with the parameter set to <see langword="null"/>.
	/// </summary>
	public Ceiling() : base([null!]) { }

	/// <summary>
	/// Constructs a <see cref="Ceiling"/> function with the <paramref name="parameter"/> value.
	/// </summary>
	/// <param name="parameter">The parameter of the function.</param>
	public Ceiling(ValueHolder<Rational> parameter) : base([parameter]) { }

	#endregion

	#region Protected methods

	protected override Rational CalculateValue() => Rational.RoundUp(Parameter.GetValue());

	#endregion

	#region Public methods

	public override string Sign() => SIGN;

	#endregion
}

/// <summary>
/// Represents the sine function in an expression.
/// </summary>
public sealed class Sin : Function<Rational>
{
	#region Constants

	/// <returns>The sign of the function.</returns>
	public const string SIGN = "sin";

	#endregion

	#region Properties

	/// <summary>
	/// Gets or sets the parameter of the function.
	/// </summary>
	public ValueHolder<Rational> Parameter
	{
		get => parameters[0];
		set => parameters[0] = value;
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Constructs a <see cref="Sin"/> function with the parameters set to <see langword="null"/>.
	/// </summary>
	public Sin() : base([null!]) { }

	/// <summary>
	/// Constructs a <see cref="Sin"/> function with the <paramref name="parameter"/> value.
	/// </summary>
	/// <param name="parameter">The parameter of the function.</param>
	public Sin(ValueHolder<Rational> parameter) : base([parameter]) { }

	#endregion

	#region Protected methods

	protected override Rational CalculateValue() => Rational.Sin(Parameter.GetValue());

	#endregion

	#region Public methods

	public override string Sign() => SIGN;

	#endregion
}

/// <summary>
/// Represents the cosine function in an expression.
/// </summary>
public sealed class Cos : Function<Rational>
{
	#region Constants

	/// <returns>The sign of the function.</returns>
	public const string SIGN = "cos";

	#endregion

	#region Properties

	/// <summary>
	/// Gets or sets the parameter of the function.
	/// </summary>
	public ValueHolder<Rational> Parameter
	{
		get => parameters[0];
		set => parameters[0] = value;
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Constructs a <see cref="Cos"/> function with the parameters set to <see langword="null"/>.
	/// </summary>
	public Cos() : base([null!]) { }

	/// <summary>
	/// Constructs a <see cref="Cos"/> function with the <paramref name="parameter"/> value.
	/// </summary>
	/// <param name="parameter">The parameter of the function.</param>
	public Cos(ValueHolder<Rational> parameter) : base([parameter]) { }

	#endregion

	#region Protected methods

	protected override Rational CalculateValue() => Rational.Cos(Parameter.GetValue());

	#endregion

	#region Public methods

	public override string Sign() => SIGN;

	#endregion
}


/// <summary>
/// Represents the exponential function in an expression.
/// </summary>
public sealed class Exp : Function<Rational>
{
	#region Constants

	/// <returns>The sign of the function.</returns>
	public const string SIGN = "exp";

	#endregion

	#region Properties

	/// <summary>
	/// Gets or sets the parameter of the function.
	/// </summary>
	public ValueHolder<Rational> Parameter
	{
		get => parameters[0];
		set => parameters[0] = value;
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Constructs a <see cref="Exp"/> function with the parameters set to <see langword="null"/>.
	/// </summary>
	public Exp() : base([null!]) { }

	/// <summary>
	/// Constructs a <see cref="Exp"/> function with the <paramref name="parameter"/> value.
	/// </summary>
	/// <param name="parameter">The parameter of the function.</param>
	public Exp(ValueHolder<Rational> parameter) : base([parameter]) { }

	#endregion

	#region Protected methods

	protected override Rational CalculateValue() => Rational.Exp(Parameter.GetValue());

	#endregion

	#region Public methods

	public override string Sign() => SIGN;

	#endregion
}

/// <summary>
/// Represents the natural logarithm function in an expression.
/// </summary>
public sealed class Ln : Function<Rational>
{
	#region Constants

	/// <returns>The sign of the function.</returns>
	public const string SIGN = "ln";

	#endregion

	#region Properties

	/// <summary>
	/// Gets or sets the parameter of the function.
	/// </summary>
	public ValueHolder<Rational> Parameter
	{
		get => parameters[0];
		set => parameters[0] = value;
	}

	#endregion

	#region Constructors

	/// <summary>
	/// Constructs a <see cref="Ln"/> function with the parameters set to <see langword="null"/>.
	/// </summary>
	public Ln() : base([null!]) { }

	/// <summary>
	/// Constructs a <see cref="Ln"/> function with the <paramref name="parameter"/> value.
	/// </summary>
	/// <param name="parameter">The parameter of the function.</param>
	public Ln(ValueHolder<Rational> parameter) : base([parameter]) { }

	#endregion

	#region Protected methods

	protected override Rational CalculateValue() => Rational.Ln(Parameter.GetValue());

	#endregion

	#region Public methods

	public override string Sign() => SIGN;

	#endregion
}