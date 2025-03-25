public abstract class Container
{
    private static int counter = 1;
    public string SerialNumber { get; private set; }
    public double MaxCapacity { get; private set; } 
    public double CurrentLoad { get; private set; } = 0; 
    public string ProductType { get; private set; }

    protected Container(string type, double maxCapacity, string productType)
    {
        SerialNumber = $"KON-{type}-{counter++}";
        MaxCapacity = maxCapacity;
        ProductType = productType;
    }

    public virtual void Load(double weight, string productType)
    {
        if (ProductType != productType)
            throw new Exception("Niekompatybilny typ produktu.");

        if (CurrentLoad + weight > MaxCapacity)
            throw new Exception("Przekroczono pojemność kontenera.");

        CurrentLoad += weight;
    }

    public virtual void Unload()
    {
        CurrentLoad = 0;
    }
    
    public void SetCurrentLoad(double newLoad)
    {
        
    }
    

    public override string ToString() => 
        $"SerialNumber: {SerialNumber}, Type: {ProductType}, MaxCapacity: {MaxCapacity}, CurrentLoad: {CurrentLoad}";
}

