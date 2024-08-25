public static class StringExtensions
{
    public static bool IsAllDigits(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return false;

        return str.All(char.IsDigit);
    }
}

// Usage:
// bool result = "12345".IsAllDigits(); // Returns true
