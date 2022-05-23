using System.Text.RegularExpressions;

namespace GunStoreDesktop.Util;

public static class StringsUtil
{
    private static readonly Regex regex = new Regex("^[a-zA-Z1-9]+$");

    public static bool isAllowed(string text)
    {
        return regex.IsMatch(text);
    }
}