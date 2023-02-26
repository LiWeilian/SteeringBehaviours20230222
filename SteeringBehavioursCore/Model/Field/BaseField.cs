using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringBehavioursCore.Model.Boid;
using SteeringBehavioursCore.Model.Interaction;

namespace SteeringBehavioursCore.Model.Field
{
    public abstract class BaseField : IField
    {
        public static float Width { get { return 1200f; } }
        public static float Height { get { return 600f; } }
        public virtual IBoid[] Boids { get; protected set; }

        public virtual IFieldInteraction Interaction { get; protected set; }

        public virtual void Advance(float stepSize = 1)
        {
            throw new NotImplementedException();
        }

        public virtual void SetFieldSize(float width, float height)
        {
            throw new NotImplementedException();
        }
    }
}
