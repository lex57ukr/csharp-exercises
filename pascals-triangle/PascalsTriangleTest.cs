// This file was auto-generated based on version 1.0.0 of the canonical data.

using Xunit;
using System;


public class PascalsTriangleTest
{
    [Fact]
    public void Zero_rows()
    {
        Assert.Empty(PascalsTriangle.Calculate(0));
    }

    [Fact]
    public void Single_row()
    {
        var expected = new[] { new[] { 1 } };
        Assert.Equal(expected, PascalsTriangle.Calculate(1));
    }

    [Fact]
    public void Two_rows()
    {
        var expected = new[] { new[] { 1 }, new[] { 1, 1 } };
        Assert.Equal(expected, PascalsTriangle.Calculate(2));
    }

    [Fact]
    public void Three_rows()
    {
        var expected = new[] { new[] { 1 }, new[] { 1, 1 }, new[] { 1, 2, 1 } };
        Assert.Equal(expected, PascalsTriangle.Calculate(3));
    }

    [Fact]
    public void Four_rows()
    {
        var expected = new[] { new[] { 1 }, new[] { 1, 1 }, new[] { 1, 2, 1 }, new[] { 1, 3, 3, 1 } };
        Assert.Equal(expected, PascalsTriangle.Calculate(4));
    }

    [Fact]
    public void Negative_rows()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => PascalsTriangle.Calculate(-1));
    }
}
