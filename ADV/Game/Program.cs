using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Game
{
	class Program
	{
		[System.STAThread]
		static void Main(string[] args)
		{
            // Altseedを初期化する。
            var option = new asd.EngineOption
            {
                IsFullScreen = true
            };

            int DispX = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int DispY = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            asd.Engine.Initialize("Game", (1280 * DispX) / 1536, (745 * DispY) / 864, option);
            //asd.Engine.Initialize("Game", 1980, 1080, new asd.EngineOption());

            // タイトルのシーンのインスタンスを生成する
            TitleScene scene = new TitleScene();

            // シーンを遷移する
            asd.Engine.ChangeSceneWithTransition(scene, new asd.TransitionFade(0, 1.0f));

            // Altseedのウインドウが閉じられていないか確認する。
            while (asd.Engine.DoEvents())
			{
                // もし、Escキーが押されていたらwhileループを抜ける・
                if (asd.Engine.Keyboard.GetKeyState(asd.Keys.Escape) == asd.ButtonState.Push)
                {
                    break;
                }

                // Altseedを更新する。
                asd.Engine.Update();
			}

			// Altseedの終了処理をする。
			asd.Engine.Terminate();
		}
	}
}
