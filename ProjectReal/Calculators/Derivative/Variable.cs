using ProjectReal.NumberSet;
using System.Collections.Generic;
using System.Numerics;

namespace Calculators.Derivative;

public abstract class Variable<T> : FunctionBase<T>
where T :
	IEqualityOperators<T, T, bool>,
	IMultiplyOperators<T, T, T>,
	IPowerOperations<T, T, T>,
	IAdditiveIdentity<T, T>,
	IMultiplicativeIdentity<T, T>
{
	#region Properties

	public ValueHolder<T> Coefficient
	{
		get => parameters[0];
		set => parameters[0] = value;
	}
	public ValueHolder<T> Power
	{
		get => parameters[1];
		set => parameters[1] = value;
	}

	#endregion

	#region Constructors

	public Variable() : base([new Number<T>(T.MultiplicativeIdentity), new Number<T>(T.AdditiveIdentity)]) { }

	public Variable(ValueHolder<T>? coefficient, ValueHolder<T>? power) :
		base([coefficient ?? new Number<T>(T.MultiplicativeIdentity), power ?? new Number<T>(T.AdditiveIdentity)])
	{ }

	#endregion

	#region Protected methods

	protected abstract T VariableValue();

	protected virtual Variable<T> Create(ValueHolder<T>? coefficient, ValueHolder<T>? power) { throw new System.Exception(); }

	protected override T CalculateValue() => Coefficient.GetValue() * (VariableValue() ^ Power.GetValue());

	protected static (bool ShowCoefficient, bool ShowSign, bool ShowPower) WhatToShow(Variable<T> v)
	{
		bool coefficient = true;

		if (v.Coefficient is Number<T> c)
		{
			if (c.GetValue() == T.AdditiveIdentity)
				return (true, false, false);

			if (c.GetValue() == T.MultiplicativeIdentity)
				coefficient = false;
		}

		bool power = true;

		if (v.Power is Number<T> p)
		{
			if (p.GetValue() == T.AdditiveIdentity)
				return (true, false, false);

			if (p.GetValue() == T.MultiplicativeIdentity)
				power = false;
		}

		return (coefficient, true, power);
	}

	#endregion

	#region Internal methods

	internal override void ToTree(ref Stack<Expression> result) => result.Push(this);

	internal override Priority Order() => Priority.Function;

	#endregion

	#region Public methods

	public override void FullEvaluation(ref List<(string, string)> partialValues, ValueHolder<T> root, ref int step)
	{
		Coefficient.FullEvaluation(ref partialValues, root, ref step);
		Power.FullEvaluation(ref partialValues, root, ref step);

		int stepCopy = ++step;

		string coefficient = ParenthesizeIfSigned(Coefficient.GetValue());
		string power = ParenthesizeIfSigned(Power.GetValue());

		(bool showCoefficient, bool showSign, bool showPower) = WhatToShow(this);

		coefficient = showCoefficient ? (showSign ? $"{coefficient}*" : coefficient) : string.Empty;
		power = showPower ? (showSign ? $"^{power}" : power) : string.Empty;

		partialValues.Add(($"{coefficient}{(showSign ? Sign() : string.Empty)}{power} = {coefficient}{(showSign ? VariableValue() : string.Empty)}{power} = {GetValue()}",
			root.ToStringByStep(ref stepCopy)));
	}

	public override string ToStringByStep(ref int step)
	{
		string coefficient = Coefficient.ToStringByStep(ref step);
		string power = Power.ToStringByStep(ref step);

		(bool showCoefficient, bool showSign, bool showPower) = WhatToShow(this);

		coefficient = showCoefficient ? (showSign ? $"{coefficient}*" : coefficient) : string.Empty;
		power = showPower ? (showSign ? $"^{power}" : power) : string.Empty;

		return --step <= 0 ? $"{coefficient}{(showSign ? Sign() : string.Empty)}{power}" : ParenthesizeIfSigned(GetValue());
	}

	#endregion
}

public sealed class X<T> : Variable<T>
where T :
	IEqualityOperators<T, T, bool>,
	IMultiplyOperators<T, T, T>,
	IPowerOperations<T, T, T>,
	IAdditiveIdentity<T, T>,
	IMultiplicativeIdentity<T, T>
{
	#region Fields

	public static T Value = T.AdditiveIdentity;

	#endregion

	#region Constructors

	public X() : base() { }

	public X(ValueHolder<T>? coefficient, ValueHolder<T>? power) : base(coefficient, power) { }

	#endregion

	#region Protected methods

	protected override Variable<T> Create(ValueHolder<T>? coefficient, ValueHolder<T>? power) => new X<T>(coefficient, power);

	protected override T VariableValue() => Value;

	#endregion

	#region Public methods

	public override string Sign() => "x";

	#endregion
}