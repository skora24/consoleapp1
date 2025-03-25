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
            throw new Exception("OverfillException: Przekroczona pojemność kontenera.");
        
        
        SetCurrentLoad(CurrentLoad + weight);
    }
}