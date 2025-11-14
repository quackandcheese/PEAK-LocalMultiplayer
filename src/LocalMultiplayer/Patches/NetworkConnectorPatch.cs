using com.quackandcheese.LocalMultiplayer.Helpers;
using HarmonyLib;
using Peak.Network;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;

namespace com.quackandcheese.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(NetworkConnector))]
internal static class NetworkConnectorPatch
{
    [HarmonyPatch(nameof(NetworkConnector.Start))]
    [HarmonyPostfix]
    private static void StartPatch()
    {
        PhotonNetworkHelper.SetPhotonServerSettings();
    }
}
