﻿using System;
using Maker.Hevadea.Game.Entities;
using Maker.Rise;
using Microsoft.Xna.Framework;

namespace Maker.Hevadea.Game
{
    public class Camera
    {
        public Entity FocusEntity;
        public float X;
        public float Y;
        public float Zoom = 4f;

        public Camera(Entity focusEntity)
        {
            FocusEntity = focusEntity;
        }

        public int GetWidth()
        {
            return (int) (Engine.Graphic.GetWidth() / Zoom);
        }

        public int GetHeight()
        {
            return (int) (Engine.Graphic.GetHeight() / Zoom);
        }

        public Matrix GetTransform()
        {
            Update();
            return Matrix.CreateScale(Zoom) * Matrix.CreateTranslation(
                       (float) Math.Floor(-(X * Zoom - Engine.Graphic.GetWidth() / 2f)),
                       (float) Math.Floor(-(Y * Zoom - Engine.Graphic.GetHeight() / 2f)), 0f);
        }

        public void Update()
        {
            X = (float) Math.Floor(FocusEntity.X + FocusEntity.Width / 2f);
            Y = (float) Math.Floor(FocusEntity.Y + FocusEntity.Height / 2f);
        }
    }
}