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


namespace WindowsGameTest1
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>


    public class SpriteManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        UserControlledSprite player;
        List<Sprite> spriteList = new List<Sprite>();
        int enemySpawnMinMilliseconds = 1000; //生成敌人最小毫秒数
        int enemySpawnMaxMilliseconds = 2000; //生成敌人最大毫秒数
        int enemyMinSpeed = 2;  //敌人最小速度
        int enemyMaxSpeed = 6;  //敌人最大速度
        int nextSpawnTime = 0;  //下一个敌人生成时间
        int likelihoodAutomated = 75;
        int likelihoodChasing = 20;
        int likelihoodEvading = 5;
        //三个代表每种精灵生成概率
        //声明类级变量


        public SpriteManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }



        public Vector2 GetPlayerPosition()
        {
            return player.GetPosition;
        }
        //返回玩家对象的位置

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            ResetSpawnTime();
            //在游戏启动时初始化变量

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            //初始化sprite对象

            player = new UserControlledSprite(
                Game.Content.Load<Texture2D>(@"Image/Animal"),
                Vector2.Zero, new Point(32, 32), 4, new Point(0, 0),
                new Point(3, 4), new Vector2(6, 6));
            //初始化player对象

            //spriteList.Add(new AutomatedSprite(
            //    Game.Content.Load<Texture2D>(@"Image/Animal"),
            //    new Vector2(250, 250), new Point(32, 32), 4, new Point(0, 0),
            //    new Point(3, 4), Vector2.Zero,"skullcollision"));
            //spriteList.Add(new AutomatedSprite(
            //    Game.Content.Load<Texture2D>(@"Image/Animal"),
            //    new Vector2(350, 350), new Point(32, 32), 4, new Point(0, 0),
            //    new Point(3, 4), Vector2.Zero, "skullcollision"));
            //spriteList.Add(new AutomatedSprite(
            //    Game.Content.Load<Texture2D>(@"Image/Animal"),
            //    new Vector2(450, 450), new Point(32, 32), 4, new Point(0, 0),
            //    new Point(3, 4), Vector2.Zero, "skullcollision"));
            //spriteList.Add(new AutomatedSprite(
            //    Game.Content.Load<Texture2D>(@"Image/Animal"),
            //    new Vector2(550, 550), new Point(32, 32), 4, new Point(0, 0),
            //    new Point(3, 4), Vector2.Zero, "skullcollision"));
            ////添加了4个自动精灵

            base.LoadContent();
        }



        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            nextSpawnTime -= gameTime.ElapsedGameTime.Milliseconds;
            if (nextSpawnTime < 0)
            {
                SpawnEnemy();
                ResetSpawnTime();
            }
                //重置生成计时器

                player.Update(gameTime, Game.Window.ClientBounds);
                //更新player精灵

                //foreach (Sprite s in spriteList)
                //{
                //    s.Update(gameTime, Game.Window.ClientBounds);
                //    //更新精灵列表中的所有精灵

                //    if (s.collisionRect.Intersects(player.collisionRect))
                //        Game.Exit();
                //    //检测精灵列表中的所有精灵与玩家精灵的碰撞
                //}
                for (int i = 0; i < spriteList.Count; ++i)
                {
                    Sprite s = spriteList[i];
                    s.Update(gameTime, Game.Window.ClientBounds);
                    //更新精灵列表中的所有精灵

                    if (s.collisionRect.Intersects(player.collisionRect))
                    {
                        //检查碰撞
                        if (s.collisionCueName != null)
                            ((Game1)Game).PlayCue(s.collisionCueName);
                        //播放碰撞声音
                        spriteList.RemoveAt(i);
                        --i;
                        //删除碰撞到的精灵
                    }
                    if (s.IsOutOfBounds(Game.Window.ClientBounds))
                    {
                        spriteList.RemoveAt(i);
                        --i;
                        //如果对象跑到窗口外边就删除
                    }

                    base.Update(gameTime);
                }
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            player.Draw(gameTime, spriteBatch);
            //绘制player精灵

            foreach (Sprite s in spriteList)
                s.Draw(gameTime, spriteBatch);
            //绘制精灵列表中的所有精灵

            spriteBatch.End();


            base.Draw(gameTime);
        }
        private void ResetSpawnTime()
        {
            nextSpawnTime = ((Game1)Game).rnd.Next
                (enemySpawnMinMilliseconds,
                enemySpawnMaxMilliseconds);
        }
        //将变量初始化为下一个生成时间
        
        private void SpawnEnemy()
        {
            Vector2 speed = Vector2.Zero;
            Vector2 position = Vector2.Zero;
            Point frameSize = new Point(32, 32);//默认帧大小
            switch (((Game1)Game).rnd.Next(4))
            {
                case 0: //从左飞右
                    position = new Vector2(
                        -frameSize.X, ((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight
                        - frameSize.Y));
                    speed = new Vector2(((Game1)Game).rnd.Next(
                        enemyMinSpeed,
                        enemyMaxSpeed), 0);
                    break;
                case 1: //从右飞左
                    position = new Vector2(
                        Game.GraphicsDevice.PresentationParameters.BackBufferWidth, ((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight
                        - frameSize.Y));
                    speed = new Vector2(-((Game1)Game).rnd.Next(
                        enemyMinSpeed,
                        enemyMaxSpeed), 0);
                    break;
                case 2: //从下飞上
                    position = new Vector2(
                        ((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferWidth
                        - frameSize.X),
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight);
                    speed = new Vector2(0, -((Game1)Game).rnd.Next(
                        enemyMinSpeed,
                        enemyMaxSpeed));
                    break;
                case 3: //从上飞下
                    position = new Vector2(
                        ((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferWidth
                        - frameSize.X), -frameSize.Y);
                    speed = new Vector2(0, ((Game1)Game).rnd.Next(
                        enemyMinSpeed,
                        enemyMaxSpeed));
                    break;
            }
            //为即将添加的精灵创建速度和位置变量
            //随机选择在屏幕的那一边生成敌人
            //然后在那一边选择一个位置
            //然后随机选择一个速度
            //spriteList.Add(
            //    new AutomatedSprite(Game.Content.Load<Texture2D>(@"image\Animal"),
            //        position, new Point(32, 32), 4, new Point(0, 0),
            //        new Point(3, 4), speed, "skullcollision")
            //        );
            spriteList.Add(
                new EvadingSprite (Game.Content.Load<Texture2D>(@"image\Animal"),
                    position, new Point(32, 32), 4, new Point(0, 0),
                    new Point(3, 4), speed, "skullcollision",this,.75f, 150, 0)
                    );

            //创建精灵

            int random = ((Game1)Game).rnd.Next(100);
            //获取随机数
            if (random < likelihoodAutomated)
            {
                spriteList.Add(
                new AutomatedSprite(Game.Content.Load<Texture2D>(@"image\Animal"),
                    position, new Point(32, 32), 4, new Point(0, 0),
                    new Point(3, 4), speed, "skullcollision", 0));
            }
            //创建AutomatedSprite
            else if (random < likelihoodAutomated + likelihoodChasing)
            {
                spriteList.Add(
                new ChasingSprite(Game.Content.Load<Texture2D>(@"image\Animal"),
                    position, new Point(32, 32), 4, new Point(0, 0),
                    new Point(3, 4), speed, "skullcollision", this, 0));
            }
            //创建ChasingSprite
            else
            {
                spriteList.Add(
                new EvadingSprite(Game.Content.Load<Texture2D>(@"image\Animal"),
                    position, new Point(32, 32), 4, new Point(0, 0),
                    new Point(3, 4), speed, "skullcollision", this, .75f, 150, 0));
            }
            //创建EvadingSprite




        }

    }
}
