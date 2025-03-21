using com.github.zehsteam.LocalMultiplayer.Helpers;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using Steamworks;
using Steamworks.Data;
using System;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(SteamManager))]
internal static class SteamManagerPatch
{
    [HarmonyPatch(nameof(SteamManager.OnLobbyCreated))]
    [HarmonyPostfix]
    private static void OnLobbyCreatedPatch(ref Result _result, ref Lobby _lobby)
    {
        if (_result != Result.OK)
        {
            return;
        }

        GlobalSaveHelper.Save("SteamLobbyId", _lobby.Id.ToString());
    }

    [HarmonyPatch(nameof(SteamManager.SendSteamAuthTicket))]
    [HarmonyPrefix]
    private static bool SendSteamAuthTicketPatch()
    {
        PhotonNetwork.AuthValues = new AuthenticationValues(Guid.NewGuid().ToString());
        return false;
    }
}
