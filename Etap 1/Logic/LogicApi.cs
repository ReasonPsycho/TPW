 using System.Collections.ObjectModel;
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
            BallData ballData = _dataAPI.GetBallData(pos, new Vector2((float)0.0034, (float)0.0034), radius);
            BallLogic ballLogic = new BallLogic(ballData);
            Balls.Add(ballLogic);

            return ballLogic;
        }

        public override void CreateBalls(int count, int radius)
        {
            Random random = new Random();

            for (int i = 0; i < count; i++)
            {
                Vector2 pos = new Vector2(random.Next(0, Board.Width - radius), random.Next(0, Board.Height - radius));
                BallData ballData = _dataAPI.GetBallData(pos, new Vector2((float)0.0034, (float)0.0034), radius);
                BallLogic ballLogic = new BallLogic(ballData);
                Balls.Add(ballLogic);
            }
        }

        public override void DeleteBalls()
        {
            Balls.Clear();
        }
    }
 }