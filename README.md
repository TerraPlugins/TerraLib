# TerraLib
> A library to help creating plugins.

Work in progress.

## Examples

Index:
- [Creating a plugin configuration file](#creating-a-plugin-configuration-file)

### Creating a plugin configuration file:

First, create your class with the config variables:

```csharp
public class ConfigTest
{
    public int Half = 22;
    public float Life = 2.2f;
    public string Is = "ss";
    public List<int> Good = new List<int>();

    public ConfigTest()
    {
        Good.Add(2);
        Good.Add(2);
        Good.Add(3);
        Good.Add(4);
    }
}
```

The simply declare it in your plugin class:

```csharp
public ConfigTest Config = new ConfigTest();
```

And you will usually want to load it on Initialize or OnPostInitialize:

```csharp
Config = TerraLib.Config.Read<ConfigTest>("MyConfigFile.json");
```

This method will create the file if it doesn't exist.

You can also save the configuration if you want:

```csharp
// The filename will always have a .json extension no matter what.
TerraLib.Config.Save(Config, "MyConfigFile");
```