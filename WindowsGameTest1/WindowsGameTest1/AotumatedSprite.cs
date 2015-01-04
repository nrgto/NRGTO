using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGameTest1
{
    class AutomatedSprite: Sprite
    {
        public override Vector2 direction
        {
            get { return speed; }
        }

         public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,string collisionCueName,
             int scoreValue)
            : base(textureImage, position, frameSize, collisionOffset,
            currentFrame, sheetSize, speed, collisionCueName, scoreValue)
        {
        }
        public AutomatedSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, int millisecondsPerFrame,
            string collisionCueName, int scoreValue)
            : base(textureImage, position, frameSize, collisionOffset,
            currentFrame, sheetSize, speed, millisecondsPerFrame, collisionCueName, scoreValue)
        {
        }
        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //XXX
            position += direction;
            //位置+速度=新位置
            base.Update(gameTime, clientBounds);
        }
    }
}
