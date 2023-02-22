using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringBehavioursCore.Model;
using SteeringBehavioursCore.Renderer;

namespace SteeringBehavioursCore.Controller
{
    public class ArriveController
    {
        private const int BoidsCount = 10;
        public IField Field { get; private set; }
        public IRenderer Renderer { get; private set; }

        public void CreateField()
        {
            Field = new ArriveField(BoidsCount);
        }

        public void CreateRenderer(IRenderer renderer)
        {
            Renderer = renderer;
        }
    }
}
