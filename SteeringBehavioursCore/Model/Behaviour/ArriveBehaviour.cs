using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringBehavioursCore.Model.Behaviour
{
    public class ArriveBehaviour : Behaviour
    {
        private const float Weight = 0.005f;
        public Position ArrivePoint { get; set; }
        public ArriveBehaviour(Boid[] boids) : base(boids)
        {

        }

        public override void CalcVelocity(Boid curBoid)
        {
            if (ArrivePoint == null)
            {
                return;
            }
            foreach (var boid in Boids)
            {
                curBoid.Velocity += (ArrivePoint - curBoid.Position) * Weight;
            }
        }
    }
}
