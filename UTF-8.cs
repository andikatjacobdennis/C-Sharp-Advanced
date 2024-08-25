using System;
using System.Text;

namespace Utf8StringConversion
{
    class Program
    {
        static void Main(string[] args)
        {
            // Original string
            string originalString = "Hello, World! Привет, мир! こんにちは世界";

            // Convert string to byte array
            byte[] byteArray = ConvertStringToByteArray(originalString);
            Console.WriteLine("Byte Array: " + BitConverter.ToString(byteArray));

            // Convert byte array back to string
            string convertedString = ConvertByteArrayToString(byteArray);
            Console.WriteLine("Converted String: " + convertedString);

            Console.ReadLine();
        }

        // Method to convert a string to a byte array using UTF-8 encoding
        static byte[] ConvertStringToByteArray(string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }

        // Method to convert a byte array back to a string using UTF-8 encoding
        static string ConvertByteArrayToString(byte[] byteArray)
        {
            return Encoding.UTF8.GetString(byteArray);
        }
    }
}
