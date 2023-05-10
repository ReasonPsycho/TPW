using NUnit.Framework;
using System.Numerics;
using Data;

namespace Tests
{
    public class DataTests
    {
        [Test]
        public void GetBallData_ReturnsCorrectObject()
        {
            // Arrange
            var dataAPI = DataAbstractAPI.CreateDataAPI();
            var position = new Vector2(10, 20);
            var velocity = new Vector2(0, 1);
            var radius = 5;

            // Act
            var ballData = dataAPI.GetBallData(position, velocity, radius);

            // Assert
            Assert.AreEqual(position, ballData.Position);
            Assert.AreEqual(velocity, ballData.Velocity);
            Assert.AreEqual(radius, ballData.Radius);
            Assert.AreEqual(1500, ballData.Speed); // default value
        }

        [Test]
        public void GetBoardData_ReturnsCorrectObject()
        {
            // Arrange
            var dataAPI = DataAbstractAPI.CreateDataAPI();
            var width = 800;
            var height = 600;

            // Act
            var boardData = dataAPI.GetBoardData(width, height);

            // Assert
            Assert.AreEqual(width, boardData.Width);
            Assert.AreEqual(height, boardData.Height);
        }
    }
}