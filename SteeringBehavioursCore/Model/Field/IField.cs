using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SteeringBehavioursCore.Model.Interaction;

namespace SteeringBehavioursCore.Model.Field
{
    public interface IField
    {
        Boid[] Boids { get; }
        IFieldInteraction Interaction { get; }
        void SetFieldSize(float width, float height);
        void Advance(float stepSize = 1);
    }
}
