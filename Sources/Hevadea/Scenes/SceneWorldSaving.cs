﻿using Hevadea.Framework;
using Hevadea.Framework.Scening;
using Hevadea.Framework.UI;
using Hevadea.Framework.UI.Containers;
using Hevadea.Framework.UI.Widgets;
using Hevadea.Game;
using Hevadea.Game.Storage;
using Hevadea.Scenes.Widgets;
using Microsoft.Xna.Framework;
using System.Threading;

namespace Hevadea.Scenes
{
    public class SceneWorldSaving : Scene
    {

        private Thread _generatorThread;
        private GameSaver _worldSave;
        private Label _progressLabel;
        private ProgressBar _progressBar;

        public SceneWorldSaving(string path, GameManager game, bool GoToMainMenu = false)
        {
            _worldSave = new GameSaver();

            _generatorThread = new Thread(() =>
            {
                Thread.Sleep(1000);
                _worldSave.Save(path, game);

                if (GoToMainMenu)
                {
                    Rise.Scene.Switch(new MainMenu());
                }
                else
                {
                    Rise.Scene.Switch(new SceneGameplay(game));
                }
            });

            _progressLabel = new Label { Text = "Saving world...", Anchor = Anchor.Center, Origine = Anchor.Center, Font = Ressources.FontRomulus, UnitOffset = new Point(0, -24) };
            _progressBar = new ProgressBar { UnitBound = new Rectangle(0, 0, 320, 8), Anchor = Anchor.Center, Origine = Anchor.Center, UnitOffset = new Point(0, 24) };

            Container = new AnchoredContainer
            {
                Childrens = { new WidgetFancyPanel{UnitBound = new Rectangle(0, 0, 420, 128),Padding = new Padding(16), Anchor = Anchor.Center, Origine = Anchor.Center, Content = new AnchoredContainer().AddChild(_progressBar).AddChild(_progressLabel)}}
            };
        }

        public override void OnDraw(GameTime gameTime)
        {

        }

        public override void Load()
        {
            _generatorThread.Start();
        }

        public override void Unload()
        {
        }

        public override void OnUpdate(GameTime gameTime)
        {

            _progressLabel.Text = $"{_worldSave.GetStatus()}";
            _progressBar.Value = _worldSave.GetProgress();

        }

    }
}