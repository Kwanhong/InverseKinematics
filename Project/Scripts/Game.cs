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
        private Segment tentacleRoot;

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

            tentacleRoot = new Segment(winSizeX / 2, winSizeY, 10, DegreeToRadian(-45));
            Segment current = tentacleRoot;
            for (int i = 0; i < 50; i++)
            {
                Segment next = new Segment(current, 10, 0, (int)Map(i, 0, 256, 0, 50));
                current.Child = next;
                current = next;
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
            Segment next = tentacleRoot;
            while (next != null) {
                next.Wiggle();
                next.Update();
                next = next.Child;
            }
        }

        private void Render()
        {
            Segment next = tentacleRoot;
            while (next != null) {
                next.Display();
                next = next.Child;
            }

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