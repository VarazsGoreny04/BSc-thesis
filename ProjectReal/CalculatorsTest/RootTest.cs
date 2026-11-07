using Calculators;
using ProjectReal.Number;
using ProjectRealTest;
using System;

namespace CalculatorsTest;

[TestClass]
public class RootTest
{
	[TestMethod]
	public void EmptyConstructor()
	{
		{
			Root<Rational> root = new();

			Assert.IsNull(root.Left);
			Assert.IsNull(root.Right);

			Assert.ThrowsException<NullReferenceException>(root.GetValue);
		}

		{
			Root<Rational> root1 = new()
			{
				Left = new Number<Rational>("1")
			};

			Assert.ThrowsException<NullReferenceException>(root1.GetValue);
		}

		{
			Root<Rational> root2 = new()
			{
				Right = new Number<Rational>("1")
			};

			Assert.ThrowsException<NullReferenceException>(root2.GetValue);
		}

		{
			Root<Rational> root3 = new()
			{
				Left = new Number<Rational>("1"),
				Right = new Number<Rational>("1")
			};

			root3.GetValue();
		}
	}

	[TestMethod]
	public void LeftAndRightConstructor()
	{
		Rational rational1, rational2;
		Number<Rational> number1, number2;
		Root<Rational> root;

		foreach (RationalTestCase item in RationalTestCases.List)
		{
			rational1 = item.Number1;
			rational2 = item.Number2;

			number1 = new Number<Rational>(rational1);
			number2 = new Number<Rational>(rational2);

			root = new Root<Rational>(number1, number2);

			Assert.AreEqual(rational1, root.Left.GetValue());
			Assert.AreEqual(rational2, root.Right.GetValue());
		}
	}

	[TestMethod]
	public void GetValueMethod()
	{
		Rational rational1, rational2, expected;
		Number<Rational> number1, number2;
		Root<Rational> root;
		int length;

		foreach (RationalTestCase item in RationalTestCases.List)
		{
			if (item.Root is "ERROR" or "BIG")
				continue;

			rational1 = item.Number1;
			rational2 = item.Number2;

			number1 = new Number<Rational>(rational1);
			number2 = new Number<Rational>(rational2);

			root = new Root<Rational>(number2, number1);

			expected = item.Root;

			length = Math.Min(item.Root.Length, root.GetValue().ToString().Length);

			Assert.AreEqual(expected.ToString()[..length], root.GetValue().ToString()[..length]);
		}
	}
}