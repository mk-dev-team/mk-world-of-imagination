﻿using Hevadea.Framework.Graphic.SpriteAtlas;

namespace Hevadea.Entities.Components
{
    public class Pickupable : EntityComponent
    {
        public Sprite OnPickupSprite { get; set; }

        public Pickupable(Sprite pickedupSprite)
        {
            OnPickupSprite = pickedupSprite;
        }
    }
}