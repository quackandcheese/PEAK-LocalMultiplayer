/*using com.quackandcheese.LocalMultiplayer;
using HarmonyLib;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Zorro.Core;

namespace com.quackandcheese.LocalMultiplayer.Patches;

[HarmonyPatch(typeof(SteamAuthTicketService))]
internal static class SteamAuthTicketServicePatch
{
    [HarmonyPatch(nameof(SteamAuthTicketService.GenerateNewTicket))]
    [HarmonyPostfix]
    private static void GenerateNewTicketPatch()
    {
        if (!IsValidClient())
        {
            Plugin.Log.LogError("Invalid Steam client detected. Exiting application to prevent issues.");

            Application.Quit();
        }
    }

    private static bool IsValidClient()
    {
        Optionable<SteamAuthTicketService.GeneratedTicket> authTicket = GameHandler.GetService<SteamAuthTicketService>().CurrentTicket;

        if (!authTicket.IsSome)
        {
            return false;
        }

        var convertedTicket = HexStringToByteArray(authTicket.Value.TicketData);
        EBeginAuthSessionResult beginAuthResult = SteamUser.BeginAuthSession(convertedTicket, convertedTicket.Length, SteamUser.GetSteamID());
        return beginAuthResult == EBeginAuthSessionResult.k_EBeginAuthSessionResultOK;
    }

    public static byte[] HexStringToByteArray(string hex)
    {
        if (hex.Length % 2 != 0)
            throw new ArgumentException("Invalid length for a hex string.");

        byte[] result = new byte[hex.Length / 2];
        for (int i = 0; i < result.Length; i++)
        {
            string byteValue = hex.Substring(i * 2, 2);
            result[i] = Convert.ToByte(byteValue, 16);
        }
        return result;
    }
}

*/