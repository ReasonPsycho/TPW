using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Logic
{
    internal class LogicApi : LogicAbstractAPI
{
    private readonly DataAbstractAPI dataAPI;

    public static int BoardWidth { get; } = 500;
    public static int BoardHeight { get; } = 500;
    public override ObservableCollection<Ball> Balls { get; } = new ObservableCollection<Ball>();

    private CancellationToken cancelToken;
    private List<Task> tasks = new List<Task>();

    public LogicApi()
    {
        dataAPI = DataAbstractAPI.CreateDataAPI();
    }

    public override void RunSimulation()
    {
        cancelToken = CancellationToken.None;

        foreach (Ball ball in Balls)
        {
            Task task = Task.Run(() =>
            {
                while (true)
                {
                    Thread.Sleep(5);

                    try
                    {
                        cancelToken.ThrowIfCancellationRequested();
                    }
                    catch (OperationCanceledException)
                    {
                        break;
                    }

                    ball.ChangePosition();
                }
            });

            tasks.Add(task);
        }
    }

    public override void StopSimulation()
    {
        cancelToken = new CancellationToken(true);

        foreach (Task task in tasks)
        {
            task.Wait();
        }

        tasks.Clear();
        Balls.Clear();
    }

    public override Ball CreateBall(Vector2 pos, int radius)
    {
        return new Ball(pos, radius);
    }

    public override void CreateBalls(int count, int radius)
    {
        if (count < 0)
        {
            count = Math.Abs(count);
        }

        Random random = new Random();

        for (int i = 0; i < count; i++)
        {
            Ball ball = new Ball();
            ball.Velocity = new Vector2((float)0.0034, (float)0.0034);
            ball.Position = new Vector2(random.Next(1, BoardWidth - 25), random.Next(1, BoardHeight - 25));

            Balls.Add(ball);
        }
    }

    public override void DeleteBalls()
    {
        Balls.Clear();
    }

    public override int Width => BoardWidth;

    public override int Height => BoardHeight;

    public CancellationToken CancellationToken => cancelToken;
}

}
