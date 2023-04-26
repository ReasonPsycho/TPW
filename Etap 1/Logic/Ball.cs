using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace Logic
{
   public class Ball : INotifyPropertyChanged
{
    public Vector2 Position { get; set; }
    public int Radius { get; set; }
    public Vector2 Velocity { get; set; }
    private const int _speed = 1500;

    public Ball() { }

    public Ball(Vector2 ballPosition, int radius) =>
        (Position, Radius) = (ballPosition, radius);

    public float X => Position.X;
    public float Y => Position.Y;

    public event PropertyChangedEventHandler PropertyChanged;

    public void ChangePosition()
    {
        Position += new Vector2(Velocity.X * _speed, Velocity.Y * _speed);
        if (Position.X < 0 || Position.X > LogicApi.BoardWidth - 25)
            Velocity *= -Vector2.UnitX;

        if (Position.Y < 0 || Position.Y > LogicApi.BoardHeight - 25)
            Velocity *= -Vector2.UnitY;

        RaisePropertyChanged(nameof(X));
        RaisePropertyChanged(nameof(Y));
    }

    protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

   
}