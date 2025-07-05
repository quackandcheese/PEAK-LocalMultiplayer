namespace com.quackandcheese.LocalMultiplayer.Extensions;

internal static class StringExtensions
{
    public static ulong ToUlong(this string value)
    {
        if (ulong.TryParse(value, out ulong result))
        {
            return result;
        }

        return 0;
    }
}
