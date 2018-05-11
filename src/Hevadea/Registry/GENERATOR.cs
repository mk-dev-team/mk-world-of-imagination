﻿using Hevadea.GameObjects;
using Hevadea.WorldGenerator;
using Hevadea.WorldGenerator.Functions;
using Hevadea.WorldGenerator.LevelFeatures;
using Hevadea.WorldGenerator.WorldFeatures;

namespace Hevadea.Registry
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
                Properties = LEVELS.SURFACE,
                Features =
                {
                    new Terrain
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
                    new CompoundFeature("Adding animals...")
                    {
                        Content =
                        {
                            new PopulateFeature(EntityFactory.FISH)
                            {
                                Chance = 25,
                                CanBePlantOn = {TILES.WATER},
                                PlacingFunction = new PerlinFunction(1, 0.5, 10),
                                Threashold = 0.5f,
                            },
                            new PopulateFeature(EntityFactory.CHIKEN)
                            {
                                Chance = 100,
                                CanBePlantOn = {TILES.GRASS},
                                PlacingFunction = new PerlinFunction(1, 0.5, 10),
                                Threashold = 0.7f,
                            },
                        }
                    },
                    new CompoundFeature("Adding plants...")
                    {
                        Content =
                        {
                            new PopulateFeature(EntityFactory.TREE)
                            {
                                Chance = 3,
                                CanBePlantOn = {TILES.GRASS},
                                PlacingFunction = new PerlinFunction(2, 0.5, 15),
                                Threashold = 0.7f,
 
                            },
                            new PopulateFeature(EntityFactory.FLOWER)
                            {
                                Chance = 10,
                                CanBePlantOn = {TILES.GRASS},
                                PlacingFunction = new PerlinFunction(1,1,5),
                                Threashold = 0.5f
                            },
                            new PopulateFeature(EntityFactory.GRASS)
                            {
                                Chance = 3,
                                CanBePlantOn = {TILES.GRASS},
                                PlacingFunction = new PerlinFunction(1,1,5),
                                Threashold = 0.5f
                            }
                        }
                    }
                }
            };


            CAVE = new LevelGenerator
            {
                LevelId = 1,
                LevelName = "cave",
                Properties = LEVELS.UNDERGROUND,
                Features =
                {
                    new Terrain
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
                                Function = new PerlinFunction(2, 1, 30)
                            },
							new TerrainLayer
                            {
                                Priority = 1,
                                Tile = TILES.WATER,
                                Threashold = 1.6f,
								TileRequired = { TILES.DIRT },
                                Function = new PerlinFunction(3, 1, 20, 679d)
                            },
                            new TerrainLayer
                            {
                                Priority = 2,
                                Tile = TILES.IRON_ORE,
                                Threashold = 1.2f,
                                TileRequired = { TILES.ROCK},
                                Function = new PerlinFunction(2, 1, 3),
                            }
                        }
                    },
					new BspDecorator
					{
						GenerateFloor = false,
                        GenerateWall = false,
                        GeneratePath = true,
                        Depth = 7,
					},
                    new HouseFeature
                    {
                        CanBePlacedOn = { TILES.DIRT, TILES.ROCK },
                        Wall = TILES.ROCK
                    },
                    new PopulateFeature(EntityFactory.ZOMBIE)
                    {
                        Chance = 400,
                        CanBePlantOn = {TILES.DIRT},
                        PlacingFunction = new PerlinFunction(1, 0.5, 10),
                        Threashold = 0.7f,
                    },
                }
            };

            DEFAULT = new Generator
            {
                Size = 256, Seed = 0,
                Levels = { OVERWORLD, CAVE },
				WorldFeatures = {new StairCaseFeature(OVERWORLD, CAVE), new SpawnAreaFeature()}
            };
        }
    }
}