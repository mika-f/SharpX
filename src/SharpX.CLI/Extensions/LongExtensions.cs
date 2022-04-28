namespace SharpX.CLI.Extensions
{
    internal static class LongExtensions
    {
        public static string ToReadableString(this long elapsed)
        {
            if (elapsed < 1000)
                return $"{elapsed} ms";
            if (elapsed < 1000 * 60)
                return $"{elapsed / 1000} s";
            return $"{elapsed / 1000 * 60} m";
        }
    }
}
