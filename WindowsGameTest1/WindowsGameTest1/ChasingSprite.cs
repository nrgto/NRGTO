using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace WindowsGameTest1
{
    class ChasingSprite : Sprite
    {

        SpriteManager spriteManager;//保存精灵管理器的一个引用，以便通过它来获取玩家的位置
        //声明类级变量

        public ChasingSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame,
            Point sheetSize, Vector2 speed, string collisionCueName,
            SpriteManager spriteManager, int scoreValue)
            : base(textureImage, position, frameSize, collisionOffset,
            currentFrame, sheetSize, speed, collisionCueName, scoreValue)
        {
            this.spriteManager = spriteManager;
        }
        public ChasingSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame,
            Point sheetSize, Vector2 speed, int millisecondsPerFrame,
            string collisionCueName, SpriteManager spriteManager, int scoreValue)
            : base(textureImage, position, frameSize, collisionOffset,
            currentFrame, sheetSize, speed, millisecondsPerFrame, collisionCueName,
            scoreValue)
        {
            this.spriteManager = spriteManager;
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

            if (speed.X == 0)
            {
                if (player.X < position.X)
                    position.X -= Math.Abs(speed.Y);
                else if (player.X > position.X)
                    position.X += Math.Abs(speed.Y);
            }
            //如果精灵垂直移动，就水平追逐玩家
            if (speed.Y == 0)
            {
                if (player.Y < position.Y)
                    position.Y -= Math.Abs(speed.X);
                else if (player.X > position.X)
                    position.Y += Math.Abs(speed.X);
            }
            //如果精灵水平移动，就垂直追逐玩家

            //float speedVal = Math.Max(Math.Abs(speed.X), Math.Abs(speed.Y));
            ////比较两者绝对值，返回大的那个

            //if (player.X < position.X)
            //    position.X -= speedVal;
            //else if (player.X > position.X)
            //    position.X += speedVal;
            //if (player.Y < position.Y)
            //    position.Y -= speedVal;
            //else if (player.Y > position.Y)
            //    position.Y += speedVal;

            base.Update(gameTime, clientBounds);
        }

    }
}
