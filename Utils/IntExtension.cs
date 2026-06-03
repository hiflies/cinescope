namespace CineScope.Utils;

public static class IntExtension
{
    extension(int minutes)
    {
        public string ToTimeString()
        {
            var hours = minutes / 60;
            minutes -= (hours * 60);

            return hours == 0 ? $"{minutes}m" : $"{hours}h {minutes}m";
        }

        public string ToCountString()
        {
            return minutes switch
            {
                >= 1_000_000 => $"{minutes / 1_000_000} million",
                >= 1_000 => $"{minutes / 1_000}k",
                _ => minutes.ToString()
            };
        }
    }
}