using NUnit.Framework;
using NUnit;
using Calc;
using NUnit.Framework.Legacy;

namespace Calc.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _calculator;

        [SetUp]
        public void Setup()
        {
            // This runs before each test method
            _calculator = new Calculator();
        }

        [Test]
        public void Add_TwoPositiveNumbers_ReturnsCorrectSum()
        {
            // Arrange
            int a = 5;
            int b = 3;
            int expected = 8;

            // Act
            int result = _calculator.Add(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Add_NegativeNumbers_ReturnsCorrectSum()
        {
            // Arrange
            int a = -5;
            int b = -3;
            int expected = -8;

            // Act
            int result = _calculator.Add(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Subtract_ValidNumbers_ReturnsCorrectDifference()
        {
            // Arrange
            int a = 10;
            int b = 4;
            int expected = 6;

            // Act
            int result = _calculator.Subtract(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Multiply_TwoNumbers_ReturnsCorrectProduct()
        {
            // Arrange
            int a = 4;
            int b = 5;
            int expected = 20;

            // Act
            int result = _calculator.Multiply(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Divide_ValidNumbers_ReturnsCorrectQuotient()
        {
            // Arrange
            int a = 10;
            int b = 2;
            double expected = 5.0;

            // Act
            double result = _calculator.Divide(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(expected).Within(0.001)); // Third parameter is delta for floating point comparison
        }

        [Test]
        public void Divide_ByZero_ThrowsArgumentException()
        {
            // Arrange
            int a = 10;
            int b = 0;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _calculator.Divide(a, b));
        }

        [Test]
        public void Subtract_ValidNumbers_ReturnsInCorrectDifference()
        {
            // Arrange
            int a = 10;
            int b = 4;
            int expected = 2;

            // Act
            int result = _calculator.Subtract(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(expected)); // expecting error lol
        }

        [TearDown]
        public void TearDown()
        {
            // This runs after each test method
            // Clean up resources if needed
            _calculator = null;
        }
    }
}