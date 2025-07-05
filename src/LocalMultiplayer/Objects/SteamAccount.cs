using System;

namespace com.github.quackandcheese.LocalMultiplayer.Objects;

public struct SteamAccount : IEquatable<SteamAccount>
{
    public string Username;
    public ulong SteamId;

    public SteamAccount(string username, ulong steamId)
    {
        Username = username;
        SteamId = steamId;
    }

    public bool Equals(SteamAccount other)
    {
        return SteamId == other.SteamId;
    }

    public override bool Equals(object obj)
    {
        return obj is SteamAccount other && Equals(other);
    }

    public static bool operator ==(SteamAccount a, SteamAccount b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(SteamAccount a, SteamAccount b)
    {
        return !a.Equals(b);
    }

    public override int GetHashCode()
    {
        return SteamId.GetHashCode();
    }
}
