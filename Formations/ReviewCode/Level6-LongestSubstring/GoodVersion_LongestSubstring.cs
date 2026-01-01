namespace LongestSubstringWithoutRepeatingCharacters
{
    public static class GoodVersionLongestSubstring
    {
        public static int LengthOfLongestSubstring(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            return ComputeMaxUniqueSubstringLength(s);
        }

        private static int ComputeMaxUniqueSubstringLength(string s)
        {
            int maxLength = 0;
            int startIndex = 0;
            var lastSeen = new Dictionary<char, int>();

            for (int i = 0; i < s.Length; i++)
            {
                char currentChar = s[i];

                if (lastSeen.TryGetValue(currentChar, out int prevIndex) && prevIndex >= startIndex)
                {
                    startIndex = prevIndex + 1;
                }

                lastSeen[currentChar] = i;

                int currentLength = i - startIndex + 1;
                maxLength = Math.Max(maxLength, currentLength);
            }

            return maxLength;
        }
    }
}
