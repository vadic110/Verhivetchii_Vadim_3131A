using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Verhivetchii_Vadim_3131A
{
    // Definirea unei clase numite Axes
    class Axes
    {
        // Variabilă privată pentru a reține starea de vizibilitate
        private bool myVisibility;

        // Constantă care definește lungimea axelor
        private const int AXIS_LENGTH = 75;

        // Constructor care inițializează vizibilitatea la true
        public Axes()
        {
            myVisibility = true;
        }

        // Metodă pentru a desena axele
        public void Draw()
        {
            // Verificarea vizibilității înainte de desenare
            if (myVisibility)
            {
                // Setarea grosimii liniei pentru desenarea axelor
                GL.LineWidth(1.0f);

                // Începerea desenului de linii
                GL.Begin(PrimitiveType.Lines);

                // Desenarea axei X în culoare roșie
                GL.Color3(Color.Red);
                GL.Vertex3(0, 0, 0);  // Punctul de start al axei X
                GL.Vertex3(AXIS_LENGTH, 0, 0); // Punctul final al axei X

                // Desenarea axei Y în culoare verde forestier
                GL.Color3(Color.ForestGreen);
                GL.Vertex3(0, 0, 0);  // Punctul de start al axei Y
                GL.Vertex3(0, AXIS_LENGTH, 0); // Punctul final al axei Y

                // Desenarea axei Z în culoare albastru royal
                GL.Color3(Color.RoyalBlue);
                GL.Vertex3(0, 0, 0);  // Punctul de start al axei Z
                GL.Vertex3(0, 0, AXIS_LENGTH); // Punctul final al axei Z

                // Terminarea desenului
                GL.End();
            }
        }

        // Metodă pentru a arăta axele
        public void Show()
        {
            myVisibility = true;
        }

        // Metodă pentru a ascunde axele
        public void Hide()
        {
            myVisibility = false;
        }

        // Metodă pentru a comuta vizibilitatea axelor
        public void ToggleVisibility()
        {
            myVisibility = !myVisibility;
        }
    }
}
