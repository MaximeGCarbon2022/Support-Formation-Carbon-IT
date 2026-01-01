namespace ReverseString
{
	public static class CleanStringReverser
	{
		public static string Reverse(string input)
		{
			if (string.IsNullOrEmpty(input))
				return input ?? string.Empty;

			var chars = new char[input.Length];
			for (int i = 0, j = input.Length - 1; i < input.Length; i++, j--)
			{
				chars[i] = input[j];
			}

			return new string(chars);
		}
	}
}