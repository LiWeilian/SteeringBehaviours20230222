﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SteeringBehavioursCore.Model.Behaviour;
using SteeringBehavioursCore.Model.Interaction;
using SteeringBehavioursCore.Model.Field;

namespace SteeringBehavioursCore.Model.Field
{
    public class ArriveField : IField
    {
        public Boid[] Boids { get; private set; }
        public IFieldInteraction Interaction { get; private set; }
        //public List<Behaviour.Behaviour> Behaviours { get; private set; }

        private float _width = 1200f, _height = 600f;

        public ArriveField(int boids_count)
        {
            Interaction = new ArriveInteraction(this);

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
            var behaviours = new List<Behaviour.Behaviour>
            {
                new FlockBehaviour(this),
                new AlignBehaviour(this),
                new AvoidBoidsBehaviour(this),
                new ArriveBehaviour(this),
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

                behaviours.ForEach(
                    behaviour => Boids[i].AddBehaviour(behaviour));
            }
        }
    }
}