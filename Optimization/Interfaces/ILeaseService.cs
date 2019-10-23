﻿using System.Collections.Generic;

namespace Optimization.Interfaces
{
    /// <summary>
    /// Сервис аренды транспорта.
    /// </summary>
    public interface ILeaseService
    {
        /// <summary>
        /// Доступные для аренды ТС, их количество и цена аренды 1 штуки за 1 день.
        /// </summary>
        Dictionary<IVehicle, (int, decimal)> AvailableVehicles { get; }
    }
}