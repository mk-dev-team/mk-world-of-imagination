﻿using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.ES30;
using System;

namespace WorldOfImagination.Framework
{
    public class Host
    {
        Game HostedGame;
        GameWindow GameWindow;

        public int Width { get { return GameWindow.Width; } }
        public int Height { get { return GameWindow.Height; } }

        public Host(Game hostedGame, int windowWidth = 800, int windowHeight = 600, string title = "Game Host")
        {
            HostedGame = hostedGame;
            HostedGame.Host = this;
            GameWindow = new GameWindow(windowWidth, windowHeight);
            GameWindow.RenderFrame += DrawHandle;
            GameWindow.UpdateFrame += UpdateHandle;
            GameWindow.Load += LoadHandle;
            GameWindow.Closing += ExitHandle;
            GameWindow.Resize += ResizeHandle;
            GameWindow.Title = title;
            Console.Title = title;
        }

        private void LoadHandle(object sender, EventArgs e)
        { HostedGame.OnLoad(); }

        private void DrawHandle(object sender, FrameEventArgs e)
        { HostedGame.OnDraw(); GameWindow.SwapBuffers(); }

        private void UpdateHandle(object sender, FrameEventArgs e)
        { HostedGame.OnUpdate(e.Time); }

        private void ExitHandle(object sender, EventArgs e)
        { HostedGame.OnExit(); }

        private void ResizeHandle(object sender, EventArgs e)
        { GL.Viewport(0, 0, GameWindow.Width, GameWindow.Height); }

        public void Run()
        {
            Console.Title = $"World Of Imagination (OpenGL {GL.GetString(StringName.Version)})";

            Debugger.WriteLog("Game host created...", LogType.Info, nameof(Host));
            Debugger.WriteLog("GL Renderer: " + GL.GetString(StringName.Renderer), LogType.Info, nameof(Host));
            Debugger.WriteLog("GL Vendor: " + GL.GetString(StringName.Vendor), LogType.Info, nameof(Host));
            Debugger.WriteLog("GL Shader Version: " + GL.GetString(StringName.ShadingLanguageVersion), LogType.Info, nameof(Host));
            Debugger.WriteLog("GL Version: " + GL.GetString(StringName.Version), LogType.Info, nameof(Host));
            Debugger.WriteLog("GL Extensions: " + GL.GetString(StringName.Extensions), LogType.Info, nameof(Host));

            GameWindow.Run();
        }

        public void Clear(Color4 clearColor)
        {
            GL.ClearColor(clearColor);
            GL.Clear(ClearBufferMask.ColorBufferBit  | ClearBufferMask.DepthBufferBit);
        }
    }
}