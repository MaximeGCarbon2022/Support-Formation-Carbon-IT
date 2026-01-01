using System.Text;

namespace ReverseString
{
    public static class GoodVersionStringReverser
    {
        public static string Run(string input)
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");
            }

            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var result = new StringBuilder();

            for (var i = input.Length - 1; i >= 0; i--)
            {
                result.Append(input[i]);
            }

            return result.ToString();
        }

        public static string Reverse(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length == 1)
            {
                return input;
            }

            var chars = new char[input.Length];

            for (int i = 0, j = input.Length - 1; i < input.Length; i++, j--)
            {
                chars[i] = input[j];
            }

            return new string(chars);
        }

        public static string Linq(string input)
        {
            return input is null ? throw new ArgumentNullException(nameof(input)) : new string([.. input.Reverse()]);
        }
    }
}

