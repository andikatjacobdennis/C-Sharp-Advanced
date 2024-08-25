using System;

[Flags]
public enum FileAccessPermissions
{
    None = 0,
    Read = 1 << 0,   // 1
    Write = 1 << 1,  // 2
    Execute = 1 << 2, // 4
    Delete = 1 << 3, // 8
    ReadWrite = Read | Write, // 3 (1 | 2)
    All = Read | Write | Execute | Delete // 15 (1 | 2 | 4 | 8)
}

class Program
{
    static void Main()
    {
        FileAccessPermissions myPermissions = FileAccessPermissions.Read | FileAccessPermissions.Write;

        Console.WriteLine($"My Permissions: {myPermissions}");
        Console.WriteLine($"Has Read Permission: {myPermissions.HasFlag(FileAccessPermissions.Read)}");
        Console.WriteLine($"Has Execute Permission: {myPermissions.HasFlag(FileAccessPermissions.Execute)}");

        // Add a permission
        myPermissions |= FileAccessPermissions.Execute;
        Console.WriteLine($"Updated Permissions: {myPermissions}");

        // Remove a permission
        myPermissions &= ~FileAccessPermissions.Write;
        Console.WriteLine($"Updated Permissions after removing Write: {myPermissions}");
    }
}
