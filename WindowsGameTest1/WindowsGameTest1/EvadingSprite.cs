using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WindowsGameTest1
{
    class EvadingSprite : Sprite
    {

        SpriteManager spriteManager;
        //保存精灵管理器的一个引用，以便通过它来获取玩家的位置
        float evasionSpeedModifier;  //决定精灵逃离玩家速度
        int evasionRange;  //决定何时激活躲避算法
        bool evade = false;  //跟踪精灵的状态，默认是非躲避

        //声明类级变量

        public EvadingSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame,
            Point sheetSize, Vector2 speed, string collisionCueName,
            SpriteManager spriteManager, float evasionSpeedModifier,
            int evasionRange, int scoreValue)
            : base(textureImage, position, frameSize, collisionOffset,
            currentFrame, sheetSize, speed, collisionCueName, scoreValue)
        {
            this.spriteManager = spriteManager;
            this.evasionSpeedModifier = evasionSpeedModifier;
            this.evasionRange = evasionRange;

        }
        public EvadingSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame,
            Point sheetSize, Vector2 speed, int millisecondsPerFrame,
            string collisionCueName, SpriteManager spriteManager,
            float evasionSpeedModifier, int evasionRange, int scoreValue)
            : base(textureImage, position, frameSize, collisionOffset,
            currentFrame, sheetSize, speed, millisecondsPerFrame, collisionCueName,
            scoreValue)
        {
            this.spriteManager = spriteManager;
            this.evasionSpeedModifier = evasionSpeedModifier;
            this.evasionRange = evasionRange;
        }

        public override Vector2 direction
        {
            get { return speed; }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += speed;
            //首先沿着direction向量移动精灵

            Vector2 player = spriteManager.GetPlayerPosition();
            if (evade)
            {

                if (player.X < position.X)
                    position.X += Math.Abs(speed.Y);
                else if (player.X > position.X)
                    position.X -= Math.Abs(speed.Y);

                //水平远离玩家

                if (player.Y < position.Y)
                    position.Y += Math.Abs(speed.X);
                else if (player.X > position.X)
                    position.Y -= Math.Abs(speed.X);

                //垂直远离玩家
            }
            else
            {
                if (Vector2.Distance(position, player) < evasionRange)
                {
                    speed *= -evasionSpeedModifier;
                    evade = true;
                }
            }


            base.Update(gameTime, clientBounds);
        }

    }
}
