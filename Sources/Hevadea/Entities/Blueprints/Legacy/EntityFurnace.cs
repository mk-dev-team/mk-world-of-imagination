﻿using Hevadea.Entities.Components;
using Hevadea.Framework.Graphic.SpriteAtlas;
using Hevadea.Items;
using Hevadea.Registry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Hevadea.Entities.Blueprints
{
    public class EntityFurnace : Entity
    {
        private readonly Sprite _sprite;

        public EntityFurnace()
        {
            _sprite = new Sprite(Ressources.TileEntities, new Point(1, 1));

            AddComponent(new Breakable());
            AddComponent(new Light());
            AddComponent(new Dropable { Items = { new Drop(ITEMS.FURNACE, 1f, 1, 1) } });
            AddComponent(new Move());
            AddComponent(new Pushable());
            AddComponent(new Colider( new Rectangle(-6, -2, 12, 8) ));
            AddComponent(new Pickupable(_sprite));
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _sprite.Draw(spriteBatch, new Rectangle((int) X - 8, (int) Y - 8, 16, 16), Color.White);
        }
    }
}