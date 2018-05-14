﻿using Aiv.Fast2D;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSMc
{
    class Bar:GameObject
    {
        protected float value;
        protected float barWidth;
        protected Sprite frame;
        protected Texture frameTexture;

        public float MaxValue { get; set; }

        public override Vector2 Position { get => base.Position; set { SetXPosition(value.X); SetYPosition(value.Y); } }
        public override int Width { get { return (int)frame.Width; } }
        public override int Height { get { return (int)frame.Height; } }

        public Vector2 BarOffset
        {
            set
            {
                sprite.position = frame.position + value;
            }
        }

        public virtual void SetValue(float newValue)
        {
            value = newValue;
            ResizeBar();
        }

        public Bar(Vector2 position, string textureName= "loadingBar_bar", float maxValue=100, int height = 0) : base(position,textureName,DrawManager.Layer.GUI)
        {
            sprite.pivot = Vector2.Zero;
            value = MaxValue = maxValue;
            //sprite.scale = new Vector2(texture.Width, 1);

            frameTexture = GfxManager.GetTexture("loadingBar_frame");
            frame = new Sprite(Game.PixelToUnit(frameTexture.Width), Game.PixelToUnit(frameTexture.Height));
            frame.position = position;

            barWidth = Game.PixelToUnit(frameTexture.Width);
        }

        protected virtual void ResizeBar()
        {
            float scale = value / MaxValue;
            barWidth = Game.PixelToUnit(frameTexture.Width) * scale;
            sprite.scale = new Vector2(scale, 1);
            //sprite.SetMultiplyTint((1-scale)*1f,scale*0.6f,scale*0.95f, 1);
            sprite.SetAdditiveTint(1-scale, scale-1,  scale-1, 0);
        }

        public override void Draw()
        {
            if (IsActive)
            {
                frame.DrawTexture(frameTexture);
                sprite.DrawTexture(texture, 0, 0, (int)Game.PixelToUnit((int)barWidth), (int)Game.PixelToUnit((int)sprite.Height));
            }
        }

        //public override void SetCamera(Camera camera)
        //{
        //    base.SetCamera(camera);
        //    frame.Camera = camera;
        //}

        public void SetXPosition(float newX)
        {
            float xOffSet = newX - frame.position.X;
            frame.position.X = newX;
            sprite.position.X += xOffSet;
        }

        public void SetYPosition(float newY)
        {
            float yOffSet = newY - frame.position.Y;
            frame.position.Y = newY;
            sprite.position.Y += yOffSet;
        }
    }
}
