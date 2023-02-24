using SteeringBehavioursCore.Model.Interaction;
using SteeringBehavioursCore.Model.Field;
using SteeringBehavioursCore.Model.Boid;

namespace SteeringBehavioursCore.Model.Behaviour
{
    public class ArriveBehaviour : Behaviour
    {
        private const float Weight = 0.005f;
        public ArriveBehaviour(IField field) : base(field)
        {

        }

        public override void CalcVelocity(IBoid curBoid)
        {
            if ((Field.Interaction as ArriveInteraction)?.ArrivePoint == null)
            {
                return;
            }
            foreach (var boid in Boids)
            {
                curBoid.Velocity += ((Field.Interaction as ArriveInteraction).ArrivePoint - curBoid.Position) * Weight;
            }
        }
    }
}
