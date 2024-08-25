using System;
using System.IO;

namespace IDisposablePatternDemo
{
    // Simulating a FileWatcher class that raises a custom event when a file is changed
    public class FileWatcher
    {
        // Define a custom delegate for the event
        public delegate void FileChangedEventHandler(string filePath);

        // Define the event using the custom delegate
        public event FileChangedEventHandler FileChanged;

        // Method to trigger the FileChanged event
        public void OnFileChanged(string filePath)
        {
            FileChanged?.Invoke(filePath);
        }
    }

    // The main class that implements IDisposable to handle cleanup
    public class FileProcessor : IDisposable
    {
        private readonly FileWatcher _fileWatcher;
        private readonly FileStream _fileStream;
        private bool _disposed = false; // To detect redundant calls

        public FileProcessor(FileWatcher fileWatcher, string filePath)
        {
            _fileWatcher = fileWatcher ?? throw new ArgumentNullException(nameof(fileWatcher));
            _fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            // Subscribe to the FileChanged event
            _fileWatcher.FileChanged += OnFileChanged;
        }

        // Event handler for the FileChanged event
        private void OnFileChanged(string filePath)
        {
            Console.WriteLine($"File changed: {filePath}");
            // Handle the file change event (e.g., log, update, etc.)
        }

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this); // Suppress finalization for this object
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Unsubscribe from events
                _fileWatcher.FileChanged -= OnFileChanged;

                // Dispose managed resources (e.g., FileStream)
                _fileStream?.Dispose();
            }

            // Free any unmanaged resources here if necessary

            _disposed = true;
        }

        // Destructor/Finalizer
        ~FileProcessor()
        {
            Dispose(false);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var fileWatcher = new FileWatcher();
            using (var fileProcessor = new FileProcessor(fileWatcher, "example.txt"))
            {
                // Simulate a file change event
                fileWatcher.OnFileChanged("example.txt");

                // Additional work with fileProcessor
            } // fileProcessor is disposed here automatically due to the 'using' statement

            Console.WriteLine("FileProcessor has been disposed, and resources have been cleaned up.");
        }
    }
}
