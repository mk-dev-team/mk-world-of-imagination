﻿using Maker.Rise.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.Hevadea.Game.Tiles.Render
{
    public class ConnectedTileRender
    {
        private readonly SpriteSheet Sprites;

        public ConnectedTileRender(SpriteSheet sprites)
        {
            Sprites = sprites;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, TileConection connection)
        {
            var index = connection.ToByte();
            var x = index % 8;
            var y = index / 8;

            new Sprite(Sprites, new Point(x, y)).Draw(spriteBatch, position, Color.White);
        }
    }
}