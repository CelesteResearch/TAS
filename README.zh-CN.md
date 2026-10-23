[English](README.md) | [简体中文](README.zh-CN.md)

# TAS

Shared 运行时的可选 TAS 模块。工作区位置：Mods/TAS/。

| 项目                                                       | 用途                              |
| ---------------------------------------------------------- | --------------------------------- |
| Celeste.Mods.Tas.csproj                                    | 播放、命令、HUD、宿主接入和元数据 |
| StudioCommunication/Celeste.Tas.StudioCommunication.csproj | Celeste Studio 通信               |
| SettingsFormat/Celeste.Tas.SettingsFormat.csproj           | 设置表示                          |

依赖 Shared 及项目文件声明的包。先通过工作区 Tools 恢复资源，再构建和安装：

```powershell
dotnet build ./Mods/TAS/Celeste.Mods.Tas.csproj -c Release
pwsh ./Tools/Install-Tas.ps1
```

模块、manifest、Lua 脚本、资源和运行依赖安装到 Content/Celeste/Mods/TAS/。Tools/Prepare.ps1 也会自动准备已检出的 TAS 模块；-WithoutTas 跳过其独立准备和安装。Helper 消费者仍可能通过 MSBuild 依赖构建 TAS 项目。

模块代码及移植修改均采用 MIT 许可，见 [LICENSE](LICENSE) 和 [NOTICE.md](NOTICE.md)。
