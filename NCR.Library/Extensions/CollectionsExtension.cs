using NCR.Library.Helpers;

namespace NCR.Library.Extensions
{
    public static class CollectionsExtension
    { 
        public static int[] RemoveDublicates(this int[] array)
        {
            NumberHelper.BubbleSort(ref array);

            var size = array.Length;
            var element = array[0];
            var counter = 1;

            for (var i = 1; i < size; i++)
            {
                if (element == array[i])
                    continue;

                element = array[i];
                counter++;
            }

            var results = new int[counter];

            counter = 0;
            element = array[0];
            results[counter++] = element;

            for (var i = 1; i < size; i++)
            {
                if (element == array[i])
                    continue;

                element = array[i];
                results[counter++] = element;
            }

            return results;
        }
    }
}
