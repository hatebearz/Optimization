﻿using System;
using System.Collections.Generic;
using System.Linq;
using Optimization.DailyModel;
using Optimization.Infrastructure;

namespace Optimization.DataGeneration
{
    /// <summary>
    /// Класс-генератор товаров.
    /// </summary>
    public class GoodGenerator
    {
        private static readonly Random _random = new Random();

        #region Константы для генерации цен.

        /* Все массивы в этом регионе должны иметь равное количество элементов.
         * Подразумивается, что элементы следует от меньшего товара к большему (по размеру). */

        /// <summary>
        /// Массив устанавливает вес на генерации товара определенного размера/цены.
        /// </summary>
        private static readonly int[] GoodGenerationWeights = { 80, 15, 5 };

        /// <summary>
        /// Массив устанавливает границы объема товаров.
        /// </summary>
        private static readonly (double, double)[] VolumeGenerationBorders = 
            {(0.0001, 0.004), (0.004, 0.5), (0.5, 3)};

        /// <summary>
        /// Массив устанавливает границы цены товаров разного объема (большие по объему - стоят больше).
        /// </summary>
        private static readonly (int, int)[] PriceGenerationBorders = 
            {(10, 500), (500, 10000), (7000, 100000)};
        /* Заметка по границам товаров, я понимаю, что какое-нибудь маленькое кольцо может стоить
         * больше громадного холодильника, но для простоты решил так: чем больше товар, тем больше цена. */

        /* В целом, решение с массивами – грязное, возможно, что-то более
         * чистое получилось бы с перечислением, но этот класс для разовой работы,
         * так что пусть будет так. */

        #endregion

        /// <summary>
        /// Генерирует перечисление уникальных товаров.
        /// </summary>
        /// <param name="count">Количество товаров, которое необходимо сгенерировать.</param>
        /// <returns>Перечисление уникальных товаров.</returns>
        public static IEnumerable<Good> GenerateUniqueGoods(int count)
        {
            /*  */
            for (int i = 0; i < count; i++)
            {
                /* Для простоты названия товаров – "товар_[числовой идентификатор]". */
                var name = $"Товар_{i}";
                var (volume, price) = GenerateVolumeAndPrice();
                yield return new Good(volume, (decimal) price, name);
            }
        }

        /// <summary>
        /// Генерирует объем товара и его цену с учётом весов генерации <see cref="GoodGenerationWeights"/>.
        /// </summary>
        /// <returns>Кортеж, где первый элемент – это объем товара в метрах кубических, второй – цена в рублях.</returns>
        private static (double, double) GenerateVolumeAndPrice()
        {
            if(GoodGenerationWeights.Length != VolumeGenerationBorders.Length 
               || GoodGenerationWeights.Length != PriceGenerationBorders.Length)
                throw new Exception();
            
            /* Генерируем "класс" товара, по сути индекс в массивах выше. */
            var goodClassIndex = 0;
            var randomValue = _random.Next(0, GoodGenerationWeights.Sum());
            var rightBorder = GoodGenerationWeights[0];
            for (int i = 0; i < GoodGenerationWeights.Length - 1; i++)
            {
                goodClassIndex = i;
                if(randomValue < rightBorder) break;
                rightBorder += GoodGenerationWeights[i + 1];
            }
            
            var (minVolume, maxVolume) = VolumeGenerationBorders[goodClassIndex];
            var volume = Math.Round(_random.NextDouble(minVolume, maxVolume), 4);

            var (minPrice, maxPrice) = PriceGenerationBorders[goodClassIndex];
            var price = _random.Next(minPrice, maxPrice);

            return (volume, price);
        }
    }
}