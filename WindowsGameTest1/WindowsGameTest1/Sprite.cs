using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGameTest1
{
    abstract class Sprite
    {
        Texture2D textureImage;    //要绘制的精灵
        protected Point frameSize;    //精灵表中每一帧的大小
        Point currentFrame;    //当前帧在精灵表中的索引
        Point sheetSize;    //精灵表的列数、行数
        int collisionOffset;    //碰撞检测偏移量
        int timeSinceLastFrame = 0;    //上一帧绘制后经过的毫秒数
        int millisecondsPerFrame;    //帧与帧之间要等待的毫秒数
        const int defaultMillisecondsPerFrame = 16;    //默认动画速度常量
        protected Vector2 speed;    //精灵在X方向和Y方向的移动速度
        protected Vector2 position;    //精灵的绘制位置
        
        public string collisionCueName { get; private set; }
        //自动实现属性，可以在声明变量时就为它创建访问器（方法）。

        public int scoreValue { get; protected set; }
        //代表精灵的分值

        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            string collisionCueName, int scoreValue)
            : this(textureImage, position, frameSize, collisionOffset,
            currentFrame, sheetSize, speed, defaultMillisecondsPerFrame,
            collisionCueName, scoreValue)
        {
        }
        public Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame,string collisionCueName, int scoreValue)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.speed = speed;
            this.millisecondsPerFrame = millisecondsPerFrame;
            this.collisionCueName = collisionCueName;
            this.scoreValue = scoreValue;
        }

        public abstract Vector2 direction
        {
            get;
        }
        //添加一个抽象属性来表示精灵的移动方向

        public Vector2 GetPosition
        {
            get { return position; }
        }
        //创建个公共访问器来返回精灵对象的位置



        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            //virtual标记为虚方法，从而允许子类中重写（override)该方法
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame = 0;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }
            }
        }
        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage, position,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                    Color.White, 0, Vector2.Zero,
                    1f, SpriteEffects.None, 0);
        }
        public Rectangle collisionRect
        {
            get
            {
                return new Rectangle(
                    (int)position.X + collisionOffset,
                    (int)position.Y + collisionOffset,
                    frameSize.X - (collisionOffset * 2),
                    frameSize.Y - (collisionOffset * 2));
            }
        }
        //添加一个属性返回碰撞检测矩形
        public bool IsOutOfBounds(Rectangle clientRect)
        {
            if (position.X < -frameSize.X ||
                position.X > clientRect.Width ||
                position.Y < -frameSize.Y ||
                position.Y > clientRect.Height)
            {
                return true;
            }
            return false;
        }
        //接受代表窗口矩形的一个Rectangle，返回true或false来表示精灵是否跑出窗外


    }
}
