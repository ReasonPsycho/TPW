using System;
using NUnit.Framework;
using TPW;

namespace UnitTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Add()
        {
            Assert.True(Calculator.Add(6, 2) == 6 + 2);
        }
        
        [Test]
        public void Divide()
        {
            Assert.True(Calculator.Divide(2, 2) == 2 / 2);
        }
        
        [Test]
        public void Subtract()
        {
            Assert.True(Calculator.Subtract(6, 2) == 6 - 2);
        }
        
        [Test]
        public void Multiply()
        {
            Assert.True(Calculator.Multiply(6, 2) == 6 * 2);
        }
    }
}