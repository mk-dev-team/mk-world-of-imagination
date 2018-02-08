﻿using System.Collections.Generic;

namespace Maker.Hevadea.Game.Storage
{
    public class LevelStorage
    {
        public byte[] Tiles;
        public Dictionary<string, object>[] TilesData;
        public string Name { get; set; }
        public int Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public List<EntityStorage> Entities { get; set; } = new List<EntityStorage>();
    }
}