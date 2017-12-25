﻿using Maker.Rise.GameComponent.Ressource;
using Maker.Rise.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WorldOfImagination.Game.Entities;

namespace WorldOfImagination.Game.Tiles
{
    public class Tile
    {
#region Tiles instaces
        public static Tile[] Tiles = new Tile[256];

        public static VoidTile Void = new VoidTile(0);
        public static GrassTile Grass = new GrassTile(1);
        public static SandTile Sand = new SandTile(2);
        public static WaterTile Water = new WaterTile(3);
        public static RockTile Rock = new RockTile(4);
#endregion

        public readonly byte ID;
        public Sprite Sprite;
        public bool BackgroundDirt = true;
        private Sprite DirtSprite;
        public Tile(byte id)
        {
            ID = id;
            if (Tiles[id] != null) throw new Exception($"Duplicate tile ids {ID}!");
            Tiles[ID] = this;
            Sprite = new Sprite(Ressources.tile_tiles, 0);
            DirtSprite = new Sprite(Ressources.tile_tiles, 0);
        }

        public virtual void Update(Level level, int tx, int ty)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, Level level, TilePosition pos)
        {


            var onScreen = pos.ToOnScreenPosition().ToVector2();

            bool u = level.GetTile(pos.X, pos.Y - 1) == this;
            bool d = level.GetTile(pos.X, pos.Y + 1) == this;
            bool l = level.GetTile(pos.X - 1, pos.Y) == this;
            bool r = level.GetTile(pos.X + 1, pos.Y) == this;

            bool ul = level.GetTile(pos.X - 1, pos.Y - 1) == this;
            bool ur = level.GetTile(pos.X + 1, pos.Y - 1) == this;
            bool dl = level.GetTile(pos.X - 1, pos.Y + 1) == this;
            bool dr = level.GetTile(pos.X + 1, pos.Y + 1) == this;

            DrawCorner(spriteBatch, l, ul, u, new Point(0, 0), new Point(0, 2), new Point(0, 3), new Point(2, 0), new Point(2, 2), (int)(onScreen.X + 0), (int)(onScreen.Y + 0));
            DrawCorner(spriteBatch, u, ur, r, new Point(1, 0), new Point(1, 2), new Point(0, 2), new Point(3, 0), new Point(2, 2), (int)(onScreen.X + 8), (int)(onScreen.Y + 0));

            DrawCorner(spriteBatch, r, dr, d, new Point(1, 1), new Point(1, 3), new Point(1, 2), new Point(3, 1), new Point(2, 2), (int)(onScreen.X + 8), (int)(onScreen.Y + 8));
            DrawCorner(spriteBatch, d, dl, l, new Point(0, 1), new Point(0, 3), new Point(1, 3), new Point(2, 1), new Point(2, 2), (int)(onScreen.X), (int)(onScreen.Y + 8));
        }

        public void DrawCorner(SpriteBatch spriteBatch,
                               bool a, bool b, bool c,
                               Point case1, Point case2, Point case3, Point case4, Point case5,
                               int x, int y)
        {

            if (BackgroundDirt) DirtSprite.DrawSubSprite(spriteBatch, new Vector2(x, y), new Point(0, 0), Color.White);

            if (!a & !c)
            {
                Sprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case1, Color.White);
            }
            else if (a & !c)
            {
                Sprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case2, Color.White);
            }
            else if (!a & c)
            {
                Sprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case3, Color.White);
            }
            else if (!b)
            {
                Sprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case4, Color.White);
            }
            else
            {
                Sprite.DrawSubSprite(spriteBatch, new Vector2(x, y), case5 + case1, Color.White);
            }
        }
        // Properties ---------------------------------------------------------

        /* Returns if the entity can walk on it */
        public virtual bool CanPass(Level level, TilePosition pos, Entity e)
        {
            return true;
        }

        // Interaction --------------------------------------------------------

        /* What happens when you are inside the tile (ex: lava) */
        public virtual void SteppedOn(Level level, TilePosition pos, Entity entity)
        {
        }


        public static bool Colide(TilePosition tile, EntityPosition position, int width, int height)
        {
            return Colision.Check(tile.X * ConstVal.TileSize,
                                  tile.Y * ConstVal.TileSize,
                                  
                                  ConstVal.TileSize, ConstVal.TileSize,

                                  position.X,
                                  position.Y,
                                  width, height);
        }

    }
}
