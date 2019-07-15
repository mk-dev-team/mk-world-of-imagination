﻿using Hevadea.Entities;
using Hevadea.Entities.Components.Attributes;
using Hevadea.Entities.Components.States;
using Hevadea.Framework.Extension;
using Hevadea.Framework.Graphic;
using Hevadea.Framework.Utils;
using Hevadea.Worlds;
using Microsoft.Xna.Framework;

namespace Hevadea.Systems.HealthSystem
{
    public class HealthBarRenderer : EntityDrawSystem
    {
        public const float HEALTH_BAR_WIDTH  = 24f;
        public const float HEALTH_BAR_HEIGHT = 2f;

        public HealthBarRenderer()
        {
            Filter.AllOf(typeof(Health)).NoneOf(typeof(PlayerBody));
        }

        public override void Draw(Entity entity, LevelSpriteBatchPool pool, GameTime gameTime)
        {
            var health = Mathf.Max(entity.GetComponent<Health>().ValuePercent, 0.05f);
 
            if (health < 0.95f)
            {
                var barPosition = entity.Position - new Vector2(HEALTH_BAR_WIDTH / 2, HEALTH_BAR_HEIGHT / 2);
                var barBound = new Vector2(HEALTH_BAR_WIDTH * health, HEALTH_BAR_HEIGHT);

                pool.Overlay.FillRectangle(barPosition + new Vector2(1f, 1f), barBound, Color.Black * 0.45f);

                pool.Overlay.FillRectangle(barPosition, barBound, Color.Lerp(Color.Red, Color.Green, health));
            }
        }
    }
}
