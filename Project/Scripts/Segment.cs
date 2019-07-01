using System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using SFML.System;
using static InversKinematics.Data;
using static InversKinematics.Utility;

namespace InversKinematics
{

    class Segment
    {
        public Vector2f StartPos { get; set; }
        public Vector2f EndPos { get; set; }
        public float Length { get; set; }
        public float Angle { get; set; }

        public Segment Parent { get; set; } = null;
        public Segment Child { get; set; } = null;

        private float rootAngle;
        private float localAngle;
        private float[] noise = Noise(800);
        private int offset = 0;

        public Segment(float X, float Y, float length, float angle)
        {
            this.StartPos = new Vector2f(X, Y);
            this.Length = length;
            this.Angle = angle;
            this.rootAngle = this.Angle;
            this.localAngle = this.Angle;
            CalculateEndPos();
        }

        public Segment(Segment parent, float length, float angle, int offset)
        {
            this.Parent = parent;
            this.StartPos = this.Parent.EndPos;
            this.Length = length;
            this.Angle = angle;
            this.rootAngle = this.Angle;
            this.localAngle = this.Angle;
            this.offset = offset;
            CalculateEndPos();
        }

        public void Wiggle()
        {
            int interval = 10;
            float minAngle = rootAngle - 0.1f;
            float maxAngle = rootAngle + 0.1f;
            localAngle = Map(noise[offset], GetMin(noise), GetMax(noise), minAngle, maxAngle);
            if (offset < noise.Length - interval) offset += interval;
            else offset = 0;
        }

        public void Update()
        {
            this.Angle = localAngle;
            CalculateStartPos();
            CalculateEndPos();

        }

        private void CalculateStartPos()
        {
            if (Parent == null) return;
            this.Angle += Parent.Angle;
            this.StartPos = Parent.EndPos;
        }
        private void CalculateEndPos()
        {
            float x = Length * MathF.Cos(Angle);
            float y = Length * MathF.Sin(Angle);
            EndPos = new Vector2f(StartPos.X + x, StartPos.Y + y);
        }

        public void Display()
        {
            float weight = Length * 0.5f;
            weight = Limit(weight, 0.2f, 100);

            RectangleShape rect = new RectangleShape(new Vector2f(Length, weight));
            rect.OutlineThickness = 1f;
            rect.OutlineColor = new Color(225, 255, 255, 125);
            rect.Origin = new Vector2f(0, weight * 0.5f);
            rect.Position = StartPos;
            rect.Rotation = RadianToDegree(Angle);
            window.Draw(rect);

            if (this.Parent == null)
            {
                VertexArray noiseLine = new VertexArray(PrimitiveType.Lines);
                for (var i = 0; i < noise.Length; i++)
                {
                    noiseLine.Append(new Vertex(new Vector2f((float)i, noise[i] * 150), Color.White));
                }
                window.Draw(noiseLine);
            }
        }
    }
}