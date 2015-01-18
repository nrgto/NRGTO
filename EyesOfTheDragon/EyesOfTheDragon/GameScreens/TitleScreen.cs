using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using XRpgLibrary;
using XRpgLibrary.Controls;

namespace EyesOfTheDragon.GameScreens
{
    public class TitleScreen : BaseGameState
    {
        #region Field region

        Texture2D backgroundImage;
        Texture2D titleImage;
        
        //金属框
        PictureBox switchBoxImage;
        //背后的灰色
        PictureBox switchBackImage;
        //选项框
        PictureBox switchImage;

        //开始选项
        LinkLabel startGame;
        //继续选项
        LinkLabel loadGame;
        //设置选项
        LinkLabel options;
        //推出选项
        LinkLabel exitGame;
        //组件初始位置
        float maxItemWidth = 0f;

        int x = 0;
        int timeSpan = 50;
        int time = 0;

        #endregion

        #region Constructor region

        public TitleScreen(Game game, GameStateManager manager)
            : base(game, manager)
        {
        }

        #endregion

        #region XNA Method region

        protected override void LoadContent()
        {
            base.LoadContent();
            ContentManager Content = GameRef.Content;

            backgroundImage = Content.Load<Texture2D>(@"Backgrounds\titleX");
            titleImage = Content.Load<Texture2D>(@"Backgrounds\Title");
            Texture2D switchGUI = Content.Load<Texture2D>(@"GUI\Steel");

            switchImage = new PictureBox(
                                switchGUI,
                                new Rectangle(160, 300, 130, 32),
                                new Rectangle(64, 64, 32, 32));
            ControlManager.Add(switchImage);

            switchBackImage = new PictureBox(
                                switchGUI,
                                new Rectangle(140, 300, 150, 130),
                                new Rectangle(0, 0, 64, 64));
            ControlManager.Add(switchBackImage);
            
            switchBoxImage = new PictureBox(
                                switchGUI,
                                new Rectangle(130, 290, 170, 150),
                                new Rectangle(64, 0, 64, 64));
            ControlManager.Add(switchBoxImage);

            startGame = new LinkLabel();
            startGame.Text = "New Game";
            startGame.Size = startGame.SpriteFont.MeasureString(startGame.Text);
            startGame.Selected += new EventHandler(menuItem_Selected);

            ControlManager.Add(startGame);

            loadGame = new LinkLabel();
            loadGame.Text = "Continue";
            loadGame.Size = loadGame.SpriteFont.MeasureString(loadGame.Text);
            loadGame.Selected += menuItem_Selected;

            ControlManager.Add(loadGame);

            options = new LinkLabel();
            options.Text = "Options";
            options.Size = options.SpriteFont.MeasureString(options.Text);
            options.Selected += menuItem_Selected;

            ControlManager.Add(options);

            exitGame = new LinkLabel();
            exitGame.Text = "Exit Program";
            exitGame.Size = exitGame.SpriteFont.MeasureString(exitGame.Text);
            exitGame.Selected += menuItem_Selected;

            ControlManager.Add(exitGame);

            ControlManager.NextControl();

            ControlManager.FocusChanged += ControlManager_FocusChanged;

            Vector2 position = new Vector2(160, 310);
            foreach (Control c in ControlManager)
            {
                if (c is LinkLabel)
                {
                    if (c.Size.X > maxItemWidth)
                        maxItemWidth = c.Size.X;

                    c.Position = position;
                    position.Y += c.Size.Y - 3;
                }
            }

            ControlManager_FocusChanged(startGame, null);
        }

        void ControlManager_FocusChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            Vector2 position = new Vector2(control.Position.X - 10f, control.Position.Y - 2);
            switchImage.SetPosition(position);
        }

        public override void Update(GameTime gameTime)
        {
            ControlManager.Update(gameTime, PlayerIndex.One);

            time ++;
            if (time == timeSpan)
            {
                time = 0;
                x++;
                if(x > 480)
                {
                    x = 0;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            GameRef.SpriteBatch.Begin();

            base.Draw(gameTime);

            GameRef.SpriteBatch.Draw(
                backgroundImage,
                GameRef.ScreenRectangle,
                new Rectangle(x, 0, 640, 480),
                Color.White);

            GameRef.SpriteBatch.Draw(
                titleImage,
                new Rectangle(0, 0, GameRef.ScreenRectangle.Width, GameRef.ScreenRectangle.Height),
                Color.White);

            ControlManager.Draw(GameRef.SpriteBatch);

            GameRef.SpriteBatch.End();
        }

        #endregion

        #region Title Screen Methods

        private void menuItem_Selected(object sender, EventArgs e)
        {
            Transition(ChangeType.Push, GameRef.StartMenuScreen);
        }

        #endregion
    }
}
