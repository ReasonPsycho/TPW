﻿using System.Numerics;

namespace Data
{
    public class BallData
    {
        public Vector2 Position { get; set; }
        public int Radius { get; set; }
        public Vector2 Velocity { get; set; }
        public int Speed { get; set; } = 1500;

        public BallData(Vector2 position, Vector2 velocity, int radius)
        {
            Position = position;
            Velocity = velocity;
            Radius = radius;
        }
    }
}