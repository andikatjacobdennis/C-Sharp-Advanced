To dynamically load and use plugins from DLL files in C#, you'll typically use reflection to discover and invoke classes that implement a specific interface. This approach involves several key steps:

1. **Define a Common Interface**: This interface will be implemented by all plugin classes.
2. **Load DLLs Dynamically**: Use reflection to load the assemblies at runtime.
3. **Discover and Instantiate Types**: Find classes that implement the interface and create instances of these classes.
4. **Invoke Methods**: Use reflection to call methods on these instances.

Here’s a step-by-step example illustrating how to achieve this:

### Step 1: Define the Interface

Define a common interface that all plugins will implement. For example:

```csharp
public interface IPlugin
{
    void Execute();
}
```

### Step 2: Create a Plugin

Create a class library project to represent the plugin. Implement the `IPlugin` interface in this class:

```csharp
// Plugin.dll
using System;

public class MyPlugin : IPlugin
{
    public void Execute()
    {
        Console.WriteLine("Plugin executed!");
    }
}
```

Compile this class into a DLL (e.g., `Plugin.dll`).

### Step 3: Load and Use Plugins Dynamically

Create a host application that will load the DLL and use the plugin:

```csharp
using System;
using System.IO;
using System.Linq;
using System.Reflection;

class Program
{
    static void Main()
    {
        string pluginDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
        string[] pluginFiles = Directory.GetFiles(pluginDirectory, "*.dll");

        foreach (string pluginFile in pluginFiles)
        {
            try
            {
                // Load the plugin assembly
                Assembly assembly = Assembly.LoadFrom(pluginFile);

                // Find types that implement the IPlugin interface
                var pluginTypes = assembly.GetTypes()
                                          .Where(t => typeof(IPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

                foreach (var pluginType in pluginTypes)
                {
                    // Create an instance of the plugin
                    IPlugin plugin = (IPlugin)Activator.CreateInstance(pluginType);
                    
                    // Execute the plugin
                    plugin.Execute();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading plugin {pluginFile}: {ex.Message}");
            }
        }
    }
}
```

### Explanation

1. **Define the Interface:**
   - `IPlugin` is the common interface that all plugins will implement. It includes an `Execute` method that all plugins must define.

2. **Create the Plugin DLL:**
   - The `MyPlugin` class in the plugin DLL implements the `IPlugin` interface. You need to compile this into a DLL file (e.g., `Plugin.dll`).

3. **Load and Use Plugins Dynamically:**
   - **Load DLLs:** `Assembly.LoadFrom(pluginFile)` is used to load the DLL dynamically.
   - **Find Implementations:** `assembly.GetTypes()` retrieves all types from the loaded assembly. `typeof(IPlugin).IsAssignableFrom(t)` filters the types to those that implement the `IPlugin` interface.
   - **Instantiate and Execute:** `Activator.CreateInstance(pluginType)` creates an instance of the type, and the `Execute` method is called on this instance.

### Important Considerations

- **Error Handling:** Proper error handling is important for dealing with issues such as loading problems or type mismatches.
- **Security:** Be cautious when loading and executing code dynamically, especially from untrusted sources.
- **Directory Structure:** Ensure that the plugin DLLs are placed in the correct directory (`Plugins` in this case) or adjust the `pluginDirectory` path as needed.
