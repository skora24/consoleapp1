using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerManagementSystem
{
    public static class ConsoleInterface
    {
        private static List<Ship> Ships = new();
        private static List<Container> Containers = new();

        public static void Start()
        {
            while (true)
            {
                Console.WriteLine("\n--- Container Management System ---");
                Console.WriteLine("1. Dodaj statek");
                Console.WriteLine("2. Dodaj kontener");
                Console.WriteLine("3. Załaduj kontener na statek");
                Console.WriteLine("4. Rozładuj kontener ze statku");
                Console.WriteLine("5. Wyświetl informacje o statkach");
                Console.WriteLine("6. Wyświetl informacje o kontenerach");
                Console.WriteLine("7. Wyjście");
                Console.Write("Wybierz co chcesz zrobić: ");

                if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 7)
                {
                    Console.WriteLine("Nieprawidłowy wybór! Spróbuj ponownie.");
                    continue;
                }

                if (choice == 7) break;

                try
                {
                    switch (choice)
                    {
                        case 1: AddShip(); break;
                        case 2: AddContainer(); break;
                        case 3: LoadContainerToShip(); break;
                        case 4: UnloadContainerFromShip(); break;
                        case 5: DisplayShipInfo(); break;
                        case 6: DisplayContainerInfo(); break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private static void AddShip()
        {
            Console.Write("Wprowadź nazwę statku: ");
            string name = Console.ReadLine();

            Console.Write("Wprowadź prędkośc maksymalną (w węzłach): ");
            double maxSpeed = double.Parse(Console.ReadLine());

            Console.Write("Wprowadź maksymalną ilość kontenerów: ");
            int maxCount = int.Parse(Console.ReadLine());

            Console.Write("Wprowadź maksymalną wagę (w tonach): ");
            double maxWeight = double.Parse(Console.ReadLine());

            Ships.Add(new Ship(name, maxSpeed, maxCount, maxWeight));
            Console.WriteLine("Statek został pomyślnie dodany.");
        }

        private static void AddContainer()
        {
            Console.WriteLine("Wybierz typ konteneru: 1. Na Płyny, 2. Na Gaz, 3. Chłodniczy");
            int type = int.Parse(Console.ReadLine());

            Console.Write("Wprowadź maksymalną pojemność (w kg): ");
            double maxCapacity = double.Parse(Console.ReadLine());

            Console.Write("Wprowadz typ prodyktu: ");
            string productType = Console.ReadLine();

            Container container = null;

            switch (type)
            {
                case 1:
                    Console.Write("Czy ładunek jest niebezpieczny? (tak/nie): ");
                    bool isHazardous = Console.ReadLine().ToLower() == "tak";
                    container = new LiquidContainer(maxCapacity, productType, isHazardous);
                    break;

                case 2:
                    Console.Write("Wprowadź ciśnienie (w atmosferach): ");
                    double pressure = double.Parse(Console.ReadLine());
                    container = new GasContainer(maxCapacity, productType, pressure);
                    break;

                case 3:
                    Console.Write("Wprowadź temperaturę (w stopniach Celsjusza): ");
                    double temperature = double.Parse(Console.ReadLine());
                    container = new RefrigeratedContainer(maxCapacity, productType, temperature);
                    break;
            }

            if (container != null)
            {
                Containers.Add(container);
                Console.WriteLine("Kontener został pomyślnie dodany. ");
            }
        }

        private static void LoadContainerToShip()
        {
            Console.Write("Wprowadź numer kontenera: ");
            string serial = Console.ReadLine();
            var container = Containers.FirstOrDefault(c => c.SerialNumber == serial);

            if (container == null)
            {
                Console.WriteLine("Nie znaleziono kontenera z podanym numerem.");
                return;
            }

            Console.Write("Wprowadz nazwe statku: ");
            string shipName = Console.ReadLine();
            var ship = Ships.FirstOrDefault(s => s.Name == shipName);

            if (ship == null)
            {
                Console.WriteLine("Nie znaleziono takiego statku.");
                return;
            }

            ship.AddContainer(container);
            Console.WriteLine("Kontener załadowany pomyślnie.");
        }

        private static void UnloadContainerFromShip()
        {
            Console.Write("Wprowadz nazwe statku: ");
            string shipName = Console.ReadLine();

            var ship = Ships.FirstOrDefault(s => s.Name == shipName);

            if (ship == null)
            {
                Console.WriteLine("Nie znaleziono takiego statku.");
                return;
            }

            Console.Write("Wprowadz numer kontenera: ");
            string serial = Console.ReadLine();

            ship.RemoveContainer(serial);
            Console.WriteLine("Kontener pomyślnie rozładowany.");
        }

        private static void DisplayShipInfo()
        {
            foreach (var ship in Ships)
            {
                Console.WriteLine(ship);
            }
        }

        private static void DisplayContainerInfo()
        {
            foreach (var container in Containers)
            {
                Console.WriteLine(container);
            }
        }
    }
}
