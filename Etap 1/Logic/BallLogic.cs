using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using Data;
using Logic;

public class BallLogic : INotifyPropertyChanged
{
    private readonly BallData _ballData;

    public Vector2 Velocity 
    { 
        get => _ballData.Velocity;
        set 
        {
            _ballData.Velocity = value;
            RaisePropertyChanged(nameof(Velocity));
        }
    }

    public BallLogic(BallData ballData)
    {
        _ballData = ballData;
    }

    public float X => _ballData.Position.X;
    public float Y => _ballData.Position.Y;
    public int Radius => _ballData.Radius;
    private static BoardData _boardData;

    public static void SetBoardData(BoardData boardData)
    {
        _boardData = boardData;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void ChangePosition()
    {
        _ballData.Position += new Vector2(_ballData.Velocity.X * _ballData.Speed, _ballData.Velocity.Y * _ballData.Speed);
        if (_ballData.Position.X < 0 || _ballData.Position.X > _boardData.Width - 25)
            _ballData.Velocity *= -Vector2.UnitX;

        if (_ballData.Position.Y < 0 || _ballData.Position.Y > _boardData.Height - 25)
            _ballData.Velocity *= -Vector2.UnitY;

        RaisePropertyChanged(nameof(X));
        RaisePropertyChanged(nameof(Y));
    }

    public void SetVelocity(Vector2 velocity)
    {
        Velocity = velocity;
    }

    protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}