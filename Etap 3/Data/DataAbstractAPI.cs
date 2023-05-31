using System.Numerics;

namespace Data
{
    public abstract class DataAbstractAPI
    {
        public static DataAbstractAPI CreateDataAPI()
        {
            return new DataLayer();
        }

        public virtual BallData GetBallData(Vector2 position, Vector2 velocity, float radius,float weight)
        {
            return new BallData(position, velocity, radius, weight);
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