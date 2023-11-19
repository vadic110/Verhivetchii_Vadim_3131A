using OpenTK.Input;
using System;
using System.Drawing;

namespace Verhivetchii_Vadim_3131A
{
    // Clasa pentru manipularea culorilor din scena 3D folosind tastatura
    class ColorController
    {
        // Generator de culori aleatorii pentru vertexuri
        private RandomColorGenerator colorGenerator = new RandomColorGenerator();

        // Stocarea stării tastelor
        private KeyboardState lastKeyPressed;

        // Cerința 1: Metoda pentru schimbarea culorii de pe fiecare canal a unei suprafețe a cubului
        public void SetColor(KeyboardState keyboard, ref double red, ref double blue, ref double green, ref double alpha)
        {
            // Verificarea tastelor apăsate și ajustarea culorilor corespunzător
            if (keyboard[Key.Up] && keyboard[Key.R] && red < 1)
            {
                red += 0.05;
            }
            else if (keyboard[Key.Down] && keyboard[Key.R] && red > 0)
            {
                red -= 0.05;
            }
            else if (keyboard[Key.Up] && keyboard[Key.B] && blue < 1)
            {
                blue += 0.05;
            }
            else if (keyboard[Key.Down] && keyboard[Key.B] && blue > 0)
            {
                blue -= 0.05;
            }
            else if (keyboard[Key.Up] && keyboard[Key.G] && green < 1)
            {
                green += 0.05;
            }
            else if (keyboard[Key.Down] && keyboard[Key.G] && green > 0)
            {
                green -= 0.05;
            }
            else if (keyboard[Key.Up] && keyboard[Key.A] && alpha < 1)
            {
                alpha += 0.05;
            }
            else if (keyboard[Key.Down] && keyboard[Key.A] && alpha > 0)
            {
                alpha -= 0.05;
                if (alpha < 0.05)
                {
                    alpha = 0;
                }
            }
        }

        // Cerința 2: Metoda pentru setarea unor culori random vertexurilor triunghiului la apăsarea tastelor numerice 1-3
        public void SetTriangleColors(KeyboardState keyboard, ref Color color1, ref Color color2, ref Color color3)
        {
            // Stochează culorile temporar pentru a verifica modificările
            Color temp_color1 = color1;
            Color temp_color2 = color2;
            Color temp_color3 = color3;

            // Verifică dacă s-a apăsat o nouă tastă
            if (keyboard != lastKeyPressed)
            {
                // Schimbă culorile vertexurilor în funcție de tasta apăsată
                if (keyboard[Key.Number1])
                {
                    color1 = colorGenerator.Generate();
                    Console.WriteLine("Vertex 1: " + color1);
                }
                if (keyboard[Key.Number2])
                {
                    color2 = colorGenerator.Generate();
                    Console.WriteLine("Vertex 2: " + color2);
                }
                if (keyboard[Key.Number3])
                {
                    color3 = colorGenerator.Generate();
                    Console.WriteLine("Vertex 3: " + color3);
                }

                // Actualizează ultima tastă apăsată
                lastKeyPressed = keyboard;
            }
        }
    }
}