using System;
using Raylib_cs;

namespace PixelEditor_Raylib;

public struct Button
{
    int posX;
    int posY;
    int width;
    int height;
    public Color color = new Color();
    public Color colorText = Color.BLACK;
    public string text = "";
    float textSize;

    public Button(int _posX, int _posY, int _width, int _height, Color _color, string _text = "", Color _colorText = new Color())
    {
        posX = _posX;
        posY = _posY;
        width = _width;
        height = _height;
        color = _color;
        colorText = _colorText;

        textSize = (_width * _height) / 1000;
        text = _text;
    }

    public void DrawButton()
    {
         

        if (text != "")
        {
            Raylib.DrawText(text, posX, posY + height, (int)textSize, colorText);
        }

        if (Raylib.GetMouseX() > posX && Raylib.GetMouseX() < (posX + width) && Raylib.GetMouseY() > posY && Raylib.GetMouseY() < (posY + height) && Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
        {

            Raylib.DrawRectangle(posX, posY, (int)(width * 0.9f), (int)(height * 0.9f), colorText);

        }
        else 
        {
            Raylib.DrawRectangle(posX, posY, width, height, color);
        }


    }

    public bool ButtonPressed()
        {

            if (Raylib.GetMouseX() > posX && Raylib.GetMouseX() < (posX + width) && Raylib.GetMouseY() > posY && Raylib.GetMouseY() < (posY + height) && Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT))
            {
                return true;
            }
            else
            {
                return false;
            }
        }




}

public class ToggleButton
{
    int posX;
    int posY;
    int width;
    int height;
    public Color colorText = Color.BLACK;

    Color pressedColor = new Color();
    public string text = "";
    float textSize;
    bool buttonToggled = new Boolean();

    public ToggleButton(int _posX, int _posY, int _width, int _height, Color _pressedColor, string _text = "", Color _colorText = new Color())
    {
        posX = _posX;
        posY = _posY;
        width = _width;
        height = _height;

        pressedColor = _pressedColor;
        colorText = _colorText;
        textSize = (_width * _height) / 1000;
        text = _text;

    }

    public void DrawButton()
    {


        if (text != "")
        {
            Raylib.DrawText(text, posX, posY + height, (int)textSize, colorText);
        }

        if (Raylib.GetMouseX() > posX && Raylib.GetMouseX() < (posX + width) && Raylib.GetMouseY() > posY && Raylib.GetMouseY() < (posY + height) && Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
        {

            Raylib.DrawRectangle(posX, posY, (int)(width * 0.9f), (int)(height * 0.9f), pressedColor);

        }
        else
        {
            
            if (buttonToggled)
            {

                Raylib.DrawRectangleLines(posX, posY, width, height, pressedColor);
            }
            else
            {
                Raylib.DrawRectangle(posX, posY, width, height, pressedColor);

            }


        }


    }

    public bool ButtonToggled()
    {

        if (Raylib.GetMouseX() > posX && Raylib.GetMouseX() < (posX + width) && Raylib.GetMouseY() > posY && Raylib.GetMouseY() < (posY + height) && Raylib.IsMouseButtonReleased(MouseButton.MOUSE_BUTTON_LEFT))
        {
            if (!buttonToggled)
            {
                buttonToggled = true;

            }
            else
            {
                buttonToggled = false;

            }

            
        }
        return buttonToggled;
    }




}





