using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SteeringBehavioursCore.Model.Behaviour;
using SteeringBehavioursCore.Model.Interaction;
using SteeringBehavioursCore.Model.Boid;

namespace SteeringBehavioursCore.Model.Field
{
    public class FlockingBoidsField : IField
    {
        public IBoid[] Boids
        {
            get
            {
                List<IBoid> boids = new List<IBoid>();
                normal_boids.ForEach(nb => boids.Add(nb));
                enemy_boids.ForEach(eb => boids.Add(eb));

                return boids.ToArray();
            }
        }
        public IFieldInteraction Interaction { get; private set; } 

        private float _width = 1200f, _height = 600f;

        private List<NormalBoid> normal_boids = new List<NormalBoid>();
        private List<EnemyBoid> enemy_boids = new List<EnemyBoid>(); 

        public FlockingBoidsField(int boidsCount, int enemyCount)
        {
            Interaction = new FlockingBoidsInteraction(this);

            if (enemyCount > boidsCount)
                throw new Exception(
                    "Number of enemies is bigger than number of boids");
            GenerateRandomBoids(boidsCount, enemyCount);
        }

        public void SetFieldSize(float width, float height)
        {
            if (width <= 0 || height <= 0)
                throw new Exception(
                    "Wrong size of field");
            (_width, _height) = (width, height);
        }

        private void GenerateRandomBoids(int boidsCount, int enemyCount)
        {
            this.IncreaseBoidsCount(boidsCount);
            this.IncreaseEnemiesCount(enemyCount);
        }

        public void Advance(float stepSize = 1)
        {
            Parallel.ForEach(Boids, boid => boid.Move(stepSize));
        }

        public void IncreaseBoidsCount(int boids_inc)
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

            for (int i = 0; i < boids_inc; i++)
            {
                NormalBoid new_boid = new NormalBoid(
                    (float)rnd.NextDouble() * _width,
                    (float)rnd.NextDouble() * _height,
                    (float)(rnd.NextDouble() - .5),
                    (float)(rnd.NextDouble() - .5),
                    (float)(1 + rnd.NextDouble()));
                normal_boids.Add(new_boid);

                behaviours.ForEach(behaviour => new_boid.AddBehaviour(behaviour));
            }
        }
        public void IncreaseEnemiesCount(int enemies_inc)
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

            for (int i = 0; i < enemies_inc; i++)
            {
                EnemyBoid new_boid = new EnemyBoid(
                    (float)rnd.NextDouble() * _width,
                    (float)rnd.NextDouble() * _height,
                    (float)(rnd.NextDouble() - .5),
                    (float)(rnd.NextDouble() - .5),
                    (float)(0.5 + rnd.NextDouble()));
                enemy_boids.Add(new_boid);

                behaviours.ForEach(behaviour => new_boid.AddBehaviour(behaviour));
            }
        }

        public void DecreaseBoidsCount(int boids_de)
        {
            int i = boids_de;
            while (i-- > 0 && normal_boids.Count > 0)
            {
                normal_boids.RemoveAt(normal_boids.Count - 1);
            }
        }

        public void DecreaseEnemiesCount(int enemies_de)
        {
            int i = enemies_de;
            while (i-- > 0 && enemy_boids.Count > 0)
            {
                enemy_boids.RemoveAt(enemy_boids.Count - 1);
            }
        }
    }
}
