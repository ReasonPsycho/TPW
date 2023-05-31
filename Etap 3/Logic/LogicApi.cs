﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Numerics;
using Data;

namespace Logic
{
    internal class LogicApi : LogicAbstractAPI
    {
        private readonly DataAbstractAPI _dataAPI;

        // Pass the BoardData instance to BallLogic using BallLogic.SetBoardData
        public override BoardData Board { get; }
        public override ObservableCollection<BallLogic> Balls { get; } = new ObservableCollection<BallLogic>();

        private CancellationToken _cancelToken;
        private List<Task> _tasks = new List<Task>();

        public LogicApi()
        {
            _dataAPI = DataAbstractAPI.CreateDataAPI();
            Board = _dataAPI.GetBoardData(500, 500);

            // Pass the BoardData instance to BallLogic using BallLogic.SetBoardData
            BallLogic.SetBoardData(Board);
        }

 public override void RunSimulation()
{
    _cancelToken = CancellationToken.None;

    // Add Barrier object and set initial count to number of balls
    Barrier barrier = new Barrier(Balls.Count);

    foreach (BallLogic ball in Balls)
    {
        Task task = Task.Run(() =>
        {
            // Wait for all balls to start updating before continuing
            barrier.SignalAndWait();

            var timer = new System.Timers.Timer();
            timer.Elapsed += (_, elapsedArgs) =>
            {
                ball.ChangePosition();
                foreach (BallLogic otherBall in Balls)
                {
                    if (ball == otherBall) continue;
                    if (ball.CollidesWith(otherBall))
                    {
                        ball.HandleCollision(otherBall);
                    }
                }
            };
            timer.Interval = 8.8888; // 120 fps
            timer.Enabled = true;

            while (true)
            {
                try
                {
                    _cancelToken.ThrowIfCancellationRequested();
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }

            timer.Stop();
            timer.Dispose();
        });

        _tasks.Add(task);
    }
}





        public override void StopSimulation()
        {
            _cancelToken = new CancellationToken(true);

            foreach (Task task in _tasks)
            {
                task.Wait();
            }

            _tasks.Clear();
            Balls.Clear();
        }

        public override BallLogic CreateBall(Vector2 pos, int radius)
        {
            BallData ballData =
                _dataAPI.GetBallData(pos, new Vector2((float)0.0034, (float)0.0034), radius, radius / 2);
            BallLogic ballLogic = new BallLogic(ballData);
            Balls.Add(ballLogic);

            return ballLogic;
        }

        public override void CreateBalls(int count)
        {
            var rnd = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < count; i++)
            {
                float speed = 0.0005f;
                float radius = GenerateRandomFloatInRange(rnd, 10f, 30f);
                Vector2 pos = GenerateRandomVector2InRange(rnd, 0, Board.Width - radius, 0, Board.Height - radius);
                Vector2 vel = GenerateRandomVector2InRange(rnd, -speed, speed, -speed, speed);
                BallData ballData = _dataAPI.GetBallData(pos, vel, radius, radius / 2);
                BallLogic ballLogic = new BallLogic(ballData);
                Balls.Add(ballLogic);
            }
        }

        public override void DeleteBalls()
        {
            Balls.Clear();
        }

        public static float GenerateRandomFloatInRange(Random random, float minValue, float maxValue)
        {
            return (float)(random.NextDouble() * (maxValue - minValue) + minValue);
        }

        public static Vector2 GenerateRandomVector2InRange(Random random, float minValue1, float maxValue1,
            float minValue2, float maxValue2)
        {
            return (Vector2)(new Vector2(GenerateRandomFloatInRange(random, minValue1, maxValue1),
                GenerateRandomFloatInRange(random, minValue2, maxValue2)));
        }
    }
}