using System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using SFML.System;
using static InversKinematics.Data;

namespace InversKinematics
{
    public class Game
    {
        public Game()
        {
            Initialize();
            Run();
        }

        private void Initialize()
        {
            window.SetFramerateLimit(20);
            window.Closed += OnClose;
            window.KeyPressed += OnKeyPressed;
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

        }

        private void Render()
        {
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