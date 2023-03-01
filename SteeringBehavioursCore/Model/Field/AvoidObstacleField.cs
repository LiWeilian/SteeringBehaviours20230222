using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SteeringBehavioursCore.Model;
using SteeringBehavioursCore.Model.Behaviour;
using SteeringBehavioursCore.Model.Boid;
using SteeringBehavioursCore.Model.Interaction;

namespace SteeringBehavioursCore.Model.Field
{
    public class AvoidObstacleField : BaseField
    {
        public List<Obstacle> Obstacles { get; }
        private const int _boidsCount = 50;
        private const int _obstacleCount = 5;
        public AvoidObstacleField()
        {
            _width = Width;
            _height = Height;

            Interaction = new BaseInteraction(this);

            Obstacles = new List<Obstacle>();
            GenerateObstacles();

            Boids = new NormalBoid[_boidsCount];
            GenerateRandomBoids();
        }

        private void GenerateObstacles()
        {
            Random rnd = new Random();
            for (int i = 0; i < _obstacleCount; i++)
            {
                Obstacles.Add(new Obstacle());
            }
        }

        private void GenerateRandomBoids()
        {
            var behaviours = new List<Behaviour.Behaviour>
            {
                new FlockBehaviour(this),
                new AlignBehaviour(this),
                new AvoidBoidsBehaviour(this),
                new AvoidObstacleBehaviour(this),
                new AvoidWallsBehaviour(this, _width, _height)
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

    public class Obstacle
    {
        public List<Position> Points { get; } = new List<Position>();
        public Position Center
        {
            get
            {
                return new Position((Points[0].X + Points[2].X) / 2f, (Points[0].Y + Points[2].Y) / 2);
            }
        }

        public Obstacle()
        {
            Random rnd = new Random((int)DateTime.Now.Ticks);
            Thread.Sleep(10);

            float x = (float)(100 + 800 * rnd.NextDouble());
            float y = (float)(100 + 200 * rnd.NextDouble());

            float width = (float)(50 + 100 * rnd.NextDouble());
            float height = (float)(50 + 100 * rnd.NextDouble());

            Points.Add(new Position(x, y));
            Points.Add(new Position(x, y + height));
            Points.Add(new Position(x + width, y + height));
            Points.Add(new Position(x + width, y));
        }
    }
}
