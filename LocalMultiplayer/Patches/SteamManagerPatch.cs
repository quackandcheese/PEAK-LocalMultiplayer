using com.github.zehsteam.LocalMultiplayer.Helpers;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using Steamworks;
using Steamworks.Data;
using System;
using UnityEngine;

namespace com.github.zehsteam.LocalMultiplayer.Patches;

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

    [HarmonyPatch(nameof(SteamManager.Start))]
    [HarmonyPostfix]
    private static void StartPatch()
    {
        if (!IsValidClient())
        {
            Application.Quit();
        }
    }

    [HarmonyPatch(nameof(SteamManager.OnLobbyCreated))]
    [HarmonyPostfix]
    private static void OnLobbyCreatedPatch(ref Result _result, ref Lobby _lobby)
    {
        if (_result != Result.OK)
        {
            return;
        }

        GlobalSaveHelper.SteamLobbyId.Value = _lobby.Id;

        SteamAccountManager.UseSpoofAccount = false;
        SteamAccountManager.ResetSpoofAccountsInUse();
    }

    [HarmonyPatch(nameof(SteamManager.SendSteamAuthTicket))]
    [HarmonyPrefix]
    private static bool SendSteamAuthTicketPatch()
    {
        PhotonNetwork.AuthValues = new AuthenticationValues(Guid.NewGuid().ToString());
        return false;
    }

    private static bool IsValidClient()
    {
        AuthTicket authTicket = SteamManager.instance.steamAuthTicket;

        if (authTicket == null)
        {
            return false;
        }

        BeginAuthResult beginAuthResult = SteamUser.BeginAuthSession(authTicket.Data, SteamClient.SteamId);
        return beginAuthResult == BeginAuthResult.OK;
    }
}
