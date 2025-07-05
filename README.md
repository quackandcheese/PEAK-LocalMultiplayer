# LocalMultiplayer
[![GitHub](https://img.shields.io/badge/GitHub-LocalMultiplayer-brightgreen?style=for-the-badge&logo=GitHub)](https://github.com/ZehsTeam/REPO-LocalMultiplayer)
[![Thunderstore Version](https://img.shields.io/thunderstore/v/Zehs/LocalMultiplayer?style=for-the-badge&logo=thunderstore&logoColor=white)](https://thunderstore.io/c/repo/p/Zehs/LocalMultiplayer/)
[![Thunderstore Downloads](https://img.shields.io/thunderstore/dt/Zehs/LocalMultiplayer?style=for-the-badge&logo=thunderstore&logoColor=white)](https://thunderstore.io/c/repo/p/Zehs/LocalMultiplayer/)

**Play multiplayer locally with one Steam account.**

## Features
This mod allows you to connect two R.E.P.O. instances to the same multiplayer game using one Steam account.

## Local Co-Op
If you want to use this mod for local co-op, I recommend using [Nucleus Co-Op](https://nucleus-coop.github.io) to control multiple instances of the game simultaneously.

## Usage
This mod requires you to create your own Photon cloud applications. (This does not require any payment.)

### 1. Photon
1. Go to the [Photon Engine](https://www.photonengine.com) website and sign in or create an account.
2. Navigate to the [Dashboard](https://dashboard.photonengine.com).
3. Create a new Photon cloud application and select `Pun` for the Photon SDK.
4. Choose any name you would like and click Create.
5. Create a new Photon cloud application and select `Voice` for the Photon SDK.
6. Choose any name you would like and click Create.

### 2. Config Settings
1. Open the config file. (See the Config Settings section on how to find the config file.)
2. Set `AppIdRealtime` to your Photon `Pun` application's App ID.
3. Set `AppIdVoice` to your Photon `Voice` application's App ID.

### 3. Steam Launch Options

> [!TIP]
> If you use the Gale mod manager, you can just set your launch mode to Direct in the settings and skip this step.

1. Go to Steam and right-click R.E.P.O.
2. Click "Properties..."
3. In the General tab, you should see an input field called "LAUNCH OPTIONS"
4. Put this for your launch options:
```
--doorstop-enable true --doorstop-target "YOUR_PROFILE_LOCATION_HERE\BepInEx\core\BepInEx.Preloader.dll"
```

> [!IMPORTANT]
> The file path must lead to your profile's `BepInEx.Preloader.dll` file in the `BepInEx/core` folder.

### 4. Testing
1. Open the game.
2. Go to Settings > Graphics.
3. Set your window mode to Windowed. (This is for easier testing. See screenshots for an example.)
4. Host a game.
5. Open the game again. (You will have two R.E.P.O. instances open at this point.)
6. Click the join game button.

## Config Settings
You must open your game at least once with the mod installed for the config file to get generated.

This mod uses a global config file so you don't have to configure your settings for each modpack you use and to prevent your Photon App IDs from being transferred in your profile codes.

You can locate the config file at this path:
```
%localappdata%\..\LocalLow\LandCrab\PEAK\LocalMultiplayer\global.cfg
```

> [!TIP]
> You can paste the config file path in your Windows run box or file explorer to automatically open the config file.

## Credits
- [BlueAmulet](https://github.com/BlueAmulet) - Providing info on how to make this mod work.
- [Unloaded Hangar](https://github.com/UnloadedHangar) - Helping with some specific code.

## Developer Contact
**Report bugs, suggest features, or provide feedback:**

| **Discord Server** | **Forum** | **Post** |  
|--------------------|-----------|----------|  
| [R.E.P.O. Modding Server](https://discord.com/invite/vPJtKhYAFe) | `#released-mods` | [LocalMultiplayer](https://discord.com/channels/1344557689979670578/1352815417579798652) |

- **GitHub Issues Page:** [LocalMultiplayer](https://github.com/ZehsTeam/REPO-LocalMultiplayer/issues)
- **Links:** https://solo.to/crithaxxog

[<img src="https://i.imgur.com/duJZQTS.png" width="200px">](https://ko-fi.com/zehsteam)

## Screenshots
<img src="https://i.imgur.com/VhVYtPV.png" width="100%">

----

<img src="https://i.imgur.com/nHoK8yb.png" width="100%">