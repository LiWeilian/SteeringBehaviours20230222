using SteeringBehavioursCore.Model;
using SteeringBehavioursCore.Renderer;

namespace SteeringBehavioursCore.Controller
{
    public class Controller
    {
        private const int BoidsCount = 100;
        private const int EnemyCount = 5;
        public Field Field { get; private set; }
        public IRenderer Renderer { get; private set; }

        public void CreateField()
        {
            Field = new Field(
                BoidsCount,
                EnemyCount
            );
        }

        public void CreateRenderer(IRenderer renderer)
        {
            Renderer = renderer;
        }
    }
}
