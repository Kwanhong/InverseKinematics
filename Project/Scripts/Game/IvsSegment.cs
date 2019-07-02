using System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using SFML.System;
using static InversKinematics.Data;
using static InversKinematics.Utility;

namespace InversKinematics
{
    public class IvsSegment
    {
        Vector2f StartPos { get; set; }
        Vector2f EndPos { get; set; }
        float Length { get; set; }
        float Angle { get; set; }

        IvsSegment Parent {get;set;} = null;

        public IvsSegment(float x, float y, float length, float angle)
        {
            this.StartPos = new Vector2f(x, y);
            this.Length = length;
            this.Angle = angle;
        }

        public IvsSegment(IvsSegment parent, float length, float angle) {
            this.Parent = parent;
            this.Length = length;
            this.Angle = angle;
            LookAt(this.Parent.StartPos);
        }

        private void CalculateEndPos()
        {
            float dx = Length * MathF.Cos(Angle);
            float dy = Length * MathF.Sin(Angle);
            EndPos = new Vector2f(StartPos.X + dx, StartPos.Y + dy);
        }

        public void LookAt(Vector2f target)
        {
            Vector2f direction = target - StartPos;
            Angle = GetAngle(direction);

            direction = SetMagnitude(direction, Length);
            direction *= -1;

            StartPos = target + direction;
        }

        public void Update()
        {
            CalculateEndPos();
            if (Parent != null) {
                LookAt(Parent.StartPos);
            }
        }

        public void Display()
        {
            Vector2f size = new Vector2f(Length, Length * 0.3f);
            RectangleShape rect = new RectangleShape(size);
            rect.Origin = new Vector2f(0, size.Y / 2);
            rect.Position = StartPos;
            rect.Rotation = ToDegree(Angle);
            window.Draw(rect);
        }
    }
}