﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    // タイトルを表すシーン
    class TitleScene : asd.Scene
    {
        // 各種ボタン
        Button newgame = new Button("NEW GAME", asd.Engine.WindowSize.X / 7, asd.Engine.WindowSize.Y / 1.6f, 22, new asd.Color(255, 255, 255, 255));
        Button dataload = new Button("DATA LOAD", asd.Engine.WindowSize.X / 7, asd.Engine.WindowSize.Y / 1.51f, 22, new asd.Color(255, 255, 255, 255));
        Button quickload = new Button("QUICK LOAD", asd.Engine.WindowSize.X / 7, asd.Engine.WindowSize.Y / 1.42f, 22, new asd.Color(255, 255, 255, 255));
        Button conti = new Button("CONTINUE", asd.Engine.WindowSize.X / 7, asd.Engine.WindowSize.Y / 1.34f, 22, new asd.Color(255, 255, 255, 255));
        Button config = new Button("CONFIG", asd.Engine.WindowSize.X / 7, asd.Engine.WindowSize.Y / 1.27f, 22, new asd.Color(255, 255, 255, 255));
        Button extra = new Button("EXTRA", asd.Engine.WindowSize.X / 7, asd.Engine.WindowSize.Y / 1.21f, 22, new asd.Color(255, 255, 255, 255));
        Button exit = new Button("GAME END", asd.Engine.WindowSize.X / 7, asd.Engine.WindowSize.Y / 1.15f, 22, new asd.Color(255, 255, 255, 255));
        List<Button> buttons = new List<Button>();

        // 今のボタンの位置
        int nowbutton = 0;

        //Point cp = Control.PointToClient(new Point(100, 200));

        protected override void OnRegistered()
        {
            // 2Dを表示するレイヤーのインスタンスを生成する
            asd.Layer2D layer = new asd.Layer2D();

            // シーンにレイヤーのインスタンスを追加する
            AddLayer(layer);

            // 背景画像を表示するオブジェクトのインスタンスを追加する
            asd.TextureObject2D background = new asd.TextureObject2D();
            //background.Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Title.png");

            // Listにボタンを入れる
            buttons.Add(newgame);
            buttons.Add(dataload);
            buttons.Add(quickload);
            buttons.Add(conti);
            buttons.Add(config);
            buttons.Add(extra);
            buttons.Add(exit);

            // レイヤーにオブジェクトのインスタンスを追加する
            layer.AddObject(background);

            for (int i = 0; i < buttons.Count; i++) layer.AddObject(buttons[i]);
        }

        protected override void OnUpdated()
        {
            // もし、Zキーを押したら{}内の処理を行う
            if(asd.Engine.Keyboard.GetKeyState(asd.Keys.Z) == asd.ButtonState.Push)
            {
                asd.Engine.ChangeSceneWithTransition(new SelectScene(), new asd.TransitionFade(1.0f, 1.0f));
            }

            // もし、上ボタンが押されていたら、次のmodeにする。
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Up) == asd.ButtonState.Push && 0 < nowbutton)
            {
                nowbutton--;
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)buttons[nowbutton].Position.X, (int)buttons[nowbutton].Position.Y + 10);
            }

            // もし、下ボタンが押されていたら、前のmodeにする。
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Down) == asd.ButtonState.Push && nowbutton < (buttons.Count - 1))
            {
                nowbutton++;
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)buttons[nowbutton].Position.X, (int)buttons[nowbutton].Position.Y + 10);


            }
        }
    }
}