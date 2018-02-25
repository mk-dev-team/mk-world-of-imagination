﻿using Hevadea.Game.Entities;
using Hevadea.Game.Entities.Component;
using Hevadea.Game.Entities.Creatures;
using Hevadea.Game.Registry;
using Hevadea.Game.Tiles;
using Maker.Rise.Ressource;

namespace Hevadea.Game.Items
{
    public class PlacableItem : Item
    {
        private readonly EntityBlueprint _blueprint;
        
        public PlacableItem(string name, EntityBlueprint blueprint, Sprite sprite) : base(name, sprite)
        {
            _blueprint = blueprint;
        }

        public override void InteracteOn(Entity user, TilePosition pos)
        {
            if (user.Level.GetEntityOnTile(pos).Count == 0)
            {
                var inventory = user.Components.Get<Inventory>();

                if (inventory != null) inventory.Content.Remove(this, 1);

                user.Level.SpawnEntity(_blueprint.Build(), pos.X, pos.Y);

                if (user is PlayerEntity p)
                    if (p.Components.Get<Inventory>().Content.Count(p.HoldingItem) == 0)
                        p.HoldingItem = null;
            }
            else
            {
                base.InteracteOn(user, pos);
            }
        }
    }
}