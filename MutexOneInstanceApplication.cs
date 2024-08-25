using System;
using System.Threading;

class Program
{
    static void Main()
    {
        // Create a named mutex
        bool isCreatedNew;
        string mutexName = "Global\\MyUniqueAppMutex"; // Use a unique name for your application

        using (var mutex = new Mutex(true, mutexName, out isCreatedNew))
        {
            // Check if this is the first instance
            if (isCreatedNew)
            {
                // This is the first instance, continue execution
                Console.WriteLine("This is the only instance running.");

                // Simulate work
                Console.WriteLine("Press Enter to exit...");
                Console.ReadLine();
            }
            else
            {
                // Another instance is already running
                Console.WriteLine("Another instance is already running.");
                Console.ReadLine();
            }
        }
    }
}
