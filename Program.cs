using System;

//Tema Lab5

namespace Verhivetchii_Vadim_3131A
{
    /// Clasa ce contine punctul de intrare in aplicatie
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Instatierea unui obiect de tipul Window
            using (Window3D example = new Window3D())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}
