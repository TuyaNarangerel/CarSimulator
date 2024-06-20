namespace CarSimulator
{
    public enum Direction
    {
        North,
        South,
        West,
        East
    }

    public class Car
    {
        public Direction Direction { get; set; }
        public int Fuel { get; private set; } = 20;
        public int DriverFatique { get; private set; } = 0;
        public const int MaxFatique = 10;
        public const int MaxFuel = 20;

        public Car()
        {
            Direction = Direction.North;
        }

        public void TurnLeft()
        {
            IncreaseFatique();
            Direction = Direction switch
            {
                Direction.North => Direction.West,
                Direction.West => Direction.South,
                Direction.South => Direction.East,
                Direction.East => Direction.North,
                _ => Direction
            };
        }

        public void TurnRight()
        {
            IncreaseFatique();
            Direction = Direction switch
            {
                Direction.North => Direction.East,
                Direction.East => Direction.South,
                Direction.South => Direction.West,
                Direction.West => Direction.North,
                _ => Direction
            };
        }

        public void MoveForward()
        {
            if (Fuel <= 0)
            {
                Console.WriteLine("Oops! We're out of fuel. Time to fill up the tank!");
                return;
            }

            IncreaseFatique();
            Fuel--;
        }

        public void MoveBackward()
        {
            if (Fuel <= 0)
            {
                Console.WriteLine("Oops! We're out of fuel. Time to fill up the tank!");
                return;
            }

            IncreaseFatique();
            Fuel--;
            
        }

        public void Refuel()
        {
            Console.WriteLine("Refueling... The car is happy and full again!");
            Fuel = MaxFuel;
        }

        public void Rest()
        {
            Console.WriteLine("The driver is taking a nap... Zzz...");
            DriverFatique = 0;
        }

        private void IncreaseFatique()
        {
            if (DriverFatique < MaxFatique)
            {
                DriverFatique++;
            }

            if (DriverFatique >= MaxFatique)
            {
                Console.WriteLine("The driver is exhausted! Time for a mandatory break!");
            }
            else if (DriverFatique >= MaxFatique / 2)
            {
                Console.WriteLine("The driver is getting sleepy... Maybe take a break soon?");
            }
        }

        public void PrintStatus()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nCar's direction: {Direction}");
            Console.WriteLine($"Fuel: {Fuel}/{MaxFuel}");
            Console.WriteLine($"Driver's fatique: {DriverFatique}/{MaxFatique}");
            Console.ResetColor();
        }
    }
}