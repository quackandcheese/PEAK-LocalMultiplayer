using HarmonyLib;
using Peak.Network;
using Photon.Realtime;
using System;

namespace LocalMultiplayer.Patches
{
    [HarmonyPatch(typeof(NetworkingUtilities))]
    internal static class NetworkingUtilitiesPatch
    {
        [HarmonyPatch(nameof(NetworkingUtilities.LoadUserID))]
        [HarmonyPrefix]
        private static bool LoadUserIDPatch(ref AuthenticationValues __result)
        {
            __result = new AuthenticationValues
            {
                AuthType = CustomAuthenticationType.None,
                UserId = Guid.NewGuid().ToString()
            };
            return false;
        }
    }
}
