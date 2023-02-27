using System;
using SkiaSharp;

using SteeringBehavioursCore.Model;
using SteeringBehavioursCore.Model.Field;
using SteeringBehavioursCore.Model.Boid;
using SteeringBehavioursCore.Model.Behaviour;

namespace SteeringBehavioursCore.Renderer
{
    public class RendererSkiaSharp : IRenderer
    {
        private const float BoidRadius = 4f;
        private readonly Color _backgroundColor = new Color(0, 0, 50);
        private readonly Color _wallColor = new Color(255, 255, 255);
        private readonly Color _boidColor = new Color(250, 250, 227);
        private readonly Color _enemyColor = new Color(247, 175, 49);
        private readonly SKCanvas _canvas;
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
            DrawField();
            DrawWall();
            foreach (var boid in field.Boids)
            {
                if (boid is IEnemy)
                    DrawTailBoid(boid, _enemyColor);
                else
                    DrawTailBoid(boid, _boidColor);
            }
        }

        public void DrawBoid(NormalBoid boid, Color color)
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

        public void DrawWall()
        {
            Point pt1 = new Point(AvoidWallsBehaviour.WallPad, AvoidWallsBehaviour.WallPad);
            Point pt2 = new Point(AvoidWallsBehaviour.WallPad, 
                BaseField.Height - AvoidWallsBehaviour.WallPad);
            Point pt3 = new Point(BaseField.Width - AvoidWallsBehaviour.WallPad,
                BaseField.Height - AvoidWallsBehaviour.WallPad);
            Point pt4 = new Point(BaseField.Width - AvoidWallsBehaviour.WallPad,
                AvoidWallsBehaviour.WallPad);
            DrawLine(pt1, pt2, 2, _wallColor);
            DrawLine(pt2, pt3, 2, _wallColor);
            DrawLine(pt3, pt4, 2, _wallColor);
            DrawLine(pt4, pt1, 2, _wallColor);
        }

        public void DrawField()
        {
            Point pt1 = new Point(0, 0);
            Point pt2 = new Point(0, BaseField.Height);
            Point pt3 = new Point(BaseField.Width, BaseField.Height);
            Point pt4 = new Point(BaseField.Width, 0);
            DrawLine(pt1, pt2, 2, _wallColor);
            DrawLine(pt2, pt3, 2, _wallColor);
            DrawLine(pt3, pt4, 2, _wallColor);
            DrawLine(pt4, pt1, 2, _wallColor);
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

        public void DrawTriangle(Point center, float direction, float speed, float size, Color color)
        {
            _paint.Color = ConvertColor(color);
            Point p1 = new Point(center.X + size * Math.Cos(direction / 180 * Math.PI), center.Y + size * Math.Sin(direction / 180 * Math.PI));
            Point p2 = new Point(center.X - size * Math.Cos(direction / 180 * Math.PI), center.Y - size * Math.Sin(direction / 180 * Math.PI));
            //速率越大，头部越尖
            Point p3 = new Point(center.X - size * Math.Sin(direction / 180 * Math.PI) * 2 * speed, center.Y + size * Math.Cos(direction / 180 * Math.PI) * 2 * speed);
            DrawLine(p1, p2, size, color);
            DrawLine(p1, p3, size, color);
            DrawLine(p2, p3, size, color);
        }

        public void DrawTailBoid(IBoid boid, Color color)
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

            DrawTriangle(new Point(boid.Position.X, boid.Position.Y), boid.Velocity.GetAngle(), boid.Velocity.GetCurrentSpeed(), boid.Size, color);
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
