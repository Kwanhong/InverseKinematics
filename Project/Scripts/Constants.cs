using System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using SFML.System;

namespace InversKinematics
{
    public static class Constants
    {
        public static int randomSeed = 51561;
        public static uint winSizeX = 800;
        public static uint winSizeY = 600;
        public static Styles styles = Styles.Resize;
        public static string winTitle = "INVERSE KINEMATIC";
    }
}