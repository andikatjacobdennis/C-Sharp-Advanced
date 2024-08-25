using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    // Create a Mutex object
    private static Mutex _mutex = new Mutex();
    private static int _counter = 0; // Shared resource

    static void Main()
    {
        Task[] tasks = new Task[5];

        for (int i = 0; i < tasks.Length; i++)
        {
            int taskId = i; // Capture the loop variable
            tasks[i] = Task.Run(() => AccessCriticalSection(taskId));
        }

        Task.WaitAll(tasks); // Wait for all tasks to complete
    }

    static void AccessCriticalSection(int taskId)
    {
        Console.WriteLine($"Task {taskId} is requesting the mutex.");

        // Wait to acquire the mutex
        _mutex.WaitOne();

        try
        {
            Console.WriteLine($"Task {taskId} has acquired the mutex.");

            // Simulate some work with the shared resource
            for (int i = 0; i < 5; i++)
            {
                int localCounter = _counter;
                Thread.Sleep(100); // Simulate work
                localCounter++;
                _counter = localCounter;
                Console.WriteLine($"Task {taskId} incremented counter to {_counter}.");
            }
        }
        finally
        {
            // Release the mutex
            _mutex.ReleaseMutex();
            Console.WriteLine($"Task {taskId} has released the mutex.");
        }
    }
}
