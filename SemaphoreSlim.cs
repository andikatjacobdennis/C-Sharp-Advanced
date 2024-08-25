using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    private static SemaphoreSlim _semaphore;
    private static int _maxConcurrentThreads = 3; // Maximum number of threads that can access the resource concurrently

    static void Main()
    {
        _semaphore = new SemaphoreSlim(_maxConcurrentThreads, _maxConcurrentThreads);

        Task[] tasks = new Task[10];

        for (int i = 0; i < tasks.Length; i++)
        {
            int taskId = i; // Capture the loop variable
            tasks[i] = Task.Run(() => AccessResource(taskId));
        }

        Task.WaitAll(tasks); // Wait for all tasks to complete
    }

    static async Task AccessResource(int taskId)
    {
        Console.WriteLine($"Task {taskId} is requesting access...");

        // Wait to enter the semaphore
        await _semaphore.WaitAsync();

        try
        {
            Console.WriteLine($"Task {taskId} has entered the semaphore.");

            // Simulate some work
            await Task.Delay(2000); // 2 seconds delay to simulate work

            Console.WriteLine($"Task {taskId} is releasing the semaphore.");
        }
        finally
        {
            // Release the semaphore
            _semaphore.Release();
        }
    }
}
