﻿using FluentValidation;
using Optimization.Core;
using Optimization.Validation;

namespace Optimization.DailyModel
{
    public class VehicleModel: IVehicleModel
    {
        public VehicleModel(double capacity, double accelerationTime, (double, double, double) dimensions, 
            VehicleType type, double rentalPrice, string name)
        {
            Capacity = capacity;
            AccelerationTime = accelerationTime;
            Dimensions = dimensions;
            Type = type;
            RentalPrice = rentalPrice;
            Name = name;
            VehicleModelValidator.Instance.ValidateAndThrow(this);
        }

        public string Name { get; }
        public double Capacity { get; }
        public double AccelerationTime { get; }
        public (double, double, double) Dimensions { get; }
        public VehicleType Type { get; }
        public double RentalPrice { get; }
        public override string ToString() => Name;
    }
}