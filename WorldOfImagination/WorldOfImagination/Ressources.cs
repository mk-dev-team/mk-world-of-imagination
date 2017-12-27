﻿using Maker.Rise;
using Maker.Rise.GameComponent.Ressource;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WorldOfImagination
{
    public static class Ressources
    {
        public static Texture2D img_tiles;
        public static Texture2D img_items;
        public static Texture2D img_entities;

        public static SpriteSheet tile_tiles;
        public static SpriteSheet tile_items;
        public static SpriteSheet tile_entities;

        public static void Load(RiseGame Game)
        {
            img_tiles = Game.Ressource.GetImage("tiles");
            img_items = Game.Ressource.GetImage("items");

            img_entities = Game.Ressource.GetImage("entities");

            tile_tiles = new SpriteSheet(img_tiles, new Point(32, 32));

            tile_items = new SpriteSheet(img_items, new Point(32, 32));
            tile_entities = new SpriteSheet(img_entities, new Point(48, 64));
            
        }

    }
}