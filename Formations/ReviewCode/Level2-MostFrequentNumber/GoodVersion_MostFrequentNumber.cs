namespace MostFrequentNumber;

public static class GoodVersion_MostFrequentNumber
{
    public static void Run()
    {
        int[] array = [1, 3, 2, 1, 4, 1, 3, 2, 3, 3, 3, 2, 2, 2];
        var mostCommon = GetMostFrequentNumber(array);

        if (mostCommon.HasValue)
        {
            Console.WriteLine($"Most common number is: {mostCommon.Value}");
        }
        else
        {
            Console.WriteLine("Array is empty or null.");
        }
    }

    public static int? GetMostFrequentNumber(int[] array)
    {
        if (array == null || array.Length == 0)
        {
            return null;
        }

        return array
            .CountBy(x => x)
            .MaxBy(kvp => kvp.Value)
            .Key;
    }
}

