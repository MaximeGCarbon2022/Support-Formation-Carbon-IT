namespace Anagrammes
{
    public static class BadVersion_Anagrammes
    {
        public static bool AreAnagrams(string s1, string s2)
        {
            if (s1.Length != s2.Length)
                return false;

            foreach (char c in s1)
            {
                if (!s2.Contains(c))
                    return false;
            }

            return true;
        }
    }
}

