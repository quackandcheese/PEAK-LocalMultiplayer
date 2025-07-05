using com.github.quackandcheese.LocalMultiplayer.Helpers;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

namespace com.github.quackandcheese.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(NetworkConnector))]
internal static class NetworkConnectorPatch
{
    [HarmonyPatch(nameof(NetworkConnector.Start))]
    [HarmonyPostfix]
    private static void StartPatch()
    {
        PhotonNetworkHelper.SetPhotonServerSettings();
    }

    [HarmonyPatch(nameof(NetworkConnector.LoadUserID))]
    [HarmonyPrefix]
    private static bool LoadUserIDPatch(ref AuthenticationValues __result)
    {
        __result = new AuthenticationValues(Guid.NewGuid().ToString());
        return false;
    }
}
