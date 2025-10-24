A small Windows desktop (cool) web browser built with .NET (WinForms). 

This is not intended to be a full-featured secure web browser. Instead it is a playground to add and test simple browser features and it is a useful starting point for learning WinForms + .NET desktop app structure

## How to run

Requirements:

- .NET SDK 8.0 or later installed (https://dotnet.microsoft.com/)
- Windows (project targets `net8.0-windows` and uses WinForms)

From a PowerShell prompt in the repository root you can run the already-built executable

```powershell
& '.\Browser\bin\Debug\net8.0-windows\Browser.exe'
# or
Start-Process -FilePath '.\Browser\bin\Debug\net8.0-windows\Browser.exe'
```