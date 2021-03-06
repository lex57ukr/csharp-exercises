// This file was auto-generated based on version 1.0.0 of the canonical data.

using Xunit;

public class RationalNumbersTest
{
    [Fact]
    public void ToString_unequal_returns_ration_notation()
    {
        var actual = new RationalNumber(1, 2).ToString();
        Assert.Equal("1/2", actual);
    }

    [Fact]
    public void ToString_Equal_returns_single_notation()
    {
        var actual = new RationalNumber(1, 1).ToString();
        Assert.Equal("1", actual);
    }

    [Fact]
    public void Equals_equal_returns_true()
    {
        var actual = new RationalNumber(1, 2).Equals(
            new RationalNumber(1, 2)
        );

        Assert.True(actual);
    }

    [Fact]
    public void Equals_not_equal_returns_false()
    {
        var actual = new RationalNumber(1, 2).Equals(
            new RationalNumber(1, 3)
        );

        Assert.False(actual);
    }

    [Fact]
    public void GetHashCode_equal_return_same()
    {
        var x = new RationalNumber(1, 2).GetHashCode();
        var y = new RationalNumber(1, 2).GetHashCode();

        Assert.Equal(x, y);
    }

    [Fact]
    public void GetHashCode_not_equal_return_diff()
    {
        var x = new RationalNumber(2, 1).GetHashCode();
        var y = new RationalNumber(1, 2).GetHashCode();

        Assert.NotEqual(x, y);
    }

    [Fact]
    public void Add_two_positive_rational_numbers()
    {
        Assert.Equal(
            new RationalNumber(7, 6),
            new RationalNumber(1, 2) + new RationalNumber(2, 3)
        );
    }

    [Fact]
    public void Add_a_positive_rational_number_and_a_negative_rational_number()
    {
        Assert.Equal(
            new RationalNumber(-1, 6),
            new RationalNumber(1, 2) + new RationalNumber(-2, 3)
        );
    }

    [Fact]
    public void Add_two_negative_rational_numbers()
    {
        Assert.Equal(
            new RationalNumber(-7, 6),
            new RationalNumber(-1, 2) + new RationalNumber(-2, 3)
        );
    }

    [Fact]
    public void Add_a_rational_number_to_its_additive_inverse()
    {
        Assert.Equal(
            new RationalNumber(0, 1),
            new RationalNumber(1, 2) + new RationalNumber(-1, 2)
        );
    }

    [Fact]
    public void Subtract_two_positive_rational_numbers()
    {
        Assert.Equal(
            new RationalNumber(-1, 6),
            new RationalNumber(1, 2) - new RationalNumber(2, 3)
        );
    }

    [Fact]
    public void Subtract_a_positive_rational_number_and_a_negative_rational_number()
    {
        Assert.Equal(
            new RationalNumber(7, 6),
            new RationalNumber(1, 2) - new RationalNumber(-2, 3)
        );
    }

    [Fact]
    public void Subtract_two_negative_rational_numbers()
    {
        Assert.Equal(
            new RationalNumber(1, 6),
            new RationalNumber(-1, 2) - new RationalNumber(-2, 3)
        );
    }

    [Fact]
    public void Subtract_a_rational_number_from_itself()
    {
        Assert.Equal(
            new RationalNumber(0, 1),
            new RationalNumber(1, 2) - new RationalNumber(1, 2)
        );
    }

    [Fact]
    public void Multiply_two_positive_rational_numbers()
    {
        Assert.Equal(
            new RationalNumber(1, 3),
            new RationalNumber(1, 2) * new RationalNumber(2, 3)
        );
    }

    [Fact]
    public void Multiply_a_negative_rational_number_by_a_positive_rational_number()
    {
        Assert.Equal(
            new RationalNumber(-1, 3),
            new RationalNumber(-1, 2) * new RationalNumber(2, 3)
        );
    }

    [Fact]
    public void Multiply_two_negative_rational_numbers()
    {
        Assert.Equal(
            new RationalNumber(1, 3),
            new RationalNumber(-1, 2) * new RationalNumber(-2, 3)
        );
    }

    [Fact]
    public void Multiply_a_rational_number_by_its_reciprocal()
    {
        Assert.Equal(
            new RationalNumber(1, 1),
            new RationalNumber(1, 2) * new RationalNumber(2, 1)
        );
    }

    [Fact]
    public void Multiply_a_rational_number_by_1()
    {
        Assert.Equal(
            new RationalNumber(1, 2),
            new RationalNumber(1, 2) * new RationalNumber(1, 1)
        );
    }

    [Fact]
    public void Multiply_a_rational_number_by_0()
    {
        Assert.Equal(
            new RationalNumber(0, 1),
            new RationalNumber(1, 2) * new RationalNumber(0, 1)
        );
    }

