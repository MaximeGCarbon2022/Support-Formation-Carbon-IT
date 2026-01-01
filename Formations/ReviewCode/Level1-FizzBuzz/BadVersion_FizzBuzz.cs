namespace FizzBuzz
{
    public static class BadVersion_FizzBuzz
    {
        public static void Run()
        {
            for (int i = 1; i <= 100; i++)
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

                Console.WriteLine(result);
            }
        }
    }
}
