using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteeringBehavioursCore.Model.Field;

namespace SteeringBehavioursCore.Model.Interaction
{
    public class FlockingBoidsInteraction : BaseInteraction
    {
        public FlockingBoidsInteraction(IField field) : base(field)
        {
        }
        public override void OnMouseDown(int mouse, float x, float y)
        {
            switch (mouse)
            {
                case 1048576:
                    int boids_count = _field.Boids.Length;
                    int enemy_count = _field.Boids.Count(b => b.IsEnemy);
                    (_field as FlockingBoidsField).SetBoidsCount(boids_count + 10, enemy_count + 1);
                    break;
                case 2097152:
                    int boids_count2 = _field.Boids.Length;
                    int enemy_count2 = _field.Boids.Count(b => b.IsEnemy);
                    if (boids_count2 > 10 && enemy_count2 > 0)
                    {
                        (_field as FlockingBoidsField).SetBoidsCount(boids_count2 - 10, enemy_count2 - 1);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
