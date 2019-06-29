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

        private Segment parent;
        private float localAngle;
        private float[] noise = Noise(256, 15);
        private int noiseCount = 0;

        public Segment(float X, float Y, float length, float angle)
        {
            this.StartPos = new Vector2f(X, Y);
            this.Length = length;
            this.Angle = angle;
            this.localAngle = this.Angle;
            CalculateEndPos();
        }

        public Segment(Segment parent, float length, float angle)
        {
            this.parent = parent;
            this.StartPos = this.parent.EndPos;
            this.Length = length;
            this.Angle = angle;
            this.localAngle = this.Angle;
            CalculateEndPos();
        }

        public void Wiggle()
        {
            localAngle = Map(noise[noiseCount], 0, 1, -1, 1);
            if (noiseCount < noise.Length - 1) noiseCount++;
            else noiseCount = 0;
        }

        public void Update()
        {
            this.Angle = localAngle;
            CalculateStartPos();
            CalculateEndPos();
        }

        private void CalculateStartPos()
        {
            if (parent == null) return;
            this.Angle += parent.Angle;
            this.StartPos = parent.EndPos;
        }
        private void CalculateEndPos()
        {
            float x = Length * MathF.Cos(Angle);
            float y = Length * MathF.Sin(Angle);
            EndPos = new Vector2f(StartPos.X + x, StartPos.Y + y);
        }

        public void Display()
        {
            VertexArray line = new VertexArray(PrimitiveType.Lines, 2);
            line[0] = new Vertex(StartPos, Color.White);
            line[1] = new Vertex(EndPos, Color.White);
            window.Draw(line);
        }
    }
}