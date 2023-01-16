using System;
using System.Drawing;
using System.Numerics;
using System.Diagnostics;
using Raylib_cs;
using System.Threading;
using System.Timers;
using System.Xml.Linq;
using Color = Raylib_cs.Color;


namespace PixelEditor_Raylib;

static class Program
{
    static int windowWidth = 800;
    static int windowHeight = 800;


    static int pixelAmp = 60;
    static int UIHeight = 160;
    static int UIwidth = 0;
    static int canvasX = UIwidth;
    static int canvasY = UIHeight;
    static int canvasHeight = windowHeight - UIHeight;
    static int canvasWidth = windowWidth;

    const int UI_ELEMENT_OFFSET_Y = 5;
    const int UI_ELEMENT_OFFSET_X = 10;
    const int DEFAULT_BUTTON_WIDTH= 40;
    const int DEFAULT_BUTTON_HEIGHT = 30;

    static int pixelSize = canvasHeight / pixelAmp;


    static string version = "v0.1.0";
    static public uint frameCount = 0;
    static int targetFPS = 0;
    static int currentTargetFPS = targetFPS;
    static Color[,] PixelGrid = new Color[(canvasWidth/pixelSize)+1, (canvasHeight/pixelSize)+1];
    public static Color currentPaintColor = Color.DARKBLUE;

    

    static Button clearButton = new
        Button(windowWidth - UI_ELEMENT_OFFSET_X - DEFAULT_BUTTON_WIDTH, UI_ELEMENT_OFFSET_Y, DEFAULT_BUTTON_WIDTH, DEFAULT_BUTTON_HEIGHT, Color.BLACK, "Clear", Color.WHITE);
    static ToggleButton testToggleButton = new
        ToggleButton(windowWidth - UI_ELEMENT_OFFSET_X*2 - DEFAULT_BUTTON_WIDTH*2, UI_ELEMENT_OFFSET_Y, DEFAULT_BUTTON_WIDTH, DEFAULT_BUTTON_HEIGHT, Color.BLACK, "Refresh", Color.WHITE);
    static ToggleButton eraserButton = new
        ToggleButton(windowWidth - UI_ELEMENT_OFFSET_X * 3 - DEFAULT_BUTTON_WIDTH * 3, UI_ELEMENT_OFFSET_Y, DEFAULT_BUTTON_WIDTH, DEFAULT_BUTTON_HEIGHT, Color.WHITE, "Eraser", Color.BLACK);

    static Button[] buttons = {clearButton};
    static ToggleButton[] toggleButtons = { testToggleButton, eraserButton };

    static bool started = false;
    static bool fullyRefresh = false;

    

    static void SetUp()
    {
        /*
         Stopwatch timer = Stopwatch.StartNew();
                TimeSpan timespan = timer.Elapsed;
                Console.WriteLine(MathF.Round((float)timespan.TotalMilliseconds , 2) + "ms");
                timer.Stop();
         */
        //Texture2D screenTexture = Raylib.LoadTexture(pixelmap);
        Raylib.SetTargetFPS(targetFPS);
        
        pixelSize = canvasHeight / pixelAmp;





        Raylib.InitWindow(windowWidth, windowHeight, "PixelEditor " + version);


        


    }




    public static void Main()
    {
        
        SetUp();
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.WHITE);
        Raylib.SwapScreenBuffer();
        Raylib.ClearBackground(Color.WHITE);
        Raylib.SwapScreenBuffer();
        Raylib.EndDrawing();

        while (!Raylib.WindowShouldClose())
        {      
            Draw(0);

        }


        Raylib.CloseWindow();
    }

    static void Draw(int scene)
    {
        Raylib.BeginDrawing();
        frameCount++;
 
        if (fullyRefresh)
        {
            Raylib.ClearBackground(Color.WHITE);


        }
        if (Raylib.IsWindowFocused() && targetFPS != currentTargetFPS)
        {
            currentTargetFPS = targetFPS;
            Raylib.SetTargetFPS(targetFPS);
        }
        else if(!Raylib.IsWindowFocused() && targetFPS == currentTargetFPS)
        {
            currentTargetFPS = 30;
            Raylib.SetTargetFPS(currentTargetFPS);
        }


        Raylib.DrawRectangle(0, 0, Raylib.GetScreenWidth(), UIHeight, Color.LIGHTGRAY);
        Raylib.DrawRectangle(0, UIHeight-5, Raylib.GetScreenWidth(), 5, Color.RED);

        Raylib.DrawRectangleGradientEx(new Raylib_cs.Rectangle(200, 0, 120, UIHeight-5), Color.WHITE, Color.SKYBLUE,Color.GREEN ,Color.RED);

        if (Raylib.IsWindowFocused())
        {
            Raylib.DrawText($"PixelEditor {version}\nFPS: {Raylib.GetFPS()}\nframecount: {frameCount}\nx:{Raylib.GetMouseX()} y:{Raylib.GetMouseY()}", 12, 12, 20, Color.BLACK);

        }

        foreach (Button _Button in buttons)
        {
            _Button.DrawButton();

        }
        foreach (ToggleButton _toggleButton in toggleButtons)
        {
            _toggleButton.DrawButton();

        }

        if (testToggleButton.ButtonToggled())
        {
            fullyRefresh = true;
        }
        else 
        {
            fullyRefresh = false;
        }
        if (eraserButton.ButtonToggled())
        {
            currentPaintColor = new Color();
        }
        else
        {
            currentPaintColor = Color.BLUE;
        }


        if (clearButton.ButtonPressed())
        {
            Raylib.ClearBackground(Color.WHITE);
            Raylib.SwapScreenBuffer();
            Raylib.ClearBackground(Color.WHITE);
            Raylib.SwapScreenBuffer();
            for (int i = 0; i < PixelGrid.GetLength(0); i++)
            {
                for (int j = 0; j < PixelGrid.GetLength(1); j++)
                {

                    if (!PixelGrid[i, j].Equals(new Color()))
                    {
                        PixelGrid[i, j] = new Color();

                    }
                }
            }


        }





        if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
        {
            int x = Raylib.GetMouseX() - Raylib.GetMouseX() % pixelSize;
            int y = Raylib.GetMouseY() - Raylib.GetMouseY() % pixelSize;
            if (x >= canvasX && x <= canvasX + canvasWidth && y >= canvasY && y <= canvasY + canvasHeight)
            {
                if (!fullyRefresh)
                {
                    Raylib.DrawRectangle(x, y, pixelSize, pixelSize, currentPaintColor);

                }
                
                PixelGrid[(x / pixelSize - (UIwidth / pixelSize)), (y / pixelSize - (UIHeight / pixelSize))] = currentPaintColor;

         
                
            }



        }



        if (fullyRefresh)
        {
            
            for (int i = 0; i < PixelGrid.GetLength(0);i++)
            {
                for (int j = 0; j < PixelGrid.GetLength(1); j++)
                {
  
                    if (!PixelGrid[i, j].Equals(new Color()))
                    {
                        Raylib.DrawRectangle((int)((i) * pixelSize) + UIwidth, (int)((j) * pixelSize) + UIHeight, pixelSize, pixelSize, PixelGrid[i, j]);

                    }
                }
            }

   
        }


        Raylib.EndDrawing();

        
    }






}










