using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SteeringBehavioursCore.Model.Behaviour;
using SteeringBehavioursCore.Model.Interaction;
using SteeringBehavioursCore.Model.Field;
using SteeringBehavioursCore.Model.Boid;

namespace SteeringBehavioursCore.Model.Field
{
    public class ArriveField : BaseField
    {
        public override IBoid[] Boids { get; protected set; }
        public override IFieldInteraction Interaction { get; protected set; }

        private float _width, _height;

        public ArriveField(int boids_count)
        {
            _width = Width;
            _height = Height;

            Interaction = new ArriveInteraction(this);

            Boids = new NormalBoid[boids_count];

            GenerateRandomBoids();
        }

        public override void Advance(float stepSize = 1)
        {
            Parallel.ForEach(Boids, boid => boid.Move(stepSize));
        }

        public override void SetFieldSize(float width, float height)
        {
            if (width <= 0 || height <= 0)
                throw new Exception(
                    "Wrong size of field");
            (_width, _height) = (width, height);
        }

        private void GenerateRandomBoids()
        {
            var behaviours = new List<Behaviour.Behaviour>
            {
                new FlockBehaviour(this),
                new AlignBehaviour(this),
                new AvoidBoidsBehaviour(this),
                new AvoidWallsBehaviour(this, _width, _height),
                new ArriveBehaviour(this)
            };

            var rnd = new Random();
            for (var i = 0; i < Boids.GetLength(0); i++)
            {
                Boids[i] = new NormalBoid(
                    (float)rnd.NextDouble() * _width,
                    (float)rnd.NextDouble() * _height,
                    (float)(rnd.NextDouble() - .5),
                    (float)(rnd.NextDouble() - .5),
                    (float)(1 + rnd.NextDouble()));

                behaviours.ForEach(
                    behaviour => Boids[i].AddBehaviour(behaviour));
            }
        }
    }
}
