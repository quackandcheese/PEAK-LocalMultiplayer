using com.quackandcheese.LocalMultiplayer;
using com.quackandcheese.LocalMultiplayer.Helpers;
using HarmonyLib;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.Services.Lobbies.Models;

namespace com.quackandcheese.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(SteamLobbyHandler))]
internal static class SteamLobbyHandlerPatch
{
    [HarmonyPatch(nameof(SteamLobbyHandler.OnLobbyCreated))]
    [HarmonyPostfix]
    private static void OnLobbyCreatedPatch(ref LobbyCreated_t param)
    {
        if (param.m_eResult != EResult.k_EResultOK)
        {
            return;
        }

        GlobalSaveHelper.SteamLobbyId.Value = param.m_ulSteamIDLobby;

        SteamAccountManager.ResetSpoofAccountsInUse();
    }

    [HarmonyPatch(nameof(SteamLobbyHandler.OnLobbyChat))]
    [HarmonyPrefix]
    private static bool OnLobbyChatPatch(ref LobbyChatMsg_t param)
    {
        if (!SteamAccountManager.IsUsingSpoofAccount)
        {
            if (param.m_ulSteamIDUser == SteamUser.GetSteamID().m_SteamID)
            {
                // If message is received from self, swap it out with spoof
                param.m_ulSteamIDUser = SteamAccountManager.SpoofAccount.SteamId;
            }
        }
        return true;
    }
}
