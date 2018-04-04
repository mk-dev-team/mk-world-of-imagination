﻿using System.Collections.Generic;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Items;
using Hevadea.Tiles;
using Hevadea.Tiles.Renderers;
using Microsoft.Xna.Framework;

namespace Hevadea.Registry
{
    public static class TILES
    {
        public static List<Tile> ById = new List<Tile>();
        public static List<Tile> GroundTile = new List<Tile>();
        public static List<Tile> WaterTile = new List<Tile>();

        public static Tile VOID;
        public static Tile GRASS;
        public static Tile SAND;
        public static Tile WATER;
        public static Tile ROCK;
        public static Tile WOOD_FLOOR;
        public static Tile WOOD_WALL;
        public static Tile DIRT;
        public static Tile IRON_ORE;

        public static void Initialize()
        {
            ROCK       = new Tile();
            GRASS = new Tile(Color.Green);
            SAND = new Tile(Color.Yellow);
            WATER      = new Tile(Color.Blue);
            WOOD_FLOOR = new Tile(new TileRenderComposite(new Sprite(Ressources.TileTiles, 5)));
            WOOD_WALL  = new Tile(new TileRenderComposite(new Sprite(Ressources.TileTiles, 6)));
            VOID       = new Tile();
            DIRT       = new Tile(new TileRenderComposite(new Sprite(Ressources.TileTiles, 9)), Color.Brown);
            IRON_ORE   = new Tile(new TileRenderComposite(new Sprite(Ressources.TileTiles, 10)));

            WaterTile = new List<Tile>(){ WATER };
            GroundTile = new List<Tile>() { GRASS, SAND, WOOD_FLOOR, DIRT };
        }

        public static void AttachRender()
        {
            WATER.Render = new TileRenderComposite(new Sprite(Ressources.TileTiles, 4)) { ConnectTo = { VOID } };
            GRASS.Render = new TileRenderComposite(new Sprite(Ressources.TileTiles, 2));
            VOID.Render = new TileRenderComposite(new Sprite(Ressources.TileTiles, 8)) { ConnectTo = { WATER } };
            SAND.Render = new TileRenderComposite(new Sprite(Ressources.TileTiles, 3));
            ROCK.Render = new TileRenderComposite(new Sprite(Ressources.TileTiles, 1)) { ConnectTo = { IRON_ORE } };
            IRON_ORE.Render = new TileRenderComposite(new Sprite(Ressources.TileTiles, 10)) { ConnectTo = { ROCK } };
        }

        public static void AttachTags()
        {

            ROCK.AddTag(new Tags.Solide(), new Tags.Damage { ReplacementTile = DIRT });
            ROCK.AddTag(new Tags.Droppable(new Drop(ITEMS.STONE,1f, 2, 3), new Drop(ITEMS.COAL,1f, 0, 2)));

            SAND.AddTag(new Tags.Breakable { ReplacementTile = DIRT });
            SAND.AddTag(new Tags.Droppable(new Drop(ITEMS.SAND, 1f, 1, 1)));

            GRASS.AddTag(new Tags.Breakable { ReplacementTile = DIRT });
            GRASS.AddTag(new Tags.Spread { SpreadChance = 50, SpreadTo = { DIRT } });
            GRASS.AddTag(new Tags.Droppable(new Drop(ITEMS.GRASS_PATCH, 1f, 1, 1)));

            WATER.AddTag(new Tags.Spread { SpreadChance = 1, SpreadTo = { VOID } });
            WATER.AddTag(new Tags.Liquide());
            WATER.AddTag(new Tags.Ground{ MoveSpeed = 0.5f });

            DIRT.AddTag(new Tags.Damage { ReplacementTile = VOID });

            WOOD_WALL.AddTag(new Tags.Solide());
            WOOD_WALL.AddTag(new Tags.Damage { ReplacementTile = DIRT });
            WOOD_WALL.AddTag(new Tags.Droppable(new Drop(ITEMS.WOOD_WALL, 1f, 1, 1)));

            WOOD_FLOOR.AddTag(new Tags.Damage { ReplacementTile = DIRT });
            WOOD_FLOOR.AddTag(new Tags.Droppable(new Drop(ITEMS.WOOD_FLOOR, 1f, 1, 1)));

            IRON_ORE.AddTag(new Tags.Solide(), new Tags.Damage { ReplacementTile = DIRT });
            IRON_ORE.AddTag(new Tags.Droppable(new Drop(ITEMS.IRON_ORE, 1f, 1, 2)));

        }
    }
}