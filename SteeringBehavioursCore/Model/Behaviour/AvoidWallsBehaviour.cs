using SteeringBehavioursCore.Model.Field;
using SteeringBehavioursCore.Model.Boid;
using System;

namespace SteeringBehavioursCore.Model.Behaviour
{
    internal class AvoidWallsBehaviour : Behaviour
    {
        public static float WallPad { get { return 50; } }
        private const float Pad = 50;
        private const float Turn = 0.3f;
        private const float Weight = 1.5f;
        private readonly float _height;
        private readonly float _width;

        public AvoidWallsBehaviour(IField field, float width, float height)
            : base(field)
        {
            _width = width;
            _height = height;
        }

        public override void CalcVelocity(IBoid curBoid)
        {
            var resultVelocity = new Velocity();

            //Arrow Position
            float x = (float)(curBoid.Position.X - curBoid.Size * Math.Sin(curBoid.Velocity.GetAngle() / 180 * Math.PI) * 2 * curBoid.Velocity.GetCurrentSpeed());
            float y = (float)(curBoid.Position.Y + curBoid.Size * Math.Cos(curBoid.Velocity.GetAngle() / 180 * Math.PI) * 2 * curBoid.Velocity.GetCurrentSpeed());

            if (x < Pad) resultVelocity.X += Turn;
            if (y < Pad) resultVelocity.Y += Turn;
            if (x > _width - Pad)
                resultVelocity.X -= Turn;
            if (y > _height - Pad)
                resultVelocity.Y -= Turn;

            //if (curBoid.Position.X < Pad) resultVelocity.X += Turn;
            //if (curBoid.Position.Y < Pad) resultVelocity.Y += Turn;
            //if (curBoid.Position.X > _width - Pad)
            //    resultVelocity.X -= Turn;
            //if (curBoid.Position.Y > _height - Pad)
            //    resultVelocity.Y -= Turn;

            curBoid.Velocity += resultVelocity * Weight;
        }
    }
}
