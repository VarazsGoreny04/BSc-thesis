using ProjectReal.NumberSet;
using System;
using System.Numerics;

namespace Calculators.Derivative;

public class Derivative<T> : Function<ValueHolder<T>>
where T :
	IEqualityOperators<T, T, bool>,
	IAdditionOperators<T, T, T>,
	ISubtractionOperators<T, T, T>,
	IMultiplyOperators<T, T, T>,
	IDivisionOperators<T, T, T>,
	IPowerOperations<T, T, T>,
	IRootOperations<T, T, T>,
	IAdditiveIdentity<T, T>,
	IMultiplicativeIdentity<T, T>
{
	public ValueHolder<ValueHolder<T>> Parameter
	{
		get => parameters[0];
		set => parameters[0] = value;
	}

	public Derivative() : base([null!]) { }
	public Derivative(ValueHolder<ValueHolder<T>> parameter) : base([parameter]) { }

	protected override ValueHolder<T> CalculateValue() => null;

	public override string Sign() => "D";

	public static FunctionBase<T> Simplify<V>(ValueHolder<T> v) where V : Variable<T>, new()
	{
		return v switch
		{
			V variable => variable,
			Term<T> t => Simplify<V>(t),
			Parenthesized<T> p => Simplify<V>(p),
			Add<T> a => Simplify<V>(a),
			Subtract<T> s => Simplify<V>(s),
			Multiply<T> m => Simplify<V>(m),
			Divide<T> d => Simplify<V>(d),
			Power<T> p => Simplify<V>(p),
			Root<T> r => Simplify<V>(r),
			_ => throw new NotImplementedException("This operation is not implemented!")
		};
	}
	public static FunctionBase<T> Simplify<V>(Term<T> v) where V : Variable<T>, new() => new V() { Coefficient = v };
	public static FunctionBase<T> Simplify<V>(Parenthesized<T> v) where V : Variable<T>, new()
	{
		ValueHolder<T> content = Simplify<V>(v.Content);

		return content is V x ? x : new Parenthesized<T>(content);
	}
	public static FunctionBase<T> Simplify<V>(Add<T> v) where V : Variable<T>, new()
	{
		Add<T> add = new(Simplify<V>(v.Left), Simplify<V>(v.Right));

		return add.Left is V left && add.Right is V right && left.Power is Number<T> lP && right.Power is Number<T> rP && lP.GetValue() == rP.GetValue() ?
			new V()
			{
				Coefficient = new Parenthesized<T>(new Add<T>(left.Coefficient, right.Coefficient)),
				Power = left.Power
			} :
			add;
	}
	public static FunctionBase<T> Simplify<V>(Subtract<T> v) where V : Variable<T>, new()
	{
		Subtract<T> subtract = new(Simplify<V>(v.Left), Simplify<V>(v.Right));

		return subtract.Left is V left && subtract.Right is V right && left.Power is Number<T> lP && right.Power is Number<T> rP && lP.GetValue() == rP.GetValue() ?
			new V()
			{
				Coefficient = new Parenthesized<T>(new Subtract<T>(left.Coefficient, right.Coefficient)),
				Power = left.Power
			} :
			subtract;
	}
	public static FunctionBase<T> Simplify<V>(Multiply<T> v) where V : Variable<T>, new()
	{
		Multiply<T> multiply = new(Simplify<V>(v.Left), Simplify<V>(v.Right));

		return multiply.Left is V left && multiply.Right is V right ?
			new V()
			{
				Coefficient = new Multiply<T>(left.Coefficient, right.Coefficient),
				Power = new Parenthesized<T>(new Add<T>(left.Power, right.Power))
			} :
			multiply;
	}
	public static FunctionBase<T> Simplify<V>(Divide<T> v) where V : Variable<T>, new()
	{
		Divide<T> divide = new(Simplify<V>(v.Left), Simplify<V>(v.Right));

		return divide.Left is V left && divide.Right is V right ?
			new V()
			{
				Coefficient = new Divide<T>(left.Coefficient, right.Coefficient),
				Power = new Parenthesized<T>(new Subtract<T>(left.Power, right.Power))
			} :
			divide;
	}
	public static FunctionBase<T> Simplify<V>(Power<T> v) where V : Variable<T>, new()
	{
		Power<T> power = new(Simplify<V>(v.Left), Simplify<V>(v.Right));

		return power.Left is V left && power.Right is V right && right.Power is Number<T> rP && rP.GetValue() == T.AdditiveIdentity ?
			new V()
			{
				Coefficient = new Power<T>(left.Coefficient, right.Coefficient),
				Power = new Parenthesized<T>(new Multiply<T>(left.Power, right.Coefficient))
			} :
			power;
	}
	public static FunctionBase<T> Simplify<V>(Root<T> v) where V : Variable<T>, new()
	{
		Power<T> root = new(Simplify<V>(v.Left), Simplify<V>(v.Right));

		return root.Left is V left && root.Right is V right && right.Power is Number<T> rP && rP.GetValue() == T.AdditiveIdentity ?
			new V()
			{
				Coefficient = new Root<T>(right.Coefficient, left.Coefficient),
				Power = new Parenthesized<T>(new Divide<T>(left.Power, right.Coefficient))
			} :
			root;
	}
}