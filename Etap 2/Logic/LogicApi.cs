﻿ using System.Collections.ObjectModel;
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

    foreach (BallLogic ball in Balls)
    {
        Task task = Task.Run(() =>
        {
            while (true)
            {
                Thread.Sleep(5);

                try
                {
                    _cancelToken.ThrowIfCancellationRequested();
                }
                catch (OperationCanceledException)
                {
                    break;
                }

                HashSet<BallLogic[]> alreadyCollidedPairs = new HashSet<BallLogic[]>();


                // Check for collisions with other balls
                lock (Balls) // Acquire lock before accessing Balls
                {
                    foreach (BallLogic otherBall in Balls)
                    {
                        if (ball == otherBall) continue;
                        if (alreadyCollidedPairs.Contains(new[] { ball, otherBall })) continue;
                        
                        if (ball.CollidesWith(otherBall))
                        {
                            // Handle collision, e.g. change velocity or position of both balls
                            ball.HandleCollision(otherBall);
                            alreadyCollidedPairs.Add(new[] { ball, otherBall });
                        }
                    }
                }
                ball.ChangePosition();
            }
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
            BallData ballData = _dataAPI.GetBallData(pos, new Vector2((float)0.0034, (float)0.0034), radius,radius/2);
            BallLogic ballLogic = new BallLogic(ballData);
            Balls.Add(ballLogic);

            return ballLogic;
        }

        public override void CreateBalls(int count)
        {
            var rnd = new Random((int)DateTime.Now.Ticks);

            for (int i = 0; i < count; i++)
            {
               
                float radius = GenerateRandomFloatInRange(rnd,10f, 50f);
                Vector2 pos =  GenerateRandomVector2InRange(rnd, 0, Board.Width - radius, 0, Board.Height - radius);
                Vector2 vel = GenerateRandomVector2InRange(rnd, -0.0025f, 0.0025f, -0.0025f, 0.0025f);
                BallData ballData = _dataAPI.GetBallData(pos,vel , radius,radius/2);
                BallLogic ballLogic = new BallLogic(ballData);
                Balls.Add(ballLogic);
            }
        }

        public override void DeleteBalls()
        {
            Balls.Clear();
        }
        
        public static float GenerateRandomFloatInRange(Random random,float minValue, float maxValue)
        {
            return (float)(random.NextDouble() * (maxValue - minValue) + minValue);
        }

        public static Vector2 GenerateRandomVector2InRange(Random random,float minValue1, float maxValue1,float minValue2 , float maxValue2)
        {
            return (Vector2)( new Vector2(GenerateRandomFloatInRange(random,minValue1,maxValue1),GenerateRandomFloatInRange(random,minValue2,maxValue2)));
        }
    }
 }