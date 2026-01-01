namespace Anagrams
{
    public static class GoodVersionAnagrams
    {
        public static bool AreAnagrams(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
            {
                return false;
            }

            if (s1 == s2)
            {
                return true;
            }
            
            if(s1.Length != s2.Length)
            {
                return false;
            }

            var arr1 = s1.ToCharArray();
            var arr2 = s2.ToCharArray();

            Array.Sort(arr1);
            Array.Sort(arr2);

            return arr1.SequenceEqual(arr2);
        }
    }
}
