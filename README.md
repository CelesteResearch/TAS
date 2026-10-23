[English](README.md) | [简体中文](README.zh-CN.md)

# TAS

Optional TAS module for the Shared runtime. Workspace location: Mods/TAS/.

| Project                                                    | Purpose                                                |
| ---------------------------------------------------------- | ------------------------------------------------------ |
| Celeste.Mods.Tas.csproj                                    | Playback, commands, HUD, host integration and metadata |
| StudioCommunication/Celeste.Tas.StudioCommunication.csproj | Celeste Studio communication                           |
| SettingsFormat/Celeste.Tas.SettingsFormat.csproj           | Settings representation                                |

Dependencies: Shared and the packages declared in the project files. Prepare resources through the workspace Tools repository, then build and install:

```powershell
dotnet build ./Mods/TAS/Celeste.Mods.Tas.csproj -c Release
pwsh ./Tools/Install-Tas.ps1
```

Installation places the module, manifest, Lua script, resources and runtime dependencies at Content/Celeste/Mods/TAS/. Tools/Prepare.ps1 also prepares a checked-out TAS module automatically; -WithoutTas skips its standalone preparation and installation. Helper consumers may still build TAS projects through their MSBuild dependencies.

The module and port-specific changes are licensed under MIT. See [LICENSE](LICENSE) and [NOTICE.md](NOTICE.md).
