using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Game
{
    class CommScene : asd.Scene
    {
        // 各種フラグ
        bool isSceneChanging = false;
        bool skipflag = false;
        bool autoflag = false;
        int timer = 0;
        asd.TextObject2D state = new asd.TextObject2D();

        // 会話のキャラ絵、テキストのインスタンスを追加する
        asd.TextureObject2D chara = new asd.TextureObject2D();
        asd.TextObject2D text = new asd.TextObject2D();
        string[] texts;
        int linenum = 999;
        int nowline = 0;
        Button choose1 = new Button("", asd.Engine.WindowSize.X / 2, 75, 22, new asd.Color(127, 127, 127, 255));
        Button choose2 = new Button("", asd.Engine.WindowSize.X / 2, 150, 22, new asd.Color(127, 127, 127, 255));
        Button choose3 = new Button("", asd.Engine.WindowSize.X / 2, 225, 22, new asd.Color(127, 127, 127, 255));
        Button choose4 = new Button("", asd.Engine.WindowSize.X / 2, 300, 22, new asd.Color(127, 127, 127, 255));
        List<Button> choose = new List<Button>();
        int choosenum;
        int choosecount;
        int choosemode = 0;

        protected override void OnRegistered()
        {
            // textsにテキストファイルから読み込む
            if (File.Exists("Resources/text.txt"))
            {
                texts = File.ReadAllLines("Resources/text.txt");
                var list = new List<string>();
                list.AddRange(texts);
                list.RemoveAll(s => s == "");
                texts = list.ToArray();
                linenum = texts.Length;
                nowline = 0;
            }
            else Console.WriteLine("baka");

            // 2Dを表示するレイヤーのインスタンスを生成する
            asd.Layer2D layer = new asd.Layer2D();

            // シーンにレイヤーのインスタンスを追加する
            AddLayer(layer);

            // フラグ初期化
            autoflag = false;
            skipflag = false;
            state.Font = asd.Engine.Graphics.CreateDynamicFont(string.Empty, 35, new asd.Color(255, 255, 255, 255), 1, new asd.Color(255, 255, 255, 255));
            state.Position = new asd.Vector2DF(520, 350);
            state.Scale = new asd.Vector2DF(1, 1);
            state.Text = "";
            layer.AddObject(state);

            // キャラの初期設定
            chara.Position = new asd.Vector2DF(50, 400);
            layer.AddObject(chara);
            //layer.RemoveObject(chara);

            // レイヤーにテキストのインスタンスを追加する
            text.Font = asd.Engine.Graphics.CreateDynamicFont(string.Empty, 25, new asd.Color(255, 255, 255, 255), 1, new asd.Color(255, 255, 255, 255));
            text.Position = new asd.Vector2DF(96, 350);
            text.Scale = new asd.Vector2DF(1, 1);
            text.Text = texts[0];
            layer.AddObject(text);

            // 選択肢系初期化
            choosenum = 0;
            choosecount = 0;
            choosemode = 0;
            choose.Add(choose1);
            choose.Add(choose2);
            choose.Add(choose3);
            choose.Add(choose4);

            // レイヤーに選択肢のインスタンスを追加する
            for (int i = 0; i < 4; i++) layer.AddObject(choose[i]);
        }

        protected override void OnUpdated()
        {
            // もしシーンが変更中でなく、会話文が終わっていたら処理を行う
            if (!isSceneChanging && linenum == nowline)
            {
                // ゲーム画面に遷移する。
                asd.Engine.ChangeSceneWithTransition(new TitleScene(), new asd.TransitionFade(1.0f, 1.0f));

                // シーンを変更中にする
                isSceneChanging = true;
            }

            if (choosemode == 1)
            {
                // もし、上ボタンが押されていたら、上の選択肢にする。
                if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Up) == asd.ButtonState.Push)
                {
                    //choose[choosenum].Font = font;
                    choose[choosenum].Scale = new asd.Vector2DF(1, 1);
                    if (0 < choosenum) choosenum--;
                    choose[choosenum].Scale = new asd.Vector2DF(1.2f, 1.2f);
                }

                // もし、下ボタンが押されていたら、下の選択肢にする。
                if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Down) == asd.ButtonState.Push)
                {
                    //choose[choosenum].Font = font;
                    choose[choosenum].Scale = new asd.Vector2DF(1, 1);
                    if (choosenum < (choosecount - 1)) choosenum++;
                    choose[choosenum].Scale = new asd.Vector2DF(1.2f, 1.2f);
                }
                //choose[choosenum].Font = fontC;
                

                for (int i = 0; i < choose.Count; i++)
                {
                    if (choose[i].flg)
                    {
                        choosenum = i;

                        for (int j = 0; j < choosecount; j++)
                        {
                            choose[j].Text = "";
                        }
                        choosemode = 2;
                    }
                }

                if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Enter) == asd.ButtonState.Push)
                {
                    for (int i = 0; i < choosecount; i++)
                    {
                        choose[i].Text = "";
                    }
                    choosemode = 2;
                }
            }

            // 会話文を次に進める
            if (asd.Engine.Mouse.LeftButton.ButtonState == asd.ButtonState.Push || asd.Engine.Keyboard.GetKeyState(asd.Keys.Enter) == asd.ButtonState.Push)
            {
                if (nowline == 0) nowline++;
                if (nowline < linenum && choosemode == 0)
                {
                    Next();

                }
                if (nowline < linenum && choosemode == 2)
                {
                    while (texts[nowline] != choosenum.ToString()) nowline++;
                    nowline++;
                    Next();
                    choosemode = 0;
                }
                nowline++;
            }

            // スキップ機能
            // 未読
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.LeftControl) == asd.ButtonState.Hold && (nowline < linenum))
            {
                timer++;
                if(timer > 15)
                {                    
                    Next();
                    nowline++;
                    timer = 0;
                }
            }
            // 既読
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.D) == asd.ButtonState.Push)
            {
                skipflag = !skipflag;
                autoflag = false;
                if(skipflag) state.Text = "Skip";
                else state.Text = "";
            }
            if (skipflag && (nowline < linenum))
            {
                timer++;
                if(timer > 15)
                {
                    Next();
                    nowline++;
                    timer = 0;
                }
            }

            // オート機能
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.A) == asd.ButtonState.Push)
            {
                autoflag = !autoflag;
                skipflag = false;
                if(autoflag) state.Text = "Auto";
                else state.Text = "";
            }
            if (autoflag && (nowline < linenum))
            {
                timer++;
                if(timer > 180)
                {                    
                    Next();
                    nowline++;
                    timer = 0;
                }
            }

            // 次の選択肢に移動
            if (asd.Engine.Keyboard.GetKeyState(asd.Keys.N) == asd.ButtonState.Push)
            {
                while (texts[nowline][0] != '[') nowline++;
                Next();
            }

        }

        string NewLine(string str)
        {
            var list = new List<string>();
            int count = 15;
            int length = (int)Math.Ceiling((double)str.Length / count);
            var newline = "";

            for (int i = 0; i < length; i++)
            {
                int start = count * i;
                if (str.Length <= start)
                {
                    break;
                }
                if (str.Length < start + count)
                {
                    list.Add(str.Substring(start));
                }
                else
                {
                    list.Add(str.Substring(start, count));
                }
                newline += (list[i] + "\n");
            }
            return newline;
        }

        void Next()
        {
            if (choosemode == 1) return;
            char c1 = texts[nowline][0];
            switch (c1)
            {
                case '[': // 選択肢
                    string[] arr = texts[nowline].Split('/');
                    choosecount = arr.Length;
                    for (int i = 0; i < choosecount; i++)
                    {
                        choose[i].Text = arr[i];
                        choose[i].size = choose[i].font.CalcTextureSize(arr[i], asd.WritingDirection.Horizontal);
                        choose[i].CenterPosition = new asd.Vector2DF(choose[i].size.X / 2.0f, choose[i].size.Y / 2.0f);
                    }
                    choosenum = 0;
                    choosemode = 1;
                    break;
                case ']': // 選択肢終了
                    while (texts[nowline] != "@") nowline++;
                    nowline++;
                    Next();
                    break;
                case 'C': // キャラ絵選択
                    string[] str = texts[nowline].Split(' ');
                    chara.Texture = asd.Engine.Graphics.CreateTexture2D("Resources/" + str[1]);
                    chara.CenterPosition = new asd.Vector2DF(chara.Texture.Size.X / 2.0f, chara.Texture.Size.Y / 2.0f);
                    break;
                default: // それ以外
                    text.Text = NewLine(texts[nowline]);
                    //text.Text = texts[nowline];
                    break;
            }
        }
    }
}
