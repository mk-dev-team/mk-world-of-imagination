﻿using Hevadea.Worlds;
using Hevadea.Worlds.Level;

namespace Hevadea.WorldGenerator
{
    public abstract class LevelFeature
    {
        public abstract string GetName();
        public abstract float GetProgress();
        public abstract void Apply(Generator gen, LevelGenerator levelGen, Level level);
    }
}