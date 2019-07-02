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
        private FwdSegment tentacleRoot;

        public Tentacle(NoiseFactors noiseFactors, float posX = winSizeX / 2, float posY = winSizeY, float angle = -90, float rootLength = 30f, float reductionRatio = 0.95f, int size = 50)
        {
            this.noiseFactors = noiseFactors;

            tentacleRoot = new FwdSegment(posX, posY, rootLength, ToRadian(angle), noiseFactors);
            FwdSegment current = tentacleRoot;
            for (int i = 0; i < size; i++)
            {
                rootLength *= reductionRatio;
                var noiseOffset = (int)Map(i, 0, size, 0, noiseFactors.Size);
                FwdSegment next = new FwdSegment(current, rootLength, 0, noiseOffset);
                current.Child = next;
                current = next;
            }
        }

        public void Update()
        {
            FwdSegment next = tentacleRoot;
            while (next != null)
            {
                next.Wiggle();
                next.Update();
                next = next.Child;
            }
        }

        public void Display()
        {
            FwdSegment next = tentacleRoot;
            while (next != null)
            {
                next.Display();
                next = next.Child;
            }
        }

    }
}