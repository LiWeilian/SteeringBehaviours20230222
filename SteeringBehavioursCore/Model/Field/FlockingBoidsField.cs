using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteeringBehavioursCore.Model.Behaviour;
using SteeringBehavioursCore.Model.Interaction;

namespace SteeringBehavioursCore.Model.Field
{
    public class FlockingBoidsField : IField
    {
        public Boid[] Boids { get; private set; }
        public IFieldInteraction Interaction { get; private set; } 

        private float _width = 1200f, _height = 600f;

        public FlockingBoidsField(int boidsCount, int enemyCount)
        {
            Interaction = new FlockingBoidsInteraction(this);

            if (enemyCount > boidsCount)
                throw new Exception(
                    "Number of enemies is bigger than number of boids");
            Boids = new Boid[boidsCount];
            GenerateRandomBoids(enemyCount);
        }

        public void SetFieldSize(float width, float height)
        {
            if (width <= 0 || height <= 0)
                throw new Exception(
                    "Wrong size of field");
            (_width, _height) = (width, height);
        }

        private void GenerateRandomBoids(int enemyCount)
        {
            var behaviours = new List<Behaviour.Behaviour>
            {
                new FlockBehaviour(this),
                new AlignBehaviour(this),
                new AvoidBoidsBehaviour(this),
                new AvoidEnemiesBehaviour(this),
                new AvoidWallsBehaviour(this, _width, _height)
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
                if (i < enemyCount)
                {
                    Boids[i].IsEnemy = true;
                    Boids[i].Speed -= .5f;
                }

                behaviours.ForEach(
                    behaviour => Boids[i].AddBehaviour(behaviour));
            }
        }

        public void Advance(float stepSize = 1)
        {
            Parallel.ForEach(Boids, boid => boid.Move(stepSize));
        }

        public void SetBoidsCount(int boids_count, int enemy_count)
        {
            if (boids_count < enemy_count)
            {
                return;
            }

            var rnd = new Random();
            List<Boid> boid_list = Boids.ToList();
            if (boid_list.Count < boids_count)
            {
                int add_count = boids_count - boid_list.Count;
                for (int i = 0; i < add_count; i++)
                {
                    Boid new_boid = new Boid(
                        (float)rnd.NextDouble() * _width,
                        (float)rnd.NextDouble() * _height,
                        (float)(rnd.NextDouble() - .5),
                        (float)(rnd.NextDouble() - .5),
                        (float)(1 + rnd.NextDouble()));
                    boid_list.Add(new_boid);
                }
            } else
            {
                while(boid_list.Count > boids_count)
                {
                    boid_list.RemoveAt(boid_list.Count - 1);
                }
            }

            for (int i = 0; i < boids_count; i++)
            {
                if (i < enemy_count && !boid_list[i].IsEnemy)
                {
                    boid_list[i].IsEnemy = true;
                    boid_list[i].Speed -= .5f;
                }

                if (i >= enemy_count && boid_list[i].IsEnemy)
                {
                    boid_list[i].IsEnemy = false;
                    boid_list[i].Speed += .5f;
                }
            }

            Boids = boid_list.ToArray();
        }
    }
}
