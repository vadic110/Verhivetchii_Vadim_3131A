using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Verhivetchii_Vadim_3131A
{
    public class MassiveObject
    {
        private const String FILENAME = "assets/soccer_ball.obj"; // Calea către fișierul .obj
        private const int FACTOR_SCALARE_IMPORT = 100; // Factorul de scalare pentru coordonatele încărcate

        private List<Vector3> coordsList; // Lista de coordonate pentru obiect
        private bool visibility; // Starea de vizibilitate a obiectului
        private Color meshColor; // Culoarea mesh-ului
        private bool hasError; // Indică dacă a apărut vreo eroare la încărcarea fișierului

        private const int GRAVITY_OFFSET = 2; // Offset pentru simularea gravitației

        public MassiveObject(Color col)
        {
            try
            {
                coordsList = LoadFromObjFile(FILENAME); // Încărcarea coordonatelor din fișierul .obj

                if (coordsList.Count == 0)
                {
                    Console.WriteLine("Crearea obiectului a eșuat: obiect negăsit/coordonate lipsă!");
                    return;
                }

                visibility = false;
                meshColor = col;
                hasError = false;
                Console.WriteLine("Obiect 3D încărcat - " + coordsList.Count.ToString() + " vertexuri disponibile!");
            }
            catch (Exception)
            {
                Console.WriteLine("EROARE: Fișierul assets <" + FILENAME + "> lipsește!!!");
                hasError = true;
            }
        }

        // Metodă pentru comutarea vizibilității obiectului
        public void ToggleVisibility()
        {
            if (hasError == false)
            {
                visibility = !visibility;
            }
        }

        // Metodă pentru desenarea obiectului
        public void Draw()
        {
            if (hasError == false && visibility == true)
            {
                GL.Color3(meshColor);
                GL.Begin(PrimitiveType.Triangles);
                foreach (var vert in coordsList)
                {
                    GL.Vertex3(vert);
                }
                GL.End();
            }
        }

        // Metodă pentru încărcarea coordonatelor din fișierul .obj
        private List<Vector3> LoadFromObjFile(string fname)
        {
            List<Vector3> vlc3 = new List<Vector3>();

            var lines = File.ReadLines(fname);
            foreach (var line in lines)
            {
                if (line.Trim().Length > 2)
                {
                    string ch1 = line.Trim().Substring(0, 1);
                    string ch2 = line.Trim().Substring(1, 1);
                    if (ch1 == "v" && ch2 == " ")
                    {
                        string[] block = line.Trim().Split(' ');
                        if (block.Length == 4)
                        {
                            float xval = float.Parse(block[1].Trim()) * FACTOR_SCALARE_IMPORT;
                            float yval = float.Parse(block[2].Trim()) * FACTOR_SCALARE_IMPORT;
                            float zval = float.Parse(block[3].Trim()) * FACTOR_SCALARE_IMPORT;

                            vlc3.Add(new Vector3((int)xval, (int)yval, (int)zval));
                        }
                    }
                }
            }

            return vlc3;
        }

        // Verifică dacă obiectul a lovit solul
        public bool GroundCollisionDetected()
        {
            foreach (Vector3 v in coordsList)
            {
                if (v.Y <= 0)
                {
                    return true;
                }
            }

            return false;
        }

        // Actualizează poziția obiectului (simulează gravitația)
        public void UpdatePosition(bool gravityStatus)
        {
            if (visibility && gravityStatus && !GroundCollisionDetected())
            {
                for (int i = 0; i < coordsList.Count; i++)
                {
                    coordsList[i] = new Vector3(coordsList[i].X, coordsList[i].Y - GRAVITY_OFFSET, coordsList[i].Z);
                }
            }
        }
    }
}
