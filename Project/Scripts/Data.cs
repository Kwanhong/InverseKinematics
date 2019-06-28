using System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using SFML.System;
using static InversKinematics.Constants;

namespace InversKinematics
{
    static class Data
    {
        public static RenderWindow window = 
        new RenderWindow
        (
            new VideoMode(winSizeX, winSizeY),
            winTitle,
            styles
        );
    }
}