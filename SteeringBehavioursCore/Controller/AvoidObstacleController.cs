﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringBehavioursCore.Model.Field;
using SteeringBehavioursCore.Renderer;

namespace SteeringBehavioursCore.Controller
{
    public class AvoidObstacleController
    {
        public IField Field { get; private set; }
        public IRenderer Renderer { get; private set; }

        public void CreateField()
        {
            Field = new AvoidObstacleField();
        }

        public void CreateRenderer(IRenderer renderer)
        {
            Renderer = renderer;
        }
    }
}
