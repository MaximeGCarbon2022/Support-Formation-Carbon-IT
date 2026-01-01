namespace MostFrequentNumber
{
    public static class BadVersion_MostFrequentNumber
    {
        public static void Run()
        {
            int[] array = [1, 3, 2, 1, 4, 1, 3, 2, 3, 3, 3, 2, 2, 2];
            int m = 0;
            int c = 0;

            for (int i = 0; i < array.Length; i++)
            {
                int count = 0;
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[j] == array[i])
                    {
                        count++;
                    }
                }
                if (count > c)
                {
                    c = count;
                    m = array[i];
                }
            }

            Console.WriteLine("Most common number is: " + m);
        }
    }
}