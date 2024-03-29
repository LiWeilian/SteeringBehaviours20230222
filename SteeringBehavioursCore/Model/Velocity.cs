﻿using System;

namespace SteeringBehavioursCore.Model
{
    public class Velocity
    {
        public float X, Y;

        public Velocity(float x = 0, float y = 0)
        {
            (X, Y) = (x, y);
        }

        public float GetAngle()
        {
            if (X == 0 && Y == 0)
                return 0;
            var angle = (float)(Math.Atan(Y / X) * 180 / Math.PI - 90);
            if (X < 0)
                angle += 180;
            return angle;
        }

        public float GetCurrentSpeed()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }

        public void SetSpeed(float speed)
        {
            if (X == 0 && Y == 0)
                return;

            var currentSpeed = this.GetCurrentSpeed();

            var targetX = X / currentSpeed * speed;
            var targetY = Y / currentSpeed * speed;

            X = targetX;
            Y = targetY;
        }

        public static Velocity operator +(Velocity vel1, Velocity vel2)
        {
            return new Velocity(vel1.X + vel2.X, vel1.Y + vel2.Y);
        }

        public static Velocity operator -(Velocity vel1, Velocity vel2)
        {
            return new Velocity(vel1.X - vel2.X, vel1.Y - vel2.Y);
        }

        public static Velocity operator *(Velocity vel, float num)
        {
            return new Velocity(vel.X * num, vel.Y * num);
        }

        public static Velocity operator /(Velocity vel, float num)
        {
            return new Velocity(vel.X / num, vel.Y / num);
        }

        public static Velocity operator +(Velocity vel, Position pos)
        {
            return new Velocity(vel.X + pos.X, vel.Y + pos.Y);
        }

        public static Velocity operator -(Velocity vel, Position pos)
        {
            return new Velocity(vel.X - pos.X, vel.Y - pos.Y);
        }

        public static implicit operator Velocity(Position pos)
        {
            return new Velocity(pos.X, pos.Y);
        }
    }
}
