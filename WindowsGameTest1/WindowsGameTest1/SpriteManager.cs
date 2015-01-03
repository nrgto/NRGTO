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
        int enemySpawnMinMilliseconds = 1000; //���ɵ�����С������
        int enemySpawnMaxMilliseconds = 2000; //���ɵ�����������
        int enemyMinSpeed = 2;  //������С�ٶ�
        int enemyMaxSpeed = 6;  //��������ٶ�
        int nextSpawnTime = 0;  //��һ����������ʱ��
        int likelihoodAutomated = 75;
        int likelihoodChasing = 20;
        int likelihoodEvading = 5;
        //��������ÿ�־������ɸ���
        //�����༶����


        public SpriteManager(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }



        public Vector2 GetPlayerPosition()
        {
            return player.GetPosition;
        }
        //������Ҷ����λ��

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            ResetSpawnTime();
            //����Ϸ����ʱ��ʼ������

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            //��ʼ��sprite����

            player = new UserControlledSprite(
                Game.Content.Load<Texture2D>(@"Image/Animal"),
                Vector2.Zero, new Point(32, 32), 4, new Point(0, 0),
                new Point(3, 4), new Vector2(6, 6));
            //��ʼ��player����

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
            ////�����4���Զ�����

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
                //�������ɼ�ʱ��

                player.Update(gameTime, Game.Window.ClientBounds);
                //����player����

                //foreach (Sprite s in spriteList)
                //{
                //    s.Update(gameTime, Game.Window.ClientBounds);
                //    //���¾����б��е����о���

                //    if (s.collisionRect.Intersects(player.collisionRect))
                //        Game.Exit();
                //    //��⾫���б��е����о�������Ҿ������ײ
                //}
                for (int i = 0; i < spriteList.Count; ++i)
                {
                    Sprite s = spriteList[i];
                    s.Update(gameTime, Game.Window.ClientBounds);
                    //���¾����б��е����о���

                    if (s.collisionRect.Intersects(player.collisionRect))
                    {
                        //�����ײ
                        if (s.collisionCueName != null)
                            ((Game1)Game).PlayCue(s.collisionCueName);
                        //������ײ����
                        spriteList.RemoveAt(i);
                        --i;
                        //ɾ����ײ���ľ���
                    }
                    if (s.IsOutOfBounds(Game.Window.ClientBounds))
                    {
                        spriteList.RemoveAt(i);
                        --i;
                        //��������ܵ�������߾�ɾ��
                    }

                    base.Update(gameTime);
                }
        }
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

            player.Draw(gameTime, spriteBatch);
            //����player����

            foreach (Sprite s in spriteList)
                s.Draw(gameTime, spriteBatch);
            //���ƾ����б��е����о���

            spriteBatch.End();


            base.Draw(gameTime);
        }
        private void ResetSpawnTime()
        {
            nextSpawnTime = ((Game1)Game).rnd.Next
                (enemySpawnMinMilliseconds,
                enemySpawnMaxMilliseconds);
        }
        //��������ʼ��Ϊ��һ������ʱ��
        
        private void SpawnEnemy()
        {
            Vector2 speed = Vector2.Zero;
            Vector2 position = Vector2.Zero;
            Point frameSize = new Point(32, 32);//Ĭ��֡��С
            switch (((Game1)Game).rnd.Next(4))
            {
                case 0: //�������
                    position = new Vector2(
                        -frameSize.X, ((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight
                        - frameSize.Y));
                    speed = new Vector2(((Game1)Game).rnd.Next(
                        enemyMinSpeed,
                        enemyMaxSpeed), 0);
                    break;
                case 1: //���ҷ���
                    position = new Vector2(
                        Game.GraphicsDevice.PresentationParameters.BackBufferWidth, ((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight
                        - frameSize.Y));
                    speed = new Vector2(-((Game1)Game).rnd.Next(
                        enemyMinSpeed,
                        enemyMaxSpeed), 0);
                    break;
                case 2: //���·���
                    position = new Vector2(
                        ((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferWidth
                        - frameSize.X),
                        Game.GraphicsDevice.PresentationParameters.BackBufferHeight);
                    speed = new Vector2(0, -((Game1)Game).rnd.Next(
                        enemyMinSpeed,
                        enemyMaxSpeed));
                    break;
                case 3: //���Ϸ���
                    position = new Vector2(
                        ((Game1)Game).rnd.Next(0,
                        Game.GraphicsDevice.PresentationParameters.BackBufferWidth
                        - frameSize.X), -frameSize.Y);
                    speed = new Vector2(0, ((Game1)Game).rnd.Next(
                        enemyMinSpeed,
                        enemyMaxSpeed));
                    break;
            }
            //Ϊ������ӵľ��鴴���ٶȺ�λ�ñ���
            //���ѡ������Ļ����һ�����ɵ���
            //Ȼ������һ��ѡ��һ��λ��
            //Ȼ�����ѡ��һ���ٶ�
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

            //��������

            int random = ((Game1)Game).rnd.Next(100);
            //��ȡ�����
            if (random < likelihoodAutomated)
            {
                spriteList.Add(
                new AutomatedSprite(Game.Content.Load<Texture2D>(@"image\Animal"),
                    position, new Point(32, 32), 4, new Point(0, 0),
                    new Point(3, 4), speed, "skullcollision", 0));
            }
            //����AutomatedSprite
            else if (random < likelihoodAutomated + likelihoodChasing)
            {
                spriteList.Add(
                new ChasingSprite(Game.Content.Load<Texture2D>(@"image\Animal"),
                    position, new Point(32, 32), 4, new Point(0, 0),
                    new Point(3, 4), speed, "skullcollision", this, 0));
            }
            //����ChasingSprite
            else
            {
                spriteList.Add(
                new EvadingSprite(Game.Content.Load<Texture2D>(@"image\Animal"),
                    position, new Point(32, 32), 4, new Point(0, 0),
                    new Point(3, 4), speed, "skullcollision", this, .75f, 150, 0));
            }
            //����EvadingSprite




        }

    }
}
