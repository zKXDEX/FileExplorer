# KnownFolders

This library retrieves paths to all "special folders" available on Windows systems, including those not exposed by the `System.Environment.GetFolderPath` method, which only covers folders available up to Windows XP, most [popularly](https://stackoverflow.com/questions/10667012/getting-downloads-folder-in-c) missing the path to the "Downloads" or other user folders introduced in Windows Vista and later.

The library is is available on [NuGet](https://www.nuget.org/packages/Syroot.Windows.IO.KnownFolders). The motivation is described in my [CodeProject article](http://www.codeproject.com/Articles/878605/Getting-All-Special-Folders-in-NET).

## Usage

```cs
using Syroot.Windows.IO;
string downloadsPath = KnownFolders.Downloads.Path;
```

The starting point is a `KnownFolder` instance, which represents exactly one special folder. It offers access to the following:

  - `string Path { get; set; }`: The current path (which might be customized by the user). This is what you want in most cases.
  - `string DefaultPath { get; set; }`: The default path the folder has if it would not have been redirected.
  - `string ExpandedPath { get; set; }`: Same as `Path`, but with all environment variables expanded.
  - `string DisplayName { get; }`: Returns a user friendly folder name as displayed in the File Explorer.
  - `Create()`: This method creates and initializes the known folder, e.g. creates the folder at the current path and writes the corresponding `Desktop.ini` (to set up the icon and localized name).

To retrieve a `KnownFolder` instance, there are two ways, depending on what you want to do:

  1. The easiest way is to use one of the many properties in the static `KnownFolders` class, which contains one property for each special folder.
  2. If you want to get the paths of another user than the current one (required rights assumed), you have to use the constructor of the `KnownFolder` class. You pass in a value of the `KnownFolderType` enumeration (which contains one member for each special folder) and the `WindowsIdentity` instance of the user to impersonate.

The [test application](https://gitlab.com/Syroot/KnownFolders/tree/master/src/Syroot.KnownFolders.Scratchpad) shows more sample code to enumerate all known folders of a Windows system.
