using System;
using Xunit;

namespace testFib
{
    public class UnitTest1
    {
        // test Fibonacci function
        // 1 1 2 3 5 8 13 21
        [Fact]
        public void Testfib1()
        {
            var expected = 1;
            var actual = fib.Calc(1);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Testfib2()
        {
            var expected = 1;
            var actual = fib.Calc(2);
            Assert.Equal(expected, actual);
        }

    }
}

