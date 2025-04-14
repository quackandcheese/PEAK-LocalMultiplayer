namespace com.github.zehsteam.LocalMultiplayer.Helpers;

internal static class SteamHelper
{
    public static ulong GenerateRandomSteamId()
    {
        // SteamID64 base value for individual users
        ulong steamIdBase = 76561197960265728;
        
        // Generate a random 32-bit unsigned integer
        var random = new System.Random();
        uint randomPart = (uint)random.Next(0, int.MaxValue);
        randomPart += (uint)random.Next(0, int.MaxValue);

        return steamIdBase + randomPart;
    }
}
