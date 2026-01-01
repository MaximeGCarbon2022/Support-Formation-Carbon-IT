namespace TwoSum
{
    public static class GoodVersionTwoSum
    {
        public static int[] TwoSum(int[] numbers, int target)
        {
            var numberToIndex = new Dictionary<int, int>();

            for (int i = 0; i < numbers.Length; i++)
            {
                int complement = target - numbers[i];

                if (numberToIndex.TryGetValue(complement, out int complementIndex))
                {
                    return [complementIndex, i];
                }

                numberToIndex[numbers[i]] = i;
            }

            throw new ArgumentException("No two sum solution found.");
        }
    }
}
