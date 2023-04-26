using System.Collections;
using System.Numerics;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Threading;
using Logic;

namespace LogicTests
{
    [TestFixture]
    public class LogicTest
    {
        private LogicAbstractAPI logic;

        [SetUp]
        public void SetUp()
        {
            logic = LogicAbstractAPI.CreateApi();
        }

        [TearDown]
        public void TearDown()
        {
            logic.StopSimulation();
        }

        [Test]
        public void CreateBall_ShouldCreateBallWithExpectedPositionAndRadius()
        {
            Vector2 expectedPosition = new Vector2(50, 50);
            int expectedRadius = 10;

            Ball ball = logic.CreateBall(expectedPosition, expectedRadius);

            Assert.AreEqual(expectedPosition, ball.Position);
            Assert.AreEqual(expectedRadius, ball.Radius);
        }

        [Test]
        public void CreateBalls_ShouldCreateSpecifiedNumberOfBalls()
        {
            int expectedCount = 5;

            logic.CreateBalls(expectedCount, 10);

            Assert.AreEqual(expectedCount, logic.Balls.Count);
        }

        [Test]
        public async Task RunSimulation_ShouldMoveBallsWhileRunning()
        {
            // Arrange
            int expectedXPosition = 100;
            Ball ball = logic.CreateBall(new Vector2(expectedXPosition, 0), 10);
            logic.Balls.Add(ball);

            // Act
            logic.RunSimulation();

            // Assert
            await Task.Delay(1000); // Wait for ball to move
            Assert.Less(ball.Position.Y, expectedXPosition); // Ball should have moved downwards
        }

        [Test]
        public void StopSimulation_ShouldStopMovingBalls()
        {
            // Arrange
            Ball ball = logic.CreateBall(new Vector2(0, 0), 10);
            logic.Balls.Add(ball);

            // Act
            logic.RunSimulation();
            logic.StopSimulation();

            // Assert
            Vector2 initialPosition = ball.Position;
            Thread.Sleep(100); // Wait for a short time to see if ball has moved
            Assert.AreEqual(initialPosition, ball.Position); // Ball should not have moved after stopping simulation
        }

        [Test]
        public void DeleteBalls_ShouldRemoveAllBalls()
        {
            // Arrange
            logic.Balls.Add(logic.CreateBall(new Vector2(0, 0), 10));
            logic.Balls.Add(logic.CreateBall(new Vector2(10, 10), 10));

            // Act
            logic.DeleteBalls();

            // Assert
            Assert.IsEmpty((IEnumerable)logic.Balls);
        }
    }
}