    [Fact]
    public void Divide_two_positive_rational_numbers()
    {
        Assert.Equal(
            new RationalNumber(3, 4),
            new RationalNumber(1, 2) / new RationalNumber(2, 3)
        );
    }

    [Fact]
    public void Divide_a_positive_rational_number_by_a_negative_rational_number()
    {
        Assert.Equal(
            new RationalNumber(-3, 4),
            new RationalNumber(1, 2) / new RationalNumber(-2, 3)
        );
    }

    [Fact]
    public void Divide_two_negative_rational_numbers()
    {
        Assert.Equal(
            new RationalNumber(3, 4),
            new RationalNumber(-1, 2) / new RationalNumber(-2, 3)
        );
    }

    [Fact]
    public void Divide_a_rational_number_by_1()
    {
        Assert.Equal(
            new RationalNumber(1, 2),
            new RationalNumber(1, 2) / new RationalNumber(1, 1)
        );
    }

    [Fact]
    public void Absolute_value_of_a_positive_rational_number()
    {
        Assert.Equal(
            new RationalNumber(1, 2),
            new RationalNumber(1, 2).Abs()
        );
    }

    [Fact]
    public void Absolute_value_of_a_negative_rational_number()
    {
        Assert.Equal(
            new RationalNumber(1, 2),
            new RationalNumber(-1, 2).Abs()
        );
    }

    [Fact]
    public void Absolute_value_of_zero()
    {
        Assert.Equal(
            new RationalNumber(0, 1),
            new RationalNumber(0, 1).Abs()
        );
    }

    [Fact]
    public void Raise_a_positive_rational_number_to_a_positive_integer_power()
    {
        Assert.Equal(
            new RationalNumber(1, 8),
            new RationalNumber(1, 2).Exprational(3)
        );
    }

    [Fact]
    public void Raise_a_negative_rational_number_to_a_positive_integer_power()
    {
        Assert.Equal(
            new RationalNumber(-1, 8),
            new RationalNumber(-1, 2).Exprational(3)
        );
    }

    [Fact]
    public void Raise_zero_to_an_integer_power()
    {
        Assert.Equal(
            new RationalNumber(0, 1),
            new RationalNumber(0, 1).Exprational(5)
        );
    }

    [Fact]
    public void Raise_one_to_an_integer_power()
    {
        Assert.Equal(
            new RationalNumber(1, 1),
            new RationalNumber(1, 1).Exprational(4)
        );
    }

    [Fact]
    public void Raise_a_positive_rational_number_to_the_power_of_zero()
    {
        Assert.Equal(
            new RationalNumber(1, 1),
            new RationalNumber(1, 2).Exprational(0)
        );
    }

    [Fact]
    public void Raise_a_negative_rational_number_to_the_power_of_zero()
    {
        Assert.Equal(
            new RationalNumber(1, 1),
            new RationalNumber(-1, 2).Exprational(0)
        );
    }

    [Fact]
    public void Raise_a_real_number_to_a_positive_rational_number()
    {
        Assert.Equal(
            expected:  16,
            actual:    8.Expreal(new RationalNumber(4, 3)),
            precision: 0
        );
    }

    [Fact]
    public void Raise_a_real_number_to_a_negative_rational_number()
    {
        Assert.Equal(
            expected:  0.333333333333333,
            actual:    9.Expreal(new RationalNumber(-1, 2)),
            precision: 15
        );
    }

    [Fact]
    public void Raise_a_real_number_to_a_zero_rational_number()
    {
        Assert.Equal(
            expected:  1,
            actual:    2.Expreal(new RationalNumber(0, 1)),
            precision: 0
        );
    }

    [Fact]
    public void Reduce_a_positive_rational_number_to_lowest_terms()
    {
        Assert.Equal(
            new RationalNumber(1, 2),
            new RationalNumber(2, 4).Reduce()
        );
    }

    [Fact]
    public void Reduce_a_negative_rational_number_to_lowest_terms()
    {
        Assert.Equal(
            new RationalNumber(-2, 3),
            new RationalNumber(-4, 6).Reduce()
        );
    }

    [Fact]
    public void Reduce_a_rational_number_with_a_negative_denominator_to_lowest_terms()
    {
        Assert.Equal(
            new RationalNumber(-1, 3),
            new RationalNumber(3, -9).Reduce()
        );
    }

    [Fact]
    public void Reduce_zero_to_lowest_terms()
    {
        Assert.Equal(
            new RationalNumber(0, 1),
            new RationalNumber(0, 6).Reduce()
        );
    }

    [Fact]
    public void Reduce_an_integer_to_lowest_terms()
    {
        Assert.Equal(
            new RationalNumber(-2, 1),
            new RationalNumber(-14, 7).Reduce()
        );
    }

    [Fact]
    public void Reduce_one_to_lowest_terms()
    {
        Assert.Equal(
            new RationalNumber(1, 1),
            new RationalNumber(13, 13).Reduce()
        );
    }
}