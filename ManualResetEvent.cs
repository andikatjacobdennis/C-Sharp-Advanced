using System;
using System.Threading;

class Program
{
    private static ManualResetEvent _oddEvent = new ManualResetEvent(false);
    private static ManualResetEvent _evenEvent = new ManualResetEvent(false);
    private static int _maxNumber = 20; // The maximum number to print

    static void Main()
    {
        // Create and start threads for odd and even number printing
        Thread oddThread = new Thread(PrintOddNumbers);
        Thread evenThread = new Thread(PrintEvenNumbers);

        oddThread.Start();
        evenThread.Start();

        // Initially signal the odd thread to start
        _oddEvent.Set();

        // Wait for both threads to complete
        oddThread.Join();
        evenThread.Join();
    }

    static void PrintOddNumbers()
    {
        for (int i = 1; i <= _maxNumber; i += 2)
        {
            // Wait for the signal to print
            _oddEvent.WaitOne();

            Console.WriteLine($"Odd: {i}");

            // Reset the signal for the next odd number
            _oddEvent.Reset();

            // Signal the even thread to print the next even number
            _evenEvent.Set();
        }
    }

    static void PrintEvenNumbers()
    {
        for (int i = 2; i <= _maxNumber; i += 2)
        {
            // Wait for the signal to print
            _evenEvent.WaitOne();

            Console.WriteLine($"Even: {i}");

            // Reset the signal for the next even number
            _evenEvent.Reset();

            // Signal the odd thread to print the next odd number
            _oddEvent.Set();
        }
    }
}
