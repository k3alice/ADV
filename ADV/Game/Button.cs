using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Game
{
    class Button : asd.TextObject2D
    {
        asd.Font font = asd.Engine.Graphics.CreateDynamicFont(string.Empty, 35, new asd.Color(255, 0, 0, 255), 1, new asd.Color(255, 255, 255, 255));
        asd.Vector2DI size;
        
        public Button(string s,int x,int y)
        {
            Font = font;
            size = font.CalcTextureSize(s, asd.WritingDirection.Horizontal);
            CenterPosition = new asd.Vector2DF(size.X / 2.0f, size.Y / 2.0f);
            Position = new asd.Vector2DF(x, y);
            Text = s;
        }

        protected override void OnUpdate()
        {
            if(asd.Engine.Mouse.Position.X < (Position.X + size.X / 2) && asd.Engine.Mouse.Position.X > (Position.X - size.X / 2) &&
               asd.Engine.Mouse.Position.Y < (Position.Y + size.Y / 2) && asd.Engine.Mouse.Position.Y > (Position.Y - size.Y / 2))
            {
                //OnClick();
                Scale = new asd.Vector2DF(1.1f, 1.1f);
                if (asd.Engine.Mouse.LeftButton.ButtonState == asd.ButtonState.Push)
                {
                    Console.WriteLine("click");
                }
            }
            else Scale = new asd.Vector2DF(1.0f, 1.0f);

        }

        /*public virtual void OnClick()
        {

        }*/
    }
}