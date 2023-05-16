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
    public float Radius => _ballData.Radius;
    public float Weight => _ballData.Weight;
    private static BoardData _boardData;

    public static void SetBoardData(BoardData boardData)
    {
        _boardData = boardData;
    }

    public bool CollidesWith(BallLogic other)
    {
        Vector2 distance = new Vector2(other.X,other.Y) - new Vector2(this.X,this.Y);
        float radiiSum = other.Radius/2 + this.Radius/2;

        return distance.LengthSquared() <= radiiSum * radiiSum;
    }
    
    public void HandleCollision(BallLogic other)
    {
        Vector2 collisionNormal = Vector2.Normalize(new Vector2(other.X,other.Y)  - new Vector2(this.X,this.Y));
        Vector2 relativeVelocity = other.Velocity - this.Velocity;
        float impulseMagnitude = 2 * this.Weight * other.Weight * Vector2.Dot(relativeVelocity, collisionNormal) / (this.Weight + other.Weight);
            
        other.Velocity -= impulseMagnitude / other.Weight * collisionNormal;
        this.Velocity += impulseMagnitude / this.Weight * collisionNormal;
    }
    
    public event PropertyChangedEventHandler PropertyChanged;

    public void ChangePosition()
    {
        _ballData.Position += new Vector2(_ballData.Velocity.X * _ballData.Speed, _ballData.Velocity.Y * _ballData.Speed);
        Vector2 normal = Vector2.Zero;

        if (_ballData.Position.X < 0)
            normal = Vector2.UnitX;
        else if (_ballData.Position.X > _boardData.Width - _ballData.Radius)
            normal = -Vector2.UnitX;
        else if (_ballData.Position.Y < 0)
            normal = Vector2.UnitY;
        else if (_ballData.Position.Y > _boardData.Height - _ballData.Radius)
            normal = -Vector2.UnitY;

        if (normal != Vector2.Zero)
            _ballData.Velocity = Vector2.Reflect(_ballData.Velocity, normal);

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