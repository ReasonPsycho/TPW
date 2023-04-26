
namespace Data
{
    public abstract class DataAbstractAPI
    {
        public static DataAbstractAPI CreateDataAPI()
        {
            return new DataLayer();
        }

    }
    internal class DataLayer : DataAbstractAPI
    {
        public DataLayer() { }
    }
}