namespace aoc2018.Util
{
    public class StringCompare
    {
        public static int MatchingCharacterCount(string a, string b)
        {
            var result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == b[i])
                {
                    result++;
                }
            }

            return result;
        }

        public static string GetSameCharacters(string a, string b)
        {
            var result = string.Empty;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == b[i])
                {
                    result += a[i];
                }
            }
            return result;
        }
    }
}
