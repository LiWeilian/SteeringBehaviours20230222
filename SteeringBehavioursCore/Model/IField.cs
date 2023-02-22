using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteeringBehavioursCore.Model
{
    public interface IField
    {
        Boid[] Boids { get; }
        void SetFieldSize(float width, float height);
        void Advance(float stepSize = 1);
    }
}
