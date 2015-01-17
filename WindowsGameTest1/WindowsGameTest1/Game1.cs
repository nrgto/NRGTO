using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace WindowsGameTest1
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Texture2D dog1Texture;
        //Point dog1FrameSize = new Point(32,32);
        //Point dog1CurrentFrame = new Point(0,0);
        //Point dog1SheetSize = new Point(3,4);
        //int dog1TimeSinceLastFrame = 0;
        //int dog1MillisecondsPerFrame = 50;
        //Texture2D cat1Texture;
        //Point cat1FrameSize = new Point(32, 32);
        //Point cat1CurrentFrame = new Point(0, 0);
        //Point cat1SheetSize = new Point(3, 4);
        //int cat1TimeSinceLastFrame = 0;
        //int cat1MillisecondsPerFrame = 50;
        //Vector2 dog1Position = Vector2.Zero;
        //const float dog1Speed = 6;
        //MouseState prevMouseState;
        //Vector2 cat1Position = new Vector2(100, 100);

        //int dog1CollisionRectoffect = 4;
        //int cat1CollisionRectoffect = 4;
        //碰撞偏移变量
        SpriteManager spriteManager;

        AudioEngine audioEngine;
        //声音引擎，该对象在创建WaveBank和SoundBank对象是使用
        WaveBank waveBank;
        SoundBank soundBank;
        //上述两个对象分别对应于XACT文件的Wave Bank 和 Sound Bank 区域
        Cue trackCue;
        //Cue对象用于将单独的cue从Sound Bank中取出以便播放哪些cue
        public Random rnd { get; private set; }

        int currentScore = 0;
        //当前游戏总得分

        SpriteFont scoreFont;


        //声明类级变量

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            //this.IsMouseVisible = true;//鼠标输出允许
            //TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 16);
            rnd = new Random();
            //初始化Random对象
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            //窗口分辨率

        }
        //game1构造方法
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        //protected bool Collide1()
        //{
        //    Rectangle dog1Rect = new Rectangle((int)dog1Position.X + dog1CollisionRectoffect, (int)dog1Position.Y + dog1CollisionRectoffect, dog1FrameSize.X - (dog1CollisionRectoffect*2), dog1FrameSize.Y - (dog1CollisionRectoffect*2));
        //    Rectangle cat1Rect = new Rectangle((int)cat1Position.X + cat1CollisionRectoffect, (int)cat1Position.Y + cat1CollisionRectoffect, cat1FrameSize.X - (cat1CollisionRectoffect*2), cat1FrameSize.Y - (cat1CollisionRectoffect*2));
        //    return dog1Rect.Intersects(cat1Rect);
        //}
        //dog1和cat1碰撞检测

        public void PlayCue(string cueName)
        {
            soundBank.PlayCue(cueName);
        }
        //在SpriteManager类中调用

        protected override void Initialize()
        {
            spriteManager = new SpriteManager(this);
            Components.Add(spriteManager);
            //实例化spriteManager对象，向构造器传递对Game1类的引用（this），
            //并将对象添加到Game1类的组件列表中
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            scoreFont = Content.Load<SpriteFont>(@"Fonts\Score");

            spriteBatch = new SpriteBatch(GraphicsDevice);

            audioEngine = new AudioEngine(@"Content\Audio\GameAudio.xgs");
            waveBank = new WaveBank(audioEngine, @"Content\Audio\Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content\Audio\Sound Bank.xsb");
            //内容管道对待声音文件不是像以前一样用Content.Load方法，而是用一个较传统
            //的构造器调用来实例化每个对象。

            trackCue = soundBank.GetCue("track");
            trackCue.Play();
            //播放背景音乐track

            soundBank.PlayCue("start");
            //播放启动音效

            //dog1Texture = Content.Load<Texture2D>(@"image\Animal");
            //cat1Texture = Content.Load<Texture2D>(@"image\Animal");
            //加载
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            //dog1TimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            //if (dog1TimeSinceLastFrame >= dog1MillisecondsPerFrame)
            //{
            //    dog1TimeSinceLastFrame -= dog1MillisecondsPerFrame;
            //    ++dog1CurrentFrame.X;
            //    if (dog1CurrentFrame.X >= dog1SheetSize.X)
            //    {
            //        dog1CurrentFrame.X = 0;
            //        ++dog1CurrentFrame.Y;
            //        if (dog1CurrentFrame.Y >= dog1SheetSize.Y)
            //            dog1CurrentFrame.Y = 0;
            //    }
            //}
            //cat1TimeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            //if (cat1TimeSinceLastFrame >= cat1MillisecondsPerFrame)
            //{
            //    cat1TimeSinceLastFrame -= cat1MillisecondsPerFrame;
            //    ++cat1CurrentFrame.X;
            //    if (cat1CurrentFrame.X >= cat1SheetSize.X)
            //    {
            //        cat1CurrentFrame.X = 0;
            //        ++cat1CurrentFrame.Y;
            //        if (cat1CurrentFrame.Y >= cat1SheetSize.Y)
            //            cat1CurrentFrame.Y = 0;
            //    }
            //}
            ////遍历帧
            //KeyboardState keyboardState = Keyboard.GetState();
            //if (keyboardState.IsKeyDown(Keys.Left))
            //    dog1Position.X -= dog1Speed;
            //if (keyboardState.IsKeyDown(Keys.Right))
            //    dog1Position.X += dog1Speed;
            //if (keyboardState.IsKeyDown(Keys.Up))
            //    dog1Position.Y -= dog1Speed;
            //if (keyboardState.IsKeyDown(Keys.Down))
            //    dog1Position.Y += dog1Speed;
            ////键盘输入
            //MouseState mouseState = Mouse.GetState();
            //if (mouseState.X != prevMouseState.X || mouseState.Y != prevMouseState.Y)
            //    dog1Position = new Vector2(mouseState.X, mouseState.Y);
            //prevMouseState = mouseState;
            ////鼠标输入
            //if (dog1Position.X < 0)
            //    dog1Position.X = 0;
            //if (dog1Position.Y < 0)
            //    dog1Position.Y = 0;
            //if (dog1Position.X > Window.ClientBounds.Width - dog1FrameSize.X)
            //    dog1Position.X = Window.ClientBounds.Width - dog1FrameSize.X;
            //if (dog1Position.Y > Window.ClientBounds.Height - dog1FrameSize.Y)
            //    dog1Position.Y = Window.ClientBounds.Height - dog1FrameSize.Y;
            //// 不会跑出窗口边缘
            //// TODO: Add your update logic here
            //if (Collide1())
            //    Exit();
            ////如果dog1和cat1碰撞则退出

            audioEngine.Update();
            //使AudioEngine与游戏同步

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.White);

            spriteBatch.Begin();
            spriteBatch.DrawString(scoreFont, "Score:" + currentScore,
                new Vector2(10, 10), Color.DarkBlue, 0, Vector2.Zero,
                1, SpriteEffects.None, 1);
            spriteBatch.End();
            //绘制字体

            //spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);
            //spriteBatch.Draw(dog1Texture,dog1Position,new Rectangle(dog1CurrentFrame.X*dog1FrameSize.X,dog1CurrentFrame.Y*dog1FrameSize.Y,dog1FrameSize.X,dog1FrameSize.Y),Color.White,0,Vector2.Zero,1,SpriteEffects.None,0);
            //spriteBatch.Draw(cat1Texture,cat1Position, new Rectangle(cat1CurrentFrame.X * cat1FrameSize.X + 96, cat1CurrentFrame.Y * cat1FrameSize.Y, cat1FrameSize.X, cat1FrameSize.Y), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            //spriteBatch.End();
            //绘制
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
