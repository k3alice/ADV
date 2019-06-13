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
        public asd.Font font;
        public asd.Vector2DI size;
        public bool clickflg = false;
        public bool mouseflg = false;
        
        public Button(string text,float x,float y,int fontsize, asd.Color color)
        {
            font = asd.Engine.Graphics.CreateDynamicFont(string.Empty, fontsize, color, 1, new asd.Color(255, 255, 255, 255));
            Font = font;
            size = font.CalcTextureSize(text, asd.WritingDirection.Horizontal);
            CenterPosition = new asd.Vector2DF(size.X / 2.0f, size.Y / 2.0f);
            Position = new asd.Vector2DF(x, y);
            Text = text;
        }

        protected override void OnUpdate()
        {
            clickflg = false;
            mouseflg = false;

            if (asd.Engine.Mouse.Position.X < (Position.X + size.X / 2) && asd.Engine.Mouse.Position.X > (Position.X - size.X / 2) &&
               asd.Engine.Mouse.Position.Y < (Position.Y + size.Y / 2) && asd.Engine.Mouse.Position.Y > (Position.Y - size.Y / 2))
            {
                Scale = new asd.Vector2DF(1.1f, 1.1f);
                mouseflg = true;
                if (asd.Engine.Mouse.LeftButton.ButtonState == asd.ButtonState.Push || asd.Engine.Keyboard.GetKeyState(asd.Keys.Enter) == asd.ButtonState.Push)
                {
                    Console.WriteLine("click");
                    clickflg = true;
                }
            }
            else
            {
                Font = font;
                Scale = new asd.Vector2DF(1.0f, 1.0f);
            }
        }
    }
}