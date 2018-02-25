﻿using Hevadea.WorldGenerator;
using Hevadea.WorldGenerator.Functions;
using Hevadea.WorldGenerator.LevelFeatures;
using Hevadea.WorldGenerator.WorldFeatures;

namespace Hevadea.Game.Registry
{
    public static class GENERATOR
    {
        public static Generator DEFAULT;
        public static LevelGenerator OVERWORLD;
        public static LevelGenerator CAVE;

        public static void Initialize()
        {
            OVERWORLD = new LevelGenerator
            {
                LevelId = 0,
                LevelName = "overworld",
                Features =
                {
                    new BaseTerainFeature
                    {
                        Layers =
                        {
                            new TerrainLayer
                            {
                                Priority = 0,
                                Tile = TILES.WATER,
                                Threashold = 0f,
                                Function = new FlatFunction(0f)
                            },
                            new TerrainLayer
                            {
                                Priority = 1,
                                Tile = TILES.GRASS,
                                Threashold = 1f,
                                Function = new IslandFunction()
                            },
                            new TerrainLayer
                            {
                                Priority = 2,
                                Tile = TILES.SAND,
                                Threashold = 0.8f,
                                Function = new CombinedFunction(new PerlinFunction(2, 0.5, 30), new IslandFunction()),
                                TileRequired = {TILES.WATER}
                            },
                            new TerrainLayer
                            {
                                Priority = 3,
                                Tile = TILES.GRASS,
                                Threashold = 0.95f,
                                Function = new IslandFunction(),
                                TileRequired = {TILES.WATER}
                            },
                            new TerrainLayer
                            {
                                Priority = 4,
                                Tile = TILES.ROCK,
                                Threashold = 1f,
                                Function = new CombinedFunction(new PerlinFunction(2, 0.5, 15), new IslandFunction()),
                                TileRequired = {TILES.GRASS}
                            }
                        }
                    },
                    new HouseFeature(),
                    new CompoundFeature("Adding plants...")
                    {
                        Content =
                        {
                            new PlantFeature(ENTITIES.TREE)
                            {
                                Chance = 3,
                                CanBePlantOn = {TILES.GRASS},
                                PlacingFunction = new PerlinFunction(2, 0.5, 15),
                                Threashold = 0.7f
                            },
                            new PlantFeature(ENTITIES.GRASS)
                            {
                                Chance = 5,
                                CanBePlantOn = {TILES.GRASS},
                                PlacingFunction = new PerlinFunction(),
                                Threashold = 0.7f
                            }
                        }
                    }
                }
            };


            CAVE = new LevelGenerator
            {
                LevelId = 1,
                LevelName = "cave",

                Features =
                {
                    new BaseTerainFeature
                    {
                        Layers =
                        {
                            new TerrainLayer
                            {
                                Priority = 0,
                                Tile = TILES.ROCK,
                                Threashold = 0f,
                                Function = new FlatFunction(0f)
                            },
                            new TerrainLayer
                            {
                                Priority = 1,
                                Tile = TILES.DIRT,
                                Threashold = 1.1f,
                                Function = new PerlinFunction(2, 1, 15)
                            },
                        }
                    },
                    new HouseFeature
                    {
                        CanBePlacedOn = { TILES.DIRT, TILES.ROCK },
                        Wall = TILES.ROCK
                    },
                }
            };

            DEFAULT = new Generator
            {
                Size = 256, Seed = 0,
                Levels = {OVERWORLD, CAVE},
                WorldFeatures = {new StairCaseFeature(OVERWORLD, CAVE)}
            };
        }
    }
}