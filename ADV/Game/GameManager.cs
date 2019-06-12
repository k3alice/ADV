﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class GameManager
    {
        // 確認用ウインドウ
        public asd.Font font;
        public asd.Vector2DI size;
        public asd.Layer2D confirmation = new asd.Layer2D();
        public asd.TextObject2D conftext = new asd.TextObject2D();     
        public Button yes = new Button("YES", asd.Engine.WindowSize.X * 0.4f, asd.Engine.WindowSize.Y * 0.2f, 35, new asd.Color(255, 0, 0, 255));
        public Button no = new Button("NO", asd.Engine.WindowSize.X  * 0.6f, asd.Engine.WindowSize.Y * 0.2f, 35, new asd.Color(255, 0, 0, 255));

        public GameManager()
        {
            font = asd.Engine.Graphics.CreateDynamicFont(string.Empty, 35, new asd.Color(255, 255, 255, 255), 1, new asd.Color(255, 255, 255, 255));
            conftext.Font = font;
            //size = font.CalcTextureSize(text, asd.WritingDirection.Horizontal);
            //conftext.CenterPosition = new asd.Vector2DF(size.X / 2.0f, size.Y / 2.0f);
            conftext.Position = new asd.Vector2DF(asd.Engine.WindowSize.X / 2.0f, asd.Engine.WindowSize.Y * 0.35f);
            //conftext.Text = text;

            confirmation.AddObject(conftext);
            confirmation.AddObject(yes);
            confirmation.AddObject(no);
        }
    }
}
