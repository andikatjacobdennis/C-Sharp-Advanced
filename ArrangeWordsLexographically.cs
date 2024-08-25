using System;

class Program
{
    static void Main()
    {
        // Input text
        char[] input = "apple banana orange mango".ToCharArray();

        // Step 1: Extract words
        char[][] words = ExtractWords(input);

        // Step 2: Sort words lexicographically
        SortWords(words);

        // Step 3: Display results
        PrintWords(words);
    }

    static char[][] ExtractWords(char[] input)
    {
        // Determine the number of words
        int wordCount = CountWords(input);
        char[][] words = new char[wordCount][];

        int wordIndex = 0;
        int startIndex = 0;
        
        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] == ' ' || i == input.Length - 1)
            {
                int endIndex = i == input.Length - 1 ? i + 1 : i;
                int length = endIndex - startIndex;
                
                words[wordIndex] = new char[length];
                Array.Copy(input, startIndex, words[wordIndex], 0, length);
                wordIndex++;
                
                startIndex = i + 1;
            }
        }

        return words;
    }

    static int CountWords(char[] input)
    {
        int count = 0;
        bool inWord = false;

        for (int i = 0; i < input.Length; i++)
        {
            if (input[i] != ' ')
            {
                if (!inWord)
                {
                    count++;
                    inWord = true;
                }
            }
            else
            {
                inWord = false;
            }
        }

        return count;
    }

    static void SortWords(char[][] words)
    {
        int n = words.Length;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (CompareWords(words[i], words[j]) > 0)
                {
                    char[] temp = words[i];
                    words[i] = words[j];
                    words[j] = temp;
                }
            }
        }
    }

    static int CompareWords(char[] word1, char[] word2)
    {
        int minLength = Math.Min(word1.Length, word2.Length);

        for (int i = 0; i < minLength; i++)
        {
            if (word1[i] < word2[i])
            {
                return -1;
            }
            if (word1[i] > word2[i])
            {
                return 1;
            }
        }

        if (word1.Length < word2.Length)
        {
            return -1;
        }
        if (word1.Length > word2.Length)
        {
            return 1;
        }

        return 0;
    }

    static void PrintWords(char[][] words)
    {
        for (int i = 0; i < words.Length; i++)
        {
            for (int j = 0; j < words[i].Length; j++)
            {
                Console.Write(words[i][j]);
            }
            Console.WriteLine();
        }
    }
}
