using Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateApi()
        {
            return new LogicApi();
        }

        public abstract int Width { get; }
        public abstract int Height { get; }
        public abstract ObservableCollection<Ball> Balls { get; }
        public abstract void RunSimulation();
        public abstract void StopSimulation();
        public abstract Ball CreateBall(Vector2 pos, int radius);
        public abstract void CreateBalls(int count, int radius);
        public abstract void DeleteBalls();
    }
}
