using Calculators;
using ProjectReal.Number;
using ProjectRealTest;
using System;

namespace CalculatorsTest;

[TestClass]
public class PowerTest
{
	[TestMethod]
	public void EmptyConstructor()
	{
		{
			Power<Rational> pow = new();

			Assert.IsNull(pow.Left);
			Assert.IsNull(pow.Right);

			Assert.ThrowsException<NullReferenceException>(pow.GetValue);
		}

		{
			Power<Rational> pow1 = new()
			{
				Left = new Number<Rational>("1")
			};

			Assert.ThrowsException<NullReferenceException>(pow1.GetValue);
		}

		{
			Power<Rational> pow2 = new()
			{
				Right = new Number<Rational>("1")
			};

			Assert.ThrowsException<NullReferenceException>(pow2.GetValue);
		}

		{
			Power<Rational> pow3 = new()
			{
				Left = new Number<Rational>("1"),
				Right = new Number<Rational>("1")
			};

			pow3.GetValue();
		}
	}

	[TestMethod]
	public void LeftAndRightConstructor()
	{
		Rational rational1, rational2;
		Number<Rational> number1, number2;
		Power<Rational> pow;

		foreach (RationalTestCase item in RationalTestCases.List)
		{
			rational1 = item.Number1;
			rational2 = item.Number2;

			number1 = new Number<Rational>(rational1);
			number2 = new Number<Rational>(rational2);

			pow = new Power<Rational>(number1, number2);

			Assert.AreEqual(rational1, pow.Left.GetValue());
			Assert.AreEqual(rational2, pow.Right.GetValue());
		}
	}

	[TestMethod]
	public void GetValueMethod()
	{
		Rational rational1, rational2, expected;
		Number<Rational> number1, number2;
		Power<Rational> pow;
		int length;

		foreach (RationalTestCase item in RationalTestCases.List)
		{
			if (item.Pow is "ERROR" or "BIG")
				continue;

			rational1 = item.Number1;
			rational2 = item.Number2;

			number1 = new Number<Rational>(rational1);
			number2 = new Number<Rational>(rational2);

			pow = new Power<Rational>(number1, number2);

			expected = item.Pow;

			length = Math.Min(item.Pow.Length, pow.GetValue().ToString().Length);

			Assert.AreEqual(expected.ToString()[..length], pow.GetValue().ToString()[..length]);
		}
	}
}