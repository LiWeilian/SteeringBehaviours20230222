using SteeringBehavioursCore.Model.Field;
using SteeringBehavioursCore.Model.Boid;

namespace SteeringBehavioursCore.Model.Behaviour
{
    internal class AvoidWallsBehaviour : Behaviour
    {
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

            if (curBoid.Position.X < Pad) resultVelocity.X += Turn;
            if (curBoid.Position.Y < Pad) resultVelocity.Y += Turn;
            if (curBoid.Position.X > _width - Pad)
                resultVelocity.X -= Turn;
            if (curBoid.Position.Y > _height - Pad)
                resultVelocity.Y -= Turn;

            curBoid.Velocity += resultVelocity * Weight;
        }
    }
}
