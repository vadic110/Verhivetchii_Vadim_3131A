using System;
using System.Drawing;

namespace Verhivetchii_Vadim_3131A
{
    // Clasa pentru generarea de culori aleatorii
    class RandomColorGenerator
    {
        private Random random; // Variabilă pentru generarea numerelor aleatoare

        // Constructor pentru inițializarea generatorului de numere aleatoare
        public RandomColorGenerator()
        {
            random = new Random();
        }

        // Metoda care generează o culoare aleatoare
        public Color Generate()
        {
            // Generarea unor valori aleatoare pentru canalele Red, Green și Blue
            int red = random.Next(0, 255);
            int green = random.Next(0, 255);
            int blue = random.Next(0, 255);

            // Crearea unui obiect Color utilizând valorile generate
            Color color = Color.FromArgb(red, green, blue);

            return color; // Returnează culoarea generată aleator
        }
    }
}
