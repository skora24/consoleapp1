using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerManagementSystem
{
    public class GasContainer : Container, IHazardNotifier
    {
        public double Pressure { get; private set; }

        public GasContainer(double maxCapacity, string productType, double pressure) 
            : base("G", maxCapacity, productType)
        {
            Pressure = pressure;
        }

        public override void Unload()
        {
            SetCurrentLoad(CurrentLoad * 0.05); // Leave 5% of the load inside
        }

        public void NotifyHazard(string message)
        {
            Console.WriteLine($"[Hazard Alert] {message} - Container: {SerialNumber}");
        }
    }

    public class RefrigeratedContainer : Container
    {
        public double Temperature { get; private set; }

        public RefrigeratedContainer(double maxCapacity, string productType, double temperature) 
            : base("C", maxCapacity, productType)
        {
            Temperature = temperature;
        }

        public override void Load(double weight, string productType)
        {
            if (CurrentLoad + weight > MaxCapacity)
                throw new Exception("OverfillException: Przekroczono pojemność kontenera.");

            SetCurrentLoad(CurrentLoad + weight);
        }
    }
}