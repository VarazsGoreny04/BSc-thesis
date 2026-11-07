using Calculators;
using ProjectReal.Number;
using System;

namespace CalculatorsTest;

[TestClass]
public class DivideTest
{
	[TestMethod]
	public void EmptyConstructor()
	{
		{
			Divide<Rational> div = new();

			Assert.IsNull(div.Left);
			Assert.IsNull(div.Right);

			Assert.ThrowsException<NullReferenceException>(div.GetValue);
		}

		{
			Divide<Rational> div1 = new()
			{
				Left = new Number<Rational>("1")
			};

			Assert.ThrowsException<NullReferenceException>(div1.GetValue);
		}

		{
			Divide<Rational> div2 = new()
			{
				Right = new Number<Rational>("1")
			};

			Assert.ThrowsException<NullReferenceException>(div2.GetValue);
		}

		{
			Divide<Rational> div3 = new()
			{
				Left = new Number<Rational>("1"),
				Right = new Number<Rational>("1")
			};

			div3.GetValue();
		}
	}

	[TestMethod]
	public void LeftAndRightConstructor()
	{
		Rational rational1, rational2;
		Number<Rational> number1, number2;
		Divide<Rational> div;

		foreach (NumberTestCase item in NumberTestCases.List)
		{
			rational1 = item.Number1;
			rational2 = item.Number2;

			number1 = new Number<Rational>(rational1);
			number2 = new Number<Rational>(rational2);

			div = new Divide<Rational>(number1, number2);

			Assert.AreEqual(rational1, div.Left.GetValue());
			Assert.AreEqual(rational2, div.Right.GetValue());
		}
	}

	[TestMethod]
	public void GetValueMethod()
	{
		Rational rational1, rational2, expected;
		Number<Rational> number1, number2;
		Divide<Rational> div;

		foreach (NumberTestCase item in NumberTestCases.List)
		{
			if (item.Div is "ERROR")
				continue;

			rational1 = item.Number1;
			rational2 = item.Number2;

			number1 = new Number<Rational>(rational1);
			number2 = new Number<Rational>(rational2);

			div = new Divide<Rational>(number1, number2);

			expected = item.Div;

			Assert.AreEqual(expected, div.GetValue());
		}
	}
}