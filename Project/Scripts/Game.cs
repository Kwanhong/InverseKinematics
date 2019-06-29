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
        private Segment segment1;
        private Segment segment2;

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

            segment1 = new Segment(winSizeX / 2, winSizeY / 2, 50, DegreeToRadian(-45));
            segment2 = new Segment(segment1, 50, DegreeToRadian(-45));
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
            segment1.Wiggle();
            segment1.Update();
            segment2.Wiggle();
            segment2.Update();
        }

        private void Render()
        {
            segment1.Display();
            segment2.Display();
            
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