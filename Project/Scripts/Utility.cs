using System;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using SFML.Audio;
using static InversKinematics.Constants;

namespace InversKinematics
{
    public class Utility
    {
        public static float GetMagnitude(Vector2f vector)
        {
            return MathF.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
        }

        public static Vector2f SetMagnitude(Vector2f vector, float mag)
        {
            vector = Normalize(vector);
            vector *= mag;
            return vector;
        }

        public static Vector2f Normalize(Vector2f vector)
        {
            var magnitude = GetMagnitude(vector);
            return vector /= magnitude;
        }

        public static Vector2f Limit(Vector2f vector, float min, float max)
        {
            if (GetMagnitude(vector) < min)
                vector = SetMagnitude(vector, min);
            else if (GetMagnitude(vector) > max)
                vector = SetMagnitude(vector, max);
            return vector;
        }

        public static Vector2f Limit(Vector2f vector, float max)
        {
            if (GetMagnitude(vector) > max)
                vector = SetMagnitude(vector, max);
            return vector;
        }

        public static float Limit(float var, float min, float max)
        {
            if (var < min) var = min;
            else if (var > max) var = max;
            return var;
        }

        public static float Limit(float var, float max)
        {
            if (var > max) var = max;
            return var;
        }

        public static float Distnace(Vector2f pos1, Vector2f pos2)
        {
            return MathF.Sqrt
            (
                MathF.Pow(pos1.X - pos2.X, 2) +
                MathF.Pow(pos1.Y - pos2.Y, 2)
            );
        }

        public static Vector2f RotateVector(Vector2f vector, float angle)
        {
            return new Vector2f
            (
                MathF.Cos(angle) * vector.X -
                MathF.Sin(angle) * vector.Y,
                MathF.Sin(angle) * vector.X +
                MathF.Cos(angle) * vector.Y
            );
        }

        public static float Map(float value, float start1, float stop1, float start2, float stop2)
        {
            return ((value - start1) / (stop1 - start1)) * (stop2 - start2) + start2;
        }

        public static float RadianToDegree(float degree)
        {
            return degree * 180 / MathF.PI;
        }

        public static float DegreeToRadian(float radian)
        {
            return radian * MathF.PI / 180;
        }

        //Perlin Noise
        public static float[] Noise(int count, int octave = 10)
        {
            float[] output = new float[count];
            float[] seed = new float[count];

            Random rand = new Random(randomSeed);
            for (int i = 0; i < count; i++)
                seed[i] = (float)rand.NextDouble();

            for (int x = 0; x < count; x++)
            {
                float noise = 0f;
                float scale = 1f;
                float scaleAcc = 0f;

                for (int o = 0; o < octave; o++)
                {
                    int pitch = count >> o;
                    int sample1 = (x / pitch) * pitch;
                    int sample2 = (sample1 + pitch) % count;
                    float blend = (float)(x - sample1) / (float)pitch;
                    float sample = (1f - blend) * seed[sample1] + blend * seed[sample2];

                    noise += sample * scale;
                    scaleAcc += scale;
                    scale = scale / 2f;
                }
                output[x] = noise / scaleAcc;
            }
            return output;
        }

        public static float GetMin(float[] array) {
            float min = array[0];
            foreach (var element in array) {
                if (element <= min) min = element;
            }
            return min;
        }

        public static float GetMax(float[] array) {
            float max = array[0];
            foreach (var element in array) {
                if (element >= max) max = element;
            }
            return max;
        }
    }
}