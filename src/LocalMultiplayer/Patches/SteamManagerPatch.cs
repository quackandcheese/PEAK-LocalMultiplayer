using com.quackandcheese.LocalMultiplayer.Helpers;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using Steamworks;
using System;
using UnityEngine;
using Zorro.Core;

namespace com.quackandcheese.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(SteamManager))]
internal static class SteamManagerPatch
{
    [HarmonyPatch(nameof(SteamManager.Awake))]
    [HarmonyPostfix]
    [HarmonyPriority(Priority.First)]
    private static void AwakePatch()
    {
        SteamAccountManager.Initialize();
    }
}
