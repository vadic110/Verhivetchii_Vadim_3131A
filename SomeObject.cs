using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;

namespace Verhivetchii_Vadim_3131A
{
    public class SomeObject
    {
        private bool visibility;
        private bool isGravityBound;
        private Color color;
        private List<Vector3> coordinatesList;
        private Randomizer random;

        private const int GRAVITY_OFFSET = 1;

        // Constructorul clasei SomeObject
        public SomeObject(bool gravityStatus, List<Vector3> vertices)
        {
            random = new Randomizer();
            visibility = true;
            isGravityBound = gravityStatus;
            color = random.RandomColor();

            coordinatesList = new List<Vector3>();

            // Crearea unor coordonate pentru obiect, cu offseturi variabile
            int sizeOffset = random.RandomInt(3, 7);
            int heightOffset = random.RandomInt(40, 75);
            int radialOffset = random.RandomInt(-40, 40);
            int radOffset = random.RandomInt(-40, 40);

            for (int i = 0; i < 10; i++)
            {
                coordinatesList.Add(
                    new Vector3(vertices[i].X * sizeOffset + radialOffset,
                    vertices[i].Y * sizeOffset + heightOffset,
                    vertices[i].Z * sizeOffset + radOffset));
            }
        }

        // Metodă pentru desenarea obiectului
        public void Draw()
        {
            if (visibility)
            {
                GL.Color3(color);
                GL.Begin(PrimitiveType.QuadStrip);

                foreach (Vector3 v in coordinatesList)
                {
                    GL.Vertex3(v);
                }
                GL.End();
            }
        }

        // Metodă pentru actualizarea poziției obiectului în funcție de gravitație
        public void UpdatePosition(bool gravityStatus)
        {
            if (visibility && gravityStatus && !GroundCollisionDetected())
            {
                for (int i = 0; i < coordinatesList.Count; i++)
                {
                    coordinatesList[i] = new Vector3(coordinatesList[i].X, coordinatesList[i].Y - GRAVITY_OFFSET, coordinatesList[i].Z);
                }
            }
        }

        // Metodă pentru detectarea coliziunii cu solul
        public bool GroundCollisionDetected()
        {
            foreach (Vector3 v in coordinatesList)
            {
                if (v.Y <= 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
