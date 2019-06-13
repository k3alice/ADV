using System;
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
        Button newgame = new Button("NEW GAME", asd.Engine.WindowSize.X / 7, asd.Engine.WindowSize.Y * 0.625f, 22, new asd.Color(255, 255, 255, 255));
        Button dataload = new Button("DATA LOAD", asd.Engine.WindowSize.X / 7, asd.Engine.WindowSize.Y * 0.662f, 22, new asd.Color(255, 255, 255, 255));
        Button quickload = new Button("QUICK LOAD", asd.Engine.WindowSize.X / 7, asd.Engine.WindowSize.Y * 0.699f, 22, new asd.Color(255, 255, 255, 255));
        Button conti = new Button("CONTINUE", asd.Engine.WindowSize.X / 7, asd.Engine.WindowSize.Y * 0.736f, 22, new asd.Color(255, 255, 255, 255));
        Button config = new Button("CONFIG", asd.Engine.WindowSize.X / 7, asd.Engine.WindowSize.Y * 0.773f, 22, new asd.Color(255, 255, 255, 255));
        Button extra = new Button("EXTRA", asd.Engine.WindowSize.X / 7, asd.Engine.WindowSize.Y * 0.810f, 22, new asd.Color(255, 255, 255, 255));
        Button exit = new Button("GAME END", asd.Engine.WindowSize.X / 7, asd.Engine.WindowSize.Y * 0.847f, 22, new asd.Color(255, 255, 255, 255));
        List<Button> buttons = new List<Button>();

        // 今のボタンの位置
        int nowbutton = 0;

        // 2Dを表示するレイヤーのインスタンスを生成する
        asd.Layer2D layer = new asd.Layer2D();

        // 確認フォーム
        GameManager manager = new GameManager();

        protected override void OnRegistered()
        {

            // シーンにレイヤーのインスタンスを追加する
            AddLayer(layer);
            AddLayer(manager.confirmation);

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
            // もし、上ボタンが押されていたら、上のボタンにカーソルを合わせる。
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Up) == asd.ButtonState.Push && 0 < nowbutton)
            {
                nowbutton--;
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)buttons[nowbutton].Position.X, (int)buttons[nowbutton].Position.Y);
            }

            // もし、下ボタンが押されていたら、下のボタンにカーソルを合わせる。
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Down) == asd.ButtonState.Push && nowbutton < (buttons.Count - 1))
            {
                nowbutton++;
                System.Windows.Forms.Cursor.Position = new System.Drawing.Point((int)buttons[nowbutton].Position.X, (int)buttons[nowbutton].Position.Y);
            }

            // もし、ボタンにカーソルが当たっていたら色を変える。
            for (int i = 0; i < buttons.Count; i++)
            {
                if(buttons[i].mouseflg) buttons[i].Font = asd.Engine.Graphics.CreateDynamicFont(string.Empty, 22, new asd.Color(230,146,171,255), 1, new asd.Color(230, 146, 171, 255));
            }

            //  EXITがクリックされたら確認ウィンドウを出す(文字をセットする)
            if (exit.clickflg)
            {
                manager.conftext.Text = "ゲームを終了しますか？";
                manager.size = manager.font.CalcTextureSize("ゲームを終了しますか？", asd.WritingDirection.Horizontal);
                manager.conftext.CenterPosition = new asd.Vector2DF(manager.size.X / 2.0f, manager.size.Y / 2.0f);
                manager.order = Order.quit;
                manager.yes.Text = "YES";
                manager.no.Text = "NO";

            }

            // NEWGAMEがクリックされたらゲーム開始
            if (newgame.clickflg)
            {
                asd.Engine.ChangeSceneWithTransition(new CommScene(), new asd.TransitionFade(1.0f, 1.0f));
            }

            // 確認ウィンドウのYESが押されたとき
            if (manager.yes.clickflg)
            {
                switch (manager.order)
                {
                    case Order.quit: // exitならゲーム終了
                        asd.Engine.Terminate();
                        break;
                }
            }

            if (manager.no.clickflg)
            {
                manager.conftext.Text = "";
                manager.yes.Text = "";
                manager.no.Text = "";
            }
        }
    }
}