using System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using SFML.System;
using static InversKinematics.Data;
using static InversKinematics.Constants;
using static InversKinematics.Utility;

namespace InversKinematics
{
    public class Tentacle
    {
        public NoiseFactors noiseFactors;
        private Segment tentacleRoot;

        public Tentacle(NoiseFactors noiseFactors, float posX = winSizeX / 2, float posY = winSizeY, float angle = -90, float rootLength = 30f, float reductionRatio = 0.95f, int size = 50)
        {
            this.noiseFactors = noiseFactors;

            tentacleRoot = new Segment(posX, posY, rootLength, ToRadian(angle), noiseFactors);
            Segment current = tentacleRoot;
            for (int i = 0; i < size; i++)
            {
                rootLength *= reductionRatio;
                var noiseOffset = (int)Map(i, 0, size, 0, noiseFactors.Size);
                Segment next = new Segment(current, rootLength, 0, noiseOffset);
                current.Child = next;
                current = next;
            }
        }

        public void Update()
        {
            Segment next = tentacleRoot;
            while (next != null)
            {
                next.Wiggle();
                next.Update();
                next = next.Child;
            }
        }

        public void Display()
        {
            Segment next = tentacleRoot;
            while (next != null)
            {
                next.Display();
                next = next.Child;
            }
        }

    }
}