using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace WindowsGameTest1
{
    class UserControlledSprite: Sprite
    {
        MouseState prevMouseState;
        //声明类级变量
       
        public override Vector2 direction
        {
            get {
                Vector2 inputDirection = Vector2.Zero;

                if (Keyboard.GetState().IsKeyDown(Keys.Left))
                    inputDirection.X -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                    inputDirection.X += 1;
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    inputDirection.Y -= 1;
                if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    inputDirection.Y += 1;
                //键盘输入

                GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
                if (gamepadState.ThumbSticks.Left.X != 0)
                    inputDirection.X += gamepadState.ThumbSticks.Left.X;
                if (gamepadState.ThumbSticks.Left.Y != 0)
                    inputDirection.Y -= gamepadState.ThumbSticks.Left.Y;
                //手柄输入

                return inputDirection * speed;
                }
            //该属性返回代表移动方向的一个Vector2值乘以基类speed成员的结果
        }

        public UserControlledSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed)
            : base(textureImage, position, frameSize, collisionOffset,
            currentFrame, sheetSize, speed,null,0)
        {
        }
        public UserControlledSprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed, int millisecondsPerFrame)
            : base(textureImage, position, frameSize, collisionOffset,
            currentFrame, sheetSize, speed, millisecondsPerFrame,null,0)
        {
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            position += direction;
            //按照方向移动精灵

            MouseState currMouseState = Mouse.GetState();
            if (currMouseState.X != prevMouseState.X ||
                currMouseState.Y != prevMouseState.Y)
            {
                position = new Vector2(currMouseState.X, currMouseState.Y);
            }
            prevMouseState = currMouseState;
            //如果移动鼠标，将精灵移动到鼠标位置

            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width - frameSize.X)
                position.X = clientBounds.Width - frameSize.X;
            if (position.Y > clientBounds.Height - frameSize.Y)
                position.Y = clientBounds.Height - frameSize.Y;
            //不让精灵跑出窗口

            base.Update(gameTime, clientBounds);
        }
    }
}
