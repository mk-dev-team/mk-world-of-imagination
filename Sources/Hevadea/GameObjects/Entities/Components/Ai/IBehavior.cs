﻿using Microsoft.Xna.Framework;

namespace Hevadea.Entities.Components.Ai
{
    public interface IBehavior
    {
        void IaAborted(Agent agent, AgentAbortReason why);
        void IaFinish(Agent agent);
        void Update(Agent agent, GameTime gameTime);
    }
}