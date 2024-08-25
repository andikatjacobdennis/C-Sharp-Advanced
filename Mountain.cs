using System;

namespace MountainPattern
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the height of the mountain: ");
            int height = int.Parse(Console.ReadLine());

            DrawMountain(height);

            Console.ReadLine();
        }

        static void DrawMountain(int height)
        {
            // Draw the ascending part of the mountain
            for (int i = 1; i <= height; i++)
            {
                // Print leading spaces
                for (int j = 0; j < height - i; j++)
                {
                    Console.Write(" ");
                }

                // Print stars
                for (int k = 0; k < (2 * i - 1); k++)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }

            // Draw the descending part of the mountain
            for (int i = height - 1; i >= 1; i--)
            {
                // Print leading spaces
                for (int j = 0; j < height - i; j++)
                {
                    Console.Write(" ");
                }

                // Print stars
                for (int k = 0; k < (2 * i - 1); k++)
                {
                    Console.Write("*");
                }

                Console.WriteLine();
            }
        }
    }
}
