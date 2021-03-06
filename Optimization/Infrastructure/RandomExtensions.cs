﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Optimization.Infrastructure
{
    public static class RandomExtensions
    {
        /// <summary>
        /// Возвращает список неодинаковых целых чисел длиной <see cref="count"/>.
        /// </summary>
        public static List<int> GetDistinctRandomNumbers(this Random random, int count, int minValue, int maxValue)
        {
            if (maxValue <= minValue) throw new ArgumentException();
            if (count > maxValue - minValue) throw new ArgumentException();
            var result = new List<int>(count);
            for (int i = 0; i < count; i++)
            {
                var num = random.Next(minValue, maxValue);
                while (result.Any(x => x.Equals(num)))
                    num = random.Next(minValue, maxValue);
                result.Add(num);
            }

            return result;
        }

        /// <summary>
        /// Возвращает случаное число с плавающей запятой в отрезке [<see cref="minValue"/>, <see cref="maxValue"/>].
        /// </summary>
        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            if (maxValue <= minValue) throw new ArgumentException();
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }

        /// <summary>
        /// Возвращает перечисление из элементов <see cref="list"/> длиной <see cref="count"/> в случайном порядке.
        /// Элементы могут повторяться.
        /// </summary>
        public static IEnumerable<T> GetRandomFrom<T>(this Random random, IList<T> list, int count)
        {
            if (count <= 0) throw new ArgumentException();
            if (list?.Count == 0) throw new ArgumentException();

            for (int i = 0; i < count; i++)
            {
                var randomIndex = random.Next(0, list.Count);
                yield return list[randomIndex];
            }
        }

        /// <summary>
        /// Возвращает случайный элемент из коллекции всех доступных элементов,
        /// исключая возможность выбора элемента входящего в список исключений.
        /// </summary>
        public static T GetRandomFrom<T>(this Random random, IList<T> list, IList<T> excludeList)
        {
            list = list.Except(excludeList).ToList();
           
            if (list.Count == 1)
                return list[0];
            
            var randomIndex = random.Next(0, list.Count);
            return list[randomIndex];
        }


        public static IEnumerable<T> RandomSort<T>(this Random random, ICollection<T> enumerable)
        {
            var indexes = random.GetDistinctRandomNumbers(enumerable.Count, 0, enumerable.Count);
            return indexes.Select(enumerable.ElementAt);
        }
    }
}