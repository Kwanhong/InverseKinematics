using System;
using SFML.Graphics;
using SFML.Window;
using SFML.Audio;
using SFML.System;
using System.Collections.Generic;
using static InversKinematics.Data;
using static InversKinematics.Constants;
using static InversKinematics.Utility;

namespace InversKinematics
{
    public class Game
    {
        Vector2f head;
        List<IvsSegment> segments;
        int segmentCount;

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
            
            segmentCount = 5;
            segments = new List<IvsSegment>();
            segments.Add(new IvsSegment(winSizeX / 2, winSizeY / 2, 1, 0));

            for (int i = 1; i < segmentCount; i++)
                segments.Add(new IvsSegment(segments[i - 1], 50 * MathF.Sin(Map(i, 0, segmentCount, 0, MathF.PI)), 0));

            head = new Vector2f(winSizeX, winSizeY);
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
            DispatchEventsImmediatly();
        }

        private void Update()
        {
            segments[0].LookAt(head);
            foreach (var segment in segments)
                segment.Update();

        }

        private void ResizeSegments(List<IvsSegment> segments)
        {
            for (int i = 1; i < segmentCount; i++)
                segments[i].Length = 50 * MathF.Sin(Map(i, 0, segmentCount, 0, MathF.PI));

        }

        private void Render()
        {
            foreach (var segment in segments)
                segment.Display();

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
            if (e.Code == Keyboard.Key.F5)
            {
                Initialize();
            }
            if (e.Code == Keyboard.Key.Add)
            {
                int parentIdx = segments.Count - 1;
                segments.Add(new IvsSegment(segments[parentIdx], 50 * MathF.Sin(Map(parentIdx, 0, segmentCount, 0, MathF.PI)), 0));
                segmentCount++;
                ResizeSegments(segments);
            }
            if (e.Code == Keyboard.Key.Subtract)
            {
                if (segments.Count > 5)
                {
                    segments.RemoveAt(segments.Count - 1);
                    segmentCount--;
                    ResizeSegments(segments);
                }
            }

        }

        private void DispatchEventsImmediatly()
        {
            OnKeyPressed();
        }

        private void OnKeyPressed()
        {
            Vector2f delta = new Vector2f(0, 0);

            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                delta.Y = -10f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                delta.X = -10f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                delta.Y = +10f;
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                delta.X = +10f;

            head += delta;

            if (GetMagnitude(delta) < 0.1f)
                head = (Vector2f)Mouse.GetPosition(window);
        }
    }

}