namespace Hqub.MusicBrainz.API.Extensions
{
    /// <summary>
    /// Extensions to work with api
    /// </summary>
    /// <see href="https://musicbrainz.org/doc/Artist"/>
    public static class HqubExtensions
    {
        /// <summary>
        /// Escapes a string containing several words.
        /// </summary>
        /// <param name="s">Source line.</param>
        /// <returns></returns>
        public static string Quote(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }

            if (s.IndexOf(' ') < 0)
            {
                return s;
            }

            return "\"" + s + "\"";
        }
    }
}
