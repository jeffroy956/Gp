using GpCore.Model;
using System;
using Xunit;

namespace Tests
{
    public class Tests
    {
        [Fact]
        public void Test1() 
        {
            Assert.True(true);
        }

        [Fact]
        public void Test2()
        {
            Assert.True(true);
        }

        [Fact]
        public void Test3()
        {
            Assert.True(true);
        }

        [Fact]
        public void Test4()
        {
            Class1 c = new Class1();

            Assert.Equal("hi", c.Hi);
        }
    }
}
