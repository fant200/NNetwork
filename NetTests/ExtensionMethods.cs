using System;

namespace NetTests
{
    public static class ExtensionMethods
    {
        private static Random Random = new Random();
        public static void Randomize(this double[,] array)
        {
            for (int i = 0; i < array.GetLength(0); ++i)
            {
                for (int j = 0; j < array.GetLength(1); ++j)
                {
                    array[i, j] = Random.NextDouble();
                }
            }
        }
    }
}
