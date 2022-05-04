using Raylib_cs;
using static Raylib_cs.Raylib;
using hrv.ShellWrapper;
using hrv.Keybinds;
using hrv.Generator;
using System;
using System.Numerics;

namespace HelloWorld
{
    static class Program
    {
        public static void Main()
        {
            Raylib.InitWindow(800, 480, "HRV");

            LCG lcg = new LCG((uint)new Random().Next());
            Color bg = lcg.GenColor();

            Raylib.SetExitKey(0);
            SetTargetFPS(GetMonitorRefreshRate(GetCurrentMonitor()));

            Vector2 pos = new Vector2(12, 12);
            int size = 20;
            Console.WriteLine(pos.X + " " + pos.Y);
            while (!Raylib.WindowShouldClose())
            {
                if(HRVKeybinds.CloseKey())
                {
                    Raylib.CloseWindow();
                }
                if(HRVKeybinds.ScreenShotKey())
                {
                    Raylib.TakeScreenshot("screenshot.png");
                }
                if(IsKeyPressed(KeyboardKey.KEY_SPACE))
                {
                    bg = lcg.GenColor();
                }
                if(IsKeyDown(KeyboardKey.KEY_KP_ADD) || IsKeyDown(KeyboardKey.KEY_EQUAL))
                {
                    size++;
                }
                if(IsKeyDown(KeyboardKey.KEY_KP_SUBTRACT) || IsKeyDown(KeyboardKey.KEY_MINUS))
                {
                    size--;
                    if(size < 1)
                    {
                        size = 1;
                    }
                }
                pos = pos + HRVKeybinds.InputVector();
                Raylib.BeginDrawing();
                Raylib.ClearBackground(bg);

                Raylib.DrawRectangle(
                        (int)(pos.X - (0.5*size)),
                        (int)(pos.Y - (0.5*size)),
                        (int)(size * 10),
                        (int)(size * 4.5), Color.BLACK);

                Raylib.DrawText(
                        $"Hello, {string.Join("\n", new ShellProcess("whoami").getLines().ToArray())}",
                        (int)pos.X,
                        (int)pos.Y,
                        (int)size, Color.GREEN);

                Raylib.DrawText(
                        $"{lcg.Generate(10)}",
                        (int)pos.X,
                        (int)pos.Y+(size),
                        (int)size,
                        lcg.GenColor());

                Raylib.DrawFPS((int)pos.X, (int)pos.Y+(2*size));

                Raylib.EndDrawing();
            }

            Raylib.CloseWindow();
        }
    }
}