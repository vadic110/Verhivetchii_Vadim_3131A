using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Verhivetchii_Vadim_3131A
{
    // Clasa pentru desenarea unei rețele de grilă în spațiul 3D
    class Grid
    {
        private readonly Color colorisation; // Culorea grilei
        private bool visibility; // Starea de vizibilitate a grilei

        // CONSTANTE
        private readonly Color GRIDCOLOR = Color.WhiteSmoke; // Culoarea grilei
        private const int GRIDSTEP = 10; // Pasul grilei
        private const int UNITS = 50; // Unități (numărul de linii)
        private const int POINT_OFFSET = GRIDSTEP * UNITS; // Offset pentru poziționarea grilei
        private const int MICRO_OFFSET = 1; // Micul offset pentru a evita suprapunerea liniilor grilei cu axe

        public Grid()
        {
            colorisation = GRIDCOLOR; // Inițializare culoare grilă
            visibility = true; // Inițializare vizibilitate grilă
        }

        // Metodă pentru a arăta grila
        public void Show()
        {
            visibility = true;
        }

        // Metodă pentru a ascunde grila
        public void Hide()
        {
            visibility = false;
        }

        // Metodă pentru a comuta vizibilitatea grilei
        public void ToggleVisibility()
        {
            visibility = !visibility;
        }

        // Metodă pentru desenarea grilei
        public void Draw()
        {
            if (visibility) // Verificare vizibilitate
            {
                GL.Begin(PrimitiveType.Lines); // Începe desenarea liniilor

                GL.Color3(colorisation); // Setează culoarea grilei

                // Desenează liniile grilei pentru fiecare direcție pe planul XZ
                for (int i = -1 * GRIDSTEP * UNITS; i <= GRIDSTEP * UNITS; i += GRIDSTEP)
                {
                    // Linii paralele cu Oz (axe vertical)
                    GL.Vertex3(i + MICRO_OFFSET, 0, POINT_OFFSET);
                    GL.Vertex3(i + MICRO_OFFSET, 0, -1 * POINT_OFFSET);

                    // Linii paralele cu Ox (axe orizontal)
                    GL.Vertex3(POINT_OFFSET, 0, i + MICRO_OFFSET);
                    GL.Vertex3(-1 * POINT_OFFSET, 0, i + MICRO_OFFSET);
                }

                GL.End(); // Termină desenarea liniilor
            }
        }
    }
}
