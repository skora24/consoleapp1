public class Ship
{
    public string Name { get; private set; }
    public double MaxSpeed { get; private set; } 
    public int MaxContainerCount { get; private set; }
    public double MaxWeight { get; private set; } 
    public List<Container> Containers { get; private set; } = new();

    public Ship(string name, double maxSpeed, int maxContainerCount, double maxWeight)
    {
        Name = name;
        MaxSpeed = maxSpeed;
        MaxContainerCount = maxContainerCount;
        MaxWeight = maxWeight;
    }

    public void AddContainer(Container container)
    {
        if (Containers.Count >= MaxContainerCount || GetTotalWeight() + container.CurrentLoad / 1000 > MaxWeight)
        {
            throw new Exception("Nie można dodać kontenera: przekroczono ładowność statku.");
        }
        Containers.Add(container);
    }

    public void RemoveContainer(string serialNumber)
    {
        var container = Containers.Find(c => c.SerialNumber == serialNumber);
        if (container == null) throw new Exception("Nie znaleziono kontenera.");
        Containers.Remove(container);
    }

    public double GetTotalWeight()
    {
        double totalWeight = 0;
        foreach (var container in Containers)
        {
            totalWeight += container.CurrentLoad;
        }
        return totalWeight / 1000; 
    }

    public override string ToString() => 
        $"Ship: {Name}, MaxSpeed: {MaxSpeed} knots, MaxContainerCount: {MaxContainerCount}, MaxWeight: {MaxWeight} tons, Current Load: {GetTotalWeight()} tons";
}