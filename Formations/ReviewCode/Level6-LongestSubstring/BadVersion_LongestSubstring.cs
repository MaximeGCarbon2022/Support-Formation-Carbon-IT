namespace LongestSubstringWithoutRepeatingCharacters
{
    public static class BadVersion_LongestSubstring
    {
        public static int LengthOfLongestSubstring(string s)
        {
            int x = 0;
            int y = 0;
            Dictionary<char, int> dict = [];

            for (int i = 0; i < s.Length; i++)
            {
                if (dict.ContainsKey(s[i]) && dict[s[i]] >= y)
                {
                    y = dict[s[i]] + 1;
                }
                dict[s[i]] = i;
                int len = i - y + 1;
                if (len > x)
                {
                    x = len;
                }
            }

            return x;
        }
    }
}
