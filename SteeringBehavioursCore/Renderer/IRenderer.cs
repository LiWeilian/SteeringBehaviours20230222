using System;
using SteeringBehavioursCore.Model;
using SteeringBehavioursCore.Model.Field;
using SteeringBehavioursCore.Model.Boid;

namespace SteeringBehavioursCore.Renderer
{
    public interface IRenderer : IDisposable
    {
        void Render(IField field);
        void DrawBoid(NormalBoid boid, Color color);

        void Clear(Color color);
        void DrawLine(Point pt1, Point pt2, float lineWidth, Color color);
        void FillCircle(Point center, float radius, Color color);
    }
}
