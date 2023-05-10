using System.Numerics;

namespace Data
{
    public abstract class DataAbstractAPI
    {
        public static DataAbstractAPI CreateDataAPI()
        {
            return new DataLayer();
        }

        public virtual BallData GetBallData(Vector2 position, Vector2 velocity, int radius)
        {
            return new BallData(position, velocity, radius);
        }

        public virtual BoardData GetBoardData(int width, int height)
        {
            return new BoardData(width, height);
        }
    }

    internal class DataLayer : DataAbstractAPI
    {
        public DataLayer() { }
    }
}