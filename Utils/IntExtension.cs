namespace CineScope.Utils;

public static class IntExtension
{
    public static string ToTimeString(this int minutes)
    {
        var hours = minutes / 60;
        var remaining = minutes - (hours * 60);

        return hours == 0 ? $"{remaining}m" : $"{hours}h {remaining}m";
    }

    public static string ToCountString(this int value)
    {
        return value switch
        {
            >= 1_000_000 => $"{value / 1_000_000} million",
            >= 1_000 => $"{value / 1_000}k",
            _ => value.ToString()
        };
    }
}