using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCR.Library.Helpers
{
    public static class NumberHelper
    {
        public static void BubbleSort(ref int[] array)
        {
            var count = array.Length;

            for (var i = 1; i < count; i++)
            {
                for (var j = 0; j < count - i; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        var tmp = array[j];

                        array[j] = array[j + 1];
                        array[j + 1] = tmp;
                    }
                }
            }
        }
    }
}
