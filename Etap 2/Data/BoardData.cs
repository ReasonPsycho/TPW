namespace Data;

public class BoardData
{
    public int Width { get; set; }
    public int Height { get; set; }

    public BoardData(int width, int height)
    {
        Width = width;
        Height = height;
    }
}