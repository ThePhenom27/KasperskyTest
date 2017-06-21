using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KasperskyTest
{
    static class SumFind
    {
        /*
         * В исходной задаче не говорилось про сортировку и уникальность значений
         * Искать суммы среди повторяющихся значений не имеет смысла, а для нашего
         * алгоритма нам потребуется отсортировать список
         * */
        public static void FindSum(int[] array, int sum)
        {
            HashSet<int> items = new HashSet<int>(array);
            array = items.ToArray();
            Array.Sort(array);
            FindSumSorted(array, sum);
        }

        /*
         * Задача на поиск чисел заданной суммы в отсортированном массиве решается двумя указателями
         * */
        public static void FindSumSorted(int[] array, int sum)
        {
            int bottom = 0, top = array.Length - 1; // Один указатель на первый элемент массива, второй на последний
            while (bottom < top) // В алгоритме мы будем их приближать, пока они не столкнутся
            {
                var currSum = array[bottom] + array[top]; // Текущая сумма
                if (currSum == sum) // Числа подходят
                {
                    Console.WriteLine("{0} + {1}", array[bottom], array[top]);
                    bottom++; // У нас числа не дублируются не имеет смысла сдвигать только верхнее или только нижнее значение
                    top--;    // ... т.к. сумма в таком случае будет заведомо больше или заведомо меньше
                }
                else if (currSum < sum) // Сумма получилась меньше => сдвиг нижней границы поможет сделать ее больше
                    bottom++;
                else // И наоборот, если сумма больше нужной, сдвиг верхней границы сделает ее меньше
                    top--;
            }
        }

        /**
         * Для поиска индексов в несортированном неуникальном массиве задача решается перебором массива в двух циклах
         * */
        public static void FindSumUnsorted(int[] array, int sum)
        {
            for (int i = 0; i < array.Length - 1; i++)
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (array[i] + array[j] == sum)
                        Console.WriteLine("[{0}] {1} + [{2}] {3}", i, array[i], j, array[j]);
                }
        }
    }
}
