using System;
using System.Threading;

class Program
{
    private static ReaderWriterLockSlim _lock = new ReaderWriterLockSlim();
    private static int _maxNumber = 20; // The maximum number to print

    static void Main()
    {
        // Create and start threads for odd and even number printing
        Thread oddThread = new Thread(PrintOddNumbers);
        Thread evenThread = new Thread(PrintEvenNumbers);

        oddThread.Start();
        evenThread.Start();

        // Wait for both threads to complete
        oddThread.Join();
        evenThread.Join();
    }

    static void PrintOddNumbers()
    {
        for (int i = 1; i <= _maxNumber; i += 2)
        {
            // Acquire the reader lock to print odd numbers
            _lock.EnterReadLock();
            try
            {
                Console.WriteLine($"Odd: {i}");
                // Simulate some work
                Thread.Sleep(100);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }

    static void PrintEvenNumbers()
    {
        for (int i = 2; i <= _maxNumber; i += 2)
        {
            // Acquire the reader lock to print even numbers
            _lock.EnterReadLock();
            try
            {
                Console.WriteLine($"Even: {i}");
                // Simulate some work
                Thread.Sleep(100);
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }
    }
}
