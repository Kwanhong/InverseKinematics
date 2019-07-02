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

        // Perlin Noize Factors
        public NoiseFactors noiseFactors;
        private float[] noise;
        private float offset = 0;

        // Root Segment
        public Segment(float X, float Y, float length, float angle, NoiseFactors noiseFactors)
        {
            this.Angle = angle;
            this.rootAngle = this.Angle;
            this.localAngle = this.Angle;

            this.Length = length;
            this.StartPos = new Vector2f(X, Y);
            this.noiseFactors = noiseFactors;
            this.noise = Noise(noiseFactors);
            CalculateEndPos();
        }

        // Child Segment
        public Segment(Segment parent, float length, float angle, float offset)
        {
            this.Parent = parent;
            this.StartPos = this.Parent.EndPos;
            this.noiseFactors = this.Parent.noiseFactors;
            
            this.Angle = angle;
            this.rootAngle = this.Angle;
            this.localAngle = this.Angle;

            this.Length = length;
            this.offset = offset;
            this.noise = Noise(noiseFactors);
            CalculateEndPos();
        }

        public void Wiggle()
        {
            float minAngle = rootAngle - 0.1f;
            float maxAngle = rootAngle + 0.1f;
            localAngle = Map(noise[(int)offset], GetMin(noise), GetMax(noise), minAngle, maxAngle);
            if (offset < (float)noise.Length - noiseFactors.Interval) offset += noiseFactors.Interval;
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
            rect.OutlineColor = new Color(200, 200, 200, 125);
            rect.FillColor = new Color(100, 100,100);
            rect.Origin = new Vector2f(0, weight * 0.5f);
            rect.Position = StartPos;
            rect.Rotation = ToDegree(Angle);
            window.Draw(rect);

            // Displaying Perlin Noise
            VertexArray noiseLine = new VertexArray(PrimitiveType.Lines);
            for (var i = 0; i < noise.Length; i++)
                noiseLine.Append(new Vertex(new Vector2f((float)i, noise[i] * 600), new Color(100,100,100)));
            window.Draw(noiseLine);
        }
    }
}