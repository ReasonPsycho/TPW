using System.Numerics;
using NUnit.Framework;
using Logic;

namespace LogicTest
{
    [TestFixture]
    public class BallTests
    {
        [Test]
        public void ChangePosition_MovesBallInVelocityDirection()
        {
            // Arrange
            var ball = new Ball(new Vector2(0, 0), 10)
            {
                Velocity = new Vector2(1, 1)
            };

            // Act
            ball.ChangePosition();

            // Assert
            Assert.AreEqual(new Vector2(1500, 1500), ball.Position);
        }
    }
}