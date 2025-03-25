public class LiquidContainer : Container, IHazardNotifier
{
    public bool IsHazardous { get; private set; }

    public LiquidContainer(double maxCapacity, string productType, bool isHazardous) 
        : base("L", maxCapacity, productType)
    {
        IsHazardous = isHazardous;
    }

    public override void Load(double weight, string productType)
    {
        double limit = IsHazardous ? 0.5 * MaxCapacity : 0.9 * MaxCapacity;

        if (weight > limit)
        {
            NotifyHazard("Przekroczono limit ładowania.");
            throw new Exception("OverfillException: Przekroczono bezpieczną ładowność.");
        }

        base.Load(weight, productType);
    }

    public void NotifyHazard(string message)
    {
        Console.WriteLine($"[Hazard Alert] {message} - Container: {SerialNumber}");
    }
}