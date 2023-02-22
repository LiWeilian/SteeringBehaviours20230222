using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringBehavioursCore.Model.Behaviour;

namespace SteeringBehavioursCore.Model
{
    public class ArriveField : IField
    {
        public Boid[] Boids { get; private set; }
        public List<Behaviour.Behaviour> Behaviours { get; private set; }
        private float _width = 1200f, _height = 600f;

        public ArriveField(int boids_count)
        {
            Boids = new Boid[boids_count];

            GenerateRandomBoids();
        }

        public void Advance(float stepSize = 1)
        {
            Parallel.ForEach(Boids, boid => boid.Move(stepSize));
        }

        public void SetFieldSize(float width, float height)
        {
            if (width <= 0 || height <= 0)
                throw new Exception(
                    "Wrong size of field");
            (_width, _height) = (width, height);
        }

        private void GenerateRandomBoids()
        {
            Behaviours = new List<Behaviour.Behaviour>
            {
                new FlockBehaviour(Boids),
                new AlignBehaviour(Boids),
                new AvoidBoidsBehaviour(Boids),
                new ArriveBehaviour(Boids),
                new AvoidWallsBehaviour(Boids, _width, _height)
            };

            var rnd = new Random();
            for (var i = 0; i < Boids.GetLength(0); i++)
            {
                Boids[i] = new Boid(
                    (float)rnd.NextDouble() * _width,
                    (float)rnd.NextDouble() * _height,
                    (float)(rnd.NextDouble() - .5),
                    (float)(rnd.NextDouble() - .5),
                    (float)(1 + rnd.NextDouble()));

                Behaviours.ForEach(
                    behaviour => Boids[i].AddBehaviour(behaviour));
            }
        }
    }
}
