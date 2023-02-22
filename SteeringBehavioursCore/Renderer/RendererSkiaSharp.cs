using System;
using SteeringBehavioursCore.Model;
using SkiaSharp;

namespace SteeringBehavioursCore.Renderer
{
    public class RendererSkiaSharp : IRenderer
    {
        private const float BoidRadius = 4f;
        private readonly Color _backgroundColor = new Color(0, 0, 50);
        private readonly Color _boidColor = new Color(250, 250, 227);
        private readonly SKCanvas _canvas;
        private readonly Color _enemyColor = new Color(247, 175, 49);
        private readonly SKPaint _paint;

        public RendererSkiaSharp(SKCanvas canvas)
        {
            _canvas = canvas;
            _paint = new SKPaint
            {
                IsAntialias = true
            };
        }

        public void Render(IField field)
        {
            Clear(_backgroundColor);
            foreach (var boid in field.Boids)
                if (boid.IsEnemy) DrawTailBoid(boid, _enemyColor);
                else DrawTailBoid(boid, _boidColor);
        }

        public void DrawBoid(Boid boid, Color color)
        {
            FillCircle(new Point(boid.Position.X, boid.Position.Y), BoidRadius, color);
        }

        public void Clear(Color color)
        {
            _canvas.Clear(ConvertColor(color));
        }

        public void Dispose()
        {
            _paint.Dispose();
        }

        public void DrawLine(Point pt1, Point pt2, float lineWidth, Color color)
        {
            _paint.Color = ConvertColor(color);
            _canvas.DrawLine(ConvertPoint(pt1), ConvertPoint(pt2), _paint);
        }

        public void FillCircle(Point center, float radius, Color color)
        {
            _paint.Color = ConvertColor(color);
            _canvas.DrawCircle(ConvertPoint(center), radius, _paint);
        }

        public void FillTriangle(Point center, float direction, float radius, Color color)
        {
            _paint.Color = ConvertColor(color);
            Point p1 = new Point(center.X + radius * Math.Cos(direction / 180 * Math.PI) * 1.5, center.Y + radius * Math.Sin(direction / 180 * Math.PI) * 1.5);
            Point p2 = new Point(center.X - radius * Math.Cos(direction / 180 * Math.PI) * 1.5, center.Y - radius * Math.Sin(direction / 180 * Math.PI) * 1.5);
            Point p3 = new Point(center.X - radius * Math.Sin(direction / 180 * Math.PI) * 3, center.Y + radius * Math.Cos(direction / 180 * Math.PI) * 3);
            DrawLine(p1, p2, radius, color);
            DrawLine(p1, p3, radius, color);
            DrawLine(p2, p3, radius, color);
        }

        public void DrawTailBoid(Boid boid, Color color)
        {

            for (var i = 0; i < boid.Positions.Count; i++)
            {
                var frac = (i + 1f) / boid.Positions.Count;
                var alpha = (byte)(255 * frac * .5);
                FillCircle(new Point(boid.Positions[i].X, boid.Positions[i].Y),
                    BoidRadius / 2.5f,
                    new Color(color.R, color.G, color.B, alpha));
            }

            //FillCircle(new Point(boid.Position.X, boid.Position.Y), BoidRadius, color);

            FillTriangle(new Point(boid.Position.X, boid.Position.Y), boid.Velocity.GetAngle(), BoidRadius, color);
        }

        private SKColor ConvertColor(Color color)
        {
            return new SKColor(color.R, color.G, color.B, color.A);
        }

        private SKPoint ConvertPoint(Point pt)
        {
            return new SKPoint((float)pt.X, (float)pt.Y);
        }
    }
}
