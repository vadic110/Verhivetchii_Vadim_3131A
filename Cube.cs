using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Input;

namespace Verhivetchii_Vadim_3131A
{
    // Clasa pentru gestionarea unui cub în spațiul 3D
    class Cube
    {
        // Vectorul de Vector3 în care vor fi citite coordonatele cubului din fișier
        private List<Vector3> vertices;

        // Variabile pentru canalele de culoare
        private double red = 1, green = 1, blue = 1, alpha = 1;
        private Color tcolor1 = Color.Yellow, tcolor2 = Color.Yellow, tcolor3 = Color.Yellow;

        // Declarație a unui obiect pentru controller-ul de modificare a culorilor
        private ColorController colorController;

        // Constructor care primește calea către fișierul cu coordonatele cubului
        public Cube(string filePath)
        {
            vertices = new List<Vector3>();

            // Citirea din fișierul specificat a coordonatelor cubului
            string text = System.IO.File.ReadAllText(filePath);
            string[] lines = text.Split('\n');

            for (int i = 0; i < 36; i++)
            {
                string[] co = lines[i].Split(' ');
                vertices.Add(new Vector3(int.Parse(co[0]), int.Parse(co[1]), int.Parse(co[2])));
            }

            // Instantierea obiectului pentru controller-ul de culori
            colorController = new ColorController();
        }

        // Metoda pentru setarea culorii cubului și a unui triunghi din componența acestuia
        public void SetColor()
        {
            // Obținerea stării tastaturii
            KeyboardState keyboard = Keyboard.GetState();

            // Setarea culorii cubului și a triunghiului în funcție de tastele apăsate
            colorController.SetColor(keyboard, ref red, ref blue, ref green, ref alpha);
            colorController.SetTriangleColors(keyboard, ref tcolor1, ref tcolor2, ref tcolor3);
        }

        // Metoda pentru desenarea cubului pe ecran
        public void Draw()
        {
            GL.Begin(PrimitiveType.Triangles);
            for (int i = 0; i < 36; i = i + 3)
            {
                // Cerința 1: Setarea culorii unei suprafețe a cubului
                if (i > 28)
                    GL.Color4(red, green, blue, alpha);
                else
                    GL.Color3(Color.Blue);

                // Cerința 2: Setarea unei culori generate random pentru fiecare vertex al unui triunghi din componenta cubului
                if (i == 18)
                    GL.Color3(tcolor1);
                GL.Vertex3(vertices[i]);
                if (i == 18)
                    GL.Color3(tcolor2);
                GL.Vertex3(vertices[i + 1]);
                if (i == 18)
                    GL.Color3(tcolor3);
                GL.Vertex3(vertices[i + 2]);
            }
            GL.End();
        }
    }
}
