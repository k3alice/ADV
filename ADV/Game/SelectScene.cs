﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class SelectScene : asd.Scene
    {
        // シーンを変更中か
        bool isSceneChanging = false;

        public static string playerimage;

        int mode;
        int charanum;
        int levelnum;

        Button b = new Button("auto",100,100,35, new asd.Color(255, 255, 255, 255));
        Button c = new Button("skip", 200, 100,35,new asd.Color(255, 255, 255, 255));

        // ありすとふみかのインスタンスを追加する
        asd.TextureObject2D alice = new asd.TextureObject2D();
        asd.TextureObject2D humika = new asd.TextureObject2D();
        List<asd.TextureObject2D> chara = new List<asd.TextureObject2D>();
        string[] charaname = { "alice", "humika" };

        // レベルのテキストインスタンスを追加する
        Button easy = new Button("EASY", 106, 400, 35,new asd.Color(255, 0, 0, 255));
        Button normal = new Button("NORMAL", 320, 400, 35, new asd.Color(0, 255, 0, 255));
        Button hard = new Button("HARD", 522, 400, 35, new asd.Color(0, 0, 255, 255));
        List<Button> level = new List<Button>();


        protected override void OnRegistered()
        {
            playerimage = "";
            mode = charanum = levelnum = 0;

            // 2Dを表示するレイヤーのインスタンスを生成する
            asd.Layer2D layer = new asd.Layer2D();

            // シーンにレイヤーのインスタンスを追加する
            AddLayer(layer);

            // 背景画像を表示するオブジェクトのインスタンスを追加する
            asd.TextureObject2D background = new asd.TextureObject2D();
            background.Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Title.png");

            // レイヤーにオブジェクトのインスタンスを追加する
            //layer.AddObject(background);

            // レイヤーにありすのインスタンスを追加する
            chara.Add(alice);
            alice.Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Alice.png");
            alice.CenterPosition = new asd.Vector2DF(alice.Texture.Size.X / 2.0f, alice.Texture.Size.Y / 2.0f);
            alice.Position = new asd.Vector2DF(240, 200);
            layer.AddObject(alice);

            // レイヤーにふみかのインスタンスを追加する
            chara.Add(humika);
            humika.Texture = asd.Engine.Graphics.CreateTexture2D("Resources/Humika.png");
            humika.CenterPosition = new asd.Vector2DF(humika.Texture.Size.X / 2.0f, humika.Texture.Size.Y / 2.0f);
            humika.Position = new asd.Vector2DF(400, 200);
            layer.AddObject(humika);

            // レイヤーにeasyのインスタンスを追加する
            level.Add(easy);
            layer.AddObject(easy);

            // レイヤーにnormalのインスタンスを追加する
            level.Add(normal);
            layer.AddObject(normal);

            // レイヤーにhardのインスタンスを追加する
            level.Add(hard);
            layer.AddObject(hard);
        }

        protected override void OnUpdated()
        {
            // もしシーンが変更中でなく、エンターボタンが押されたらシーン遷移する
            if (!isSceneChanging && asd.Engine.Keyboard.GetKeyState(asd.Keys.Enter) == asd.ButtonState.Push)
            {
                // プレイヤーの画像のパスを渡す
                playerimage = "Resources/" + charaname[charanum] + "_back.png";

                // ゲーム画面に遷移する。
                //asd.Engine.ChangeSceneWithTransition(new GameScene(), new asd.TransitionFade(1.0f, 1.0f));
                asd.Engine.ChangeSceneWithTransition(new CommScene(), new asd.TransitionFade(1.0f, 1.0f));

                // シーンを変更中にする
                isSceneChanging = true;
            }

            // もし、上ボタンが押されていたら、次のmodeにする。
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Up) == asd.ButtonState.Push && mode > 0)
            {
                mode--;
            }

            // もし、下ボタンが押されていたら、前のmodeにする。
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Down) == asd.ButtonState.Push && mode < 1)
            {
                mode++;
            }

            // 選択する項目
            switch (mode)
            {
                case 0: //キャラ選択
                    // もし、右ボタンが押されていたら、右隣のキャラをピックアップする。
                    if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Right) == asd.ButtonState.Push)
                    {
                        chara[charanum].Scale = new asd.Vector2DF(1, 1);
                        if (charanum < (chara.Count - 1)) charanum++;

                    }

                    // もし、左ボタンが押されていたら、左隣のキャラをピックアップする。
                    if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Left) == asd.ButtonState.Push)
                    {
                        chara[charanum].Scale = new asd.Vector2DF(1, 1);
                        if (0 < charanum) charanum--;
                    }
                    chara[charanum].Scale = new asd.Vector2DF(2, 2);
                    break;
                case 1: //難易度選択
                    // もし、右ボタンが押されていたら、右隣の難易度にする。
                    if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Right) == asd.ButtonState.Push)
                    {
                        level[levelnum].Scale = new asd.Vector2DF(1, 1);
                        if (levelnum < (level.Count - 1)) levelnum++;

                    }

                    // もし、左ボタンが押されていたら、左隣の難易度にする。
                    if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Left) == asd.ButtonState.Push)
                    {
                        level[levelnum].Scale = new asd.Vector2DF(1, 1);
                        if (0 < levelnum) levelnum--;
                    }
                    
                    break;
            }

            level[levelnum].Scale = new asd.Vector2DF(1.5f, 1.5f);

            for (int i = 0; i < level.Count; i++)
            {
                if (level[i].clickflg)
                {
                    mode = 1;
                    levelnum = i;
                }
            }

            /*if (asd.Engine.Mouse.Position.X < 320 && asd.Engine.Mouse.LeftButton.ButtonState == asd.ButtonState.Push)
            {
                playerimage = "Resources/Alice_back.png";
            }
            if (asd.Engine.Mouse.Position.X > 320 && asd.Engine.Mouse.LeftButton.ButtonState == asd.ButtonState.Push)
            {
                playerimage = "Resources/Humika_back.png";
            }*/
        }
    }
}
