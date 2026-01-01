namespace TwoSum
{
    public static class BadVersion_TwoSum
    {
        public static int[] Run(int[] n, int t)
        {
            int i1 = 0;
            int i2 = 0;

            for (int i = 0; i < n.Length; i++)
            {
                for (int j = 0; j < n.Length; j++)
                {
                    if (i != j && n[i] + n[j] == t)
                    {
                        i1 = i;
                        i2 = j;
                    }
                }
            }

            return [i1, i2];
        }
    }
}
