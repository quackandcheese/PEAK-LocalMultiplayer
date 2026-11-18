using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using UnityEngine;

namespace com.quackandcheese.LocalMultiplayer.Helpers;

internal static class PhotonNetworkHelper
{
    public static bool HasCustomAppIds => !string.IsNullOrEmpty(ConfigManager.Photon_AppIdRealtime?.Value);

    public static void SetPhotonServerSettings()
    {
        AppSettings appSettings = PhotonNetwork.PhotonServerSettings.AppSettings;
        appSettings.AppIdRealtime = ConfigManager.Photon_AppIdRealtime.Value;
        appSettings.AppIdVoice = ConfigManager.Photon_AppIdVoice.Value;
    }

    // Real SteamID for the host instance, spoof SteamID once this instance has taken a spoof account.
    public static string CurrentInstanceUserId()
    {
        if (SteamAccountManager.IsUsingSpoofAccount)
        {
            return SteamAccountManager.SpoofAccount.SteamId.ToString();
        }

        if (SteamAccountManager.RealAccount.SteamId != 0)
        {
            return SteamAccountManager.RealAccount.SteamId.ToString();
        }

        return Steamworks.SteamUser.GetSteamID().m_SteamID.ToString();
    }

    // Fresh AuthenticationValues (no Steam ticket, no cached token) so a self-made Photon app accepts the login
    // and each instance gets its own UserId.
    public static void ApplyLocalAuthValues(string context)
    {
        string userId = CurrentInstanceUserId();
        PhotonNetwork.AuthValues = new AuthenticationValues
        {
            AuthType = CustomAuthenticationType.None,
            UserId = userId
        };
        // Only the last 4 characters of the App ID, so logs shared in bug reports don't leak it.
        string appId = ConfigManager.Photon_AppIdRealtime.Value;
        string maskedAppId = appId.Length > 4 ? "***" + appId.Substring(appId.Length - 4) : "***";
        Plugin.Log.LogInfo($"{context}: app {maskedAppId}, AuthType None, UserId {userId}");
    }

    // The joining instance was connected under the real SteamID; reconnect with its spoof UserId, wait until
    // Photon is ready again (NetworkConnector refuses JoinSpecificRoomState while not ready), then join.
    public static IEnumerator ReconnectAsSpoofThenJoin(Action join)
    {
        Plugin.Log.LogInfo($"Reconnecting to Photon as {CurrentInstanceUserId()} (state {PhotonNetwork.NetworkClientState})");

        float waited = 0f;
        if (PhotonNetwork.NetworkClientState != ClientState.Disconnected && PhotonNetwork.NetworkClientState != ClientState.PeerCreated)
        {
            PhotonNetwork.Disconnect();
            while (PhotonNetwork.NetworkClientState != ClientState.Disconnected && waited < 10f)
            {
                waited += Time.unscaledDeltaTime;
                yield return null;
            }
        }

        if (!PhotonNetwork.ConnectUsingSettings())
        {
            Plugin.Log.LogError($"ConnectUsingSettings failed (state {PhotonNetwork.NetworkClientState})");
        }

        waited = 0f;
        while (!PhotonNetwork.IsConnectedAndReady && waited < 30f)
        {
            waited += Time.unscaledDeltaTime;
            yield return null;
        }

        Plugin.Log.LogInfo($"Photon ready={PhotonNetwork.IsConnectedAndReady} state={PhotonNetwork.NetworkClientState} region={PhotonNetwork.CloudRegion} userId={PhotonNetwork.AuthValues?.UserId}; joining lobby");
        join();
    }
}
