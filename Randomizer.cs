using OpenTK;
using System;
using System.Drawing;

namespace Verhivetchii_Vadim_3131A
{
    // Această clasă generează diverse valori aleatoare pentru diferite tipuri de parametri.
    class Randomizer
    {
        private Random r; // Variabilă pentru generarea numerelor aleatoare

        private const int LOW_INT_VAL = -25;
        private const int HIGH_INT_VAL = 25;
        private const int LOW_COORD_VAL = -50;
        private const int HIGH_COORD_VAL = 50;

        // Constructor standard. Inițializat cu ceasul sistemului pentru seed-ul generatorului.
        public Randomizer()
        {
            r = new Random();
        }

        // Această metodă returnează o culoare aleatoare când este cerută.
        public Color RandomColor()
        {
            int genR = r.Next(0, 255);
            int genG = r.Next(0, 255);
            int genB = r.Next(0, 255);

            Color col = Color.FromArgb(genR, genG, genB);

            return col;
        }

        // Această metodă returnează un punct 3D aleator. Valorile sunt în intervalul (centrat pe 0).
        public Vector3 Random3DPoint()
        {
            int genA = r.Next(LOW_COORD_VAL, HIGH_COORD_VAL);
            int genB = r.Next(LOW_COORD_VAL, HIGH_COORD_VAL);
            int genC = r.Next(LOW_COORD_VAL, HIGH_COORD_VAL);

            Vector3 vec = new Vector3(genA, genB, genC);

            return vec;
        }

        // Această metodă returnează un întreg aleator. Valoarea este în intervalul prestabilit (simetric față de zero).
        public int RandomInt()
        {
            int i = r.Next(LOW_INT_VAL, HIGH_INT_VAL);

            return i;
        }

        // Această metodă returnează un întreg aleator. Valoarea este între valorile predefinite.
        public int RandomInt(int minValue, int maxValue)
        {
            int i = r.Next(minValue, maxValue);

            return i;
        }

        // Această metodă returnează un întreg aleator. Valoarea este între 0 și o valoare dată.
        public int RandomInt(int maxvalue)
        {
            int i = r.Next(maxvalue);

            return i;
        }
    }
}
