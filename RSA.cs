using System;
using System.Security.Cryptography;
using System.Text;

namespace RSAEncryptionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate a new RSA key pair
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false; // Do not store the key in a container

                // Get the public and private keys
                string publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
                string privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());

                Console.WriteLine("Public Key: \n" + publicKey);
                Console.WriteLine("\nPrivate Key: \n" + privateKey);

                // Original message
                string originalMessage = "This is a secret message.";

                // Encrypt the message using the public key
                byte[] encryptedMessage = Encrypt(Encoding.UTF8.GetBytes(originalMessage), publicKey);
                Console.WriteLine("\nEncrypted Message: " + Convert.ToBase64String(encryptedMessage));

                // Decrypt the message using the private key
                byte[] decryptedMessage = Decrypt(encryptedMessage, privateKey);
                Console.WriteLine("\nDecrypted Message: " + Encoding.UTF8.GetString(decryptedMessage));
            }

            Console.ReadLine();
        }

        public static byte[] Encrypt(byte[] dataToEncrypt, string publicKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false; // Do not store the key in a container

                // Import the public key
                rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);

                // Encrypt the data
                return rsa.Encrypt(dataToEncrypt, true); // Use OAEP padding
            }
        }

        public static byte[] Decrypt(byte[] dataToDecrypt, string privateKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false; // Do not store the key in a container

                // Import the private key
                rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);

                // Decrypt the data
                return rsa.Decrypt(dataToDecrypt, true); // Use OAEP padding
            }
        }
    }
}
