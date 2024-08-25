using System;
using System.IO;

namespace FileProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "example.txt"; // Replace with your file path
            string destinationPath = "recreated_example.txt";

            // Read the file into a byte array and get all metadata
            var fileData = ReadFile(filePath);

            // Recreate the file from the byte array and metadata
            RecreateFile(fileData, destinationPath);

            Console.WriteLine("File recreated successfully with all metadata.");
        }

        // Method to read a file and return its content as byte array along with metadata
        static FileData ReadFile(string filePath)
        {
            // Read the file content into a byte array
            byte[] fileContent = File.ReadAllBytes(filePath);

            // Get the file metadata
            string fileName = Path.GetFileName(filePath);
            DateTime creationTime = File.GetCreationTime(filePath);
            DateTime lastAccessTime = File.GetLastAccessTime(filePath);
            DateTime lastWriteTime = File.GetLastWriteTime(filePath);
            FileAttributes fileAttributes = File.GetAttributes(filePath);

            // Return the file data and metadata as an object
            return new FileData
            {
                FileName = fileName,
                FileContent = fileContent,
                CreationTime = creationTime,
                LastAccessTime = lastAccessTime,
                LastWriteTime = lastWriteTime,
                FileAttributes = fileAttributes
            };
        }

        // Method to recreate a file from byte array and metadata
        static void RecreateFile(FileData fileData, string destinationPath)
        {
            // Write the byte array back to a file
            File.WriteAllBytes(destinationPath, fileData.FileContent);

            // Restore file metadata
            File.SetCreationTime(destinationPath, fileData.CreationTime);
            File.SetLastAccessTime(destinationPath, fileData.LastAccessTime);
            File.SetLastWriteTime(destinationPath, fileData.LastWriteTime);
            File.SetAttributes(destinationPath, fileData.FileAttributes);
        }
    }

    // Class to store file data and metadata
    class FileData
    {
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public FileAttributes FileAttributes { get; set; }
    }
}
