# Revit .NET Cursor Plugin Template

A minimal .NET 8 class library template for developing Revit plugins with Design Automation API support and debugging capabilities in Visual Studio Code with Cursor.
This template is designed for automation workflows and cloud-based Revit processing using Autodesk Platform Services.

## Prerequisites

- .NET 8.0 SDK or later
- Revit 2025 or later
- Cursor IDE
- Design Automation Bridge for local testing

## Quick Setup

### 1. Configure Revit Paths

You need to **update the Revit paths** to match your installation:

#### In `RevitPlugin.csproj` (for assembly references):
```xml
<!-- Edit this path to match your Revit executable path -->
<RevitPath>C:\Program Files\Autodesk\Revit 2025</RevitPath>

<!-- Edit this path if you have Design Automation Bridge installed locally -->
<DesignAutomationBridgePath>C:\ProgramData\Autodesk\Revit\Addins\2025</DesignAutomationBridgePath>

<!-- Edit this path to match your Revit Addins path -->
<RevitAddinsPath>C:\ProgramData\Autodesk\Revit\Addins\2025</RevitAddinsPath>
```

Common Revit installation paths:
- Revit 2025: `C:\Program Files\Autodesk\Revit 2025`
- Revit 2024: `C:\Program Files\Autodesk\Revit 2026`

### 2. Build the Project

Open a terminal in the project directory and run:

```bash
dotnet build -c Debug -p:Platform=x64
```

The DLL will be created in `bin\Debug\RevitPlugin.dll`

> **Note:** The build process automatically updates the .addin file with the correct assembly path using `UpdateAddinPath.ps1`.

### 3. Debug in Cursor/VS Code

1. Set breakpoints in `Commands.cs` (e.g., in the `SampleMethod`)
2. Copy the .addin file to your Revit Addins folder (automatic in Debug builds)
3. Start Revit manually
4. In Cursor/VS Code, press `F5` to attach debugger
5. Select the `Revit.exe` process when prompted
6. Your plugin will load automatically with Revit

Your breakpoints will be hit when the Design Automation events trigger!

## Automation API Overview

This template is specifically designed for Design Automation workflows:

- Implements `IExternalDBApplication` for database-level operations
- No UI components (suitable for headless Revit execution)
- Includes `DesignAutomationBridge` for cloud automation support
- Event-driven architecture with `DesignAutomationReadyEvent`

## Debugging Configuration

The template includes debugging configuration in `.vscode/launch.json` that attaches to the Revit process.

## Project Structure

```
RevitPlugin/
├── RevitPlugin.csproj        # Project configuration
├── Commands.cs               # Main plugin implementation
├── RevitPlugin.addin         # Revit add-in manifest
├── UpdateAddinPath.ps1       # Build script for updating paths
├── package-bundle.ps1        # Packaging script for deployment
├── .vscode/
│   ├── launch.json          # Debug configuration
│   └── tasks.json           # Build tasks
├── .gitignore               # Git ignore rules
├── LICENSE                  # License file
└── README.md                # This file
```

## Customization

### Modifying the Main Plugin Logic

Edit the `HandleDesignAutomationReadyEvent` method in `Commands.cs`:

```csharp
private void HandleDesignAutomationReadyEvent(object? sender, DesignAutomationReadyEventArgs e)
{
    LogTrace("Design Automation Ready event triggered...");
    e.Succeeded = true;
    
    // Add your custom logic here
    ProcessRevitModel();
}

private void ProcessRevitModel()
{
    // Access the Revit document
    Document doc = e.DesignAutomationData.RevitDoc;
    
    // Perform your operations
    using (Transaction trans = new Transaction(doc, "My Operation"))
    {
        trans.Start();
        // Your code here
        trans.Commit();
    }
}
```

### Changing Target Revit Version

1. Update the `<RevitPath>` in the project file
2. Update the `<RevitAddinsPath>` to match the version year
3. Ensure the `<TargetFramework>` is compatible with your Revit version

## Troubleshooting

### "Could not load file or assembly" error
- Ensure Revit path is correct in the project file
- Verify you're building for x64 platform
- Check that the Revit API version matches your Revit installation

### Breakpoints not hitting
- Ensure you're running in Debug configuration
- Verify the .addin file is in the correct Revit Addins folder
- Check that the assembly path in the .addin file is correct
- Try rebuilding the project

### Design Automation Bridge issues
- Ensure DesignAutomationBridge.dll is referenced correctly
- Set `<Private>True</Private>` for the bridge reference to include it in output
- Verify the bridge path in the project file

### Plugin not loading
- Check the Revit journal file for errors
- Verify the AddInId in the .addin file is unique
- Ensure the .addin file XML is valid

## Additional Resources

- [Revit API Developer's Guide](https://help.autodesk.com/view/RVT/2025/ENU/?guid=Revit_API_Revit_API_Developers_Guide_html)
- [Automation API for Revit](https://aps.autodesk.com/en/docs/design-automation/v3/tutorials/revit/)
- [Autodesk Platform Services](https://aps.autodesk.com/)
- [Revit API Forum](https://forums.autodesk.com/t5/revit-api-forum/bd-p/160)

## License

This sample is licensed under the terms of the [MIT License](http://opensource.org/licenses/MIT). Please see the [LICENSE](LICENSE) file for full details.

## Written by

João Martins [in/jpornelas](https://www.linkedin.com/in/jpornelas/), [Autodesk Platform Services](http://aps.autodesk.com)
