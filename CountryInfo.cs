namespace ASEANPhonebook;

public static class CountryInfo
{
    private static readonly Dictionary<string, string> Countries = new()
    {
        ["60"] = "Republic of Malaysia",
        ["62"] = "Republic of Indonesia",
        ["63"] = "Republic of the Philippines",
        ["65"] = "Republic of Singapore",
        ["66"] = "Kingdom of Thailand"
    };

    public static bool IsValid(string code)
    {
        return Countries.ContainsKey(code);
    }

    public static string GetName(string code)
    {
        if (Countries.TryGetValue(code, out string? name))
        {
            return name;
        }

        return "Unknown country";
    }
}