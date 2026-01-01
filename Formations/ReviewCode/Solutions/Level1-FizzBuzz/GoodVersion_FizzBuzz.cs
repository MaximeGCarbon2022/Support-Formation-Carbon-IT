namespace FizzBuzz
{
    public static class GoodVersion_FizzBuzz
    {
        public static void Run(int max = 100)
        {
            for (int i = 1; i <= max; i++)
            {
                Console.WriteLine(GetFizzBuzzValue(i));
            }
        }

        private static string GetFizzBuzzValue(int i)
        {
            string result = "";

            if (i % 3 == 0)
            {
                result += "Fizz";
            }

            if (i % 5 == 0)
            {
                result += "Buzz";
            }

            return string.IsNullOrEmpty(result) ? i.ToString() : result;
        }
    }
}
