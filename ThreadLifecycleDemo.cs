using System;
using System.Threading;

namespace ThreadLifecycleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main thread starting.");

            // Create a thread to perform some work
            Thread workerThread = new Thread(WorkerThreadMethod);

            // Thread is in Unstarted state
            Console.WriteLine("Thread state after creation: " + workerThread.ThreadState);

            // Start the thread
            workerThread.Start();

            // Thread is in Running state
            Console.WriteLine("Thread state after starting: " + workerThread.ThreadState);

            // Put the main thread to sleep to allow workerThread to execute
            Thread.Sleep(1000);

            // Check the thread state while it's working
            Console.WriteLine("Thread state during execution: " + workerThread.ThreadState);

            // Wait for the worker thread to finish
            workerThread.Join();

            // Thread is in Stopped state
            Console.WriteLine("Thread state after completion: " + workerThread.ThreadState);

            Console.WriteLine("Main thread ending.");
        }

        static void WorkerThreadMethod()
        {
            Console.WriteLine("Worker thread starting.");

            // Simulate some work by sleeping
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"Worker thread doing work: Iteration {i}");
                Thread.Sleep(500); // Simulate work by sleeping for 500ms
            }

            Console.WriteLine("Worker thread ending.");
        }
    }
}
