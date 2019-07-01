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
    public class Game
    {
        private int tentacleCount = 1;
        private Tentacle[] tentacles;

        public Game()
        {
            Initialize();
            Run();
        }

        private void Initialize()
        {
            window.SetFramerateLimit(60);
            window.Closed += OnClose;
            window.KeyPressed += OnKeyPressed;

            tentacles = new Tentacle[tentacleCount];
            for (int i = 0; i < tentacleCount; i++)
            {
                NoiseFactors noiseFactors = new NoiseFactors(randomSeed: i, interval: 20);
                float xPosition = Map(i, 0, tentacleCount, winSizeX * 0.45f, winSizeX * 0.55f);
                tentacles[i] = new Tentacle(noiseFactors, posX: xPosition);
            }
        }

        private void Run()
        {
            while (window.IsOpen)
            {
                HandleEvnet();
                Update();
                Render();
            }
        }

        private void HandleEvnet()
        {
            window.DispatchEvents();
        }

        private void Update()
        {
            foreach (var tentacle in tentacles)
                tentacle.Update();
        }

        private void Render()
        {
            foreach (var tentacle in tentacles)
                tentacle.Display();

            window.Display();
            window.Clear(new Color(46, 46, 46));
        }

        private void OnClose(object sender, EventArgs e)
        {
            window.Close();
        }
        private void OnKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape)
            {
                window.Close();
            }
        }
    }

}