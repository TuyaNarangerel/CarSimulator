using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarSimulator;
using System;
using System.IO;

namespace CarSimulatorTests
{
    [TestClass]
    public class CarTests
    {
        [TestMethod]
        public void Test_Initial_Fuel()
        {
            // ARRANGE
            var car = new Car();

            // ACT
            int initialFuel = car.Fuel;

            // ASSERT
            Assert.AreEqual(20, initialFuel);
        }

        [TestMethod]
        public void Test_Turn_Left()
        {
            // ARRANGE
            var car = new Car();

            // ACT
            car.TurnLeft();
            Direction newDirection = car.Direction;

            // ASSERT
            Assert.AreEqual(Direction.West, newDirection);
        }

        [TestMethod]
        public void Test_Turn_Right()
        {
            // ARRANGE
            var car = new Car();

            // ACT
            car.TurnRight();
            Direction newDirection = car.Direction;

            // ASSERT
            Assert.AreEqual(Direction.East, newDirection);
        }

        [TestMethod]
        public void Test_Move_Forward()
        {
            // ARRANGE
            var car = new Car();

            // ACT
            car.MoveForward();
            int remainingFuel = car.Fuel;

            // ASSERT
            Assert.AreEqual(19, remainingFuel);
        }

        [TestMethod]
        public void Test_Move_Backward()
        {
            // ARRANGE
            var car = new Car();

            // ACT
            car.MoveBackward();
            int remainingFuel = car.Fuel;

            // ASSERT
            Assert.AreEqual(19, remainingFuel);
        }

        [TestMethod]
        public void Test_Refuel()
        {
            // ARRANGE
            var car = new Car();
            car.MoveForward();

            // ACT
            car.Refuel();
            int remainingFuel = car.Fuel;

            // ASSERT
            Assert.AreEqual(20, remainingFuel);
        }

        [TestMethod]
        public void Test_Rest()
        {
            // ARRANGE
            var car = new Car();
            car.MoveForward();

            // ACT
            car.Rest();
            int driverFatique = car.DriverFatique;

            // ASSERT
            Assert.AreEqual(0, driverFatique);
        }

        [TestMethod]
        public void Test_Increase_Fatique()
        {
            // ARRANGE
            var car = new Car();

            // ACT
            car.MoveForward();
            int driverFatique = car.DriverFatique;

            // ASSERT
            Assert.AreEqual(1, driverFatique);
        }

        [TestMethod]
        public void Test_Max_Fatique_Warning()
        {
            // ARRANGE
            var car = new Car();
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // ACT
                for (int i = 0; i < Car.MaxFatique; i++)
                {
                    car.MoveForward();
                }

                // ASSERT
                var result = sw.ToString();
                Assert.AreEqual(Car.MaxFatique, car.DriverFatique);
                Assert.IsTrue(result.Contains("The driver is exhausted! Time for a mandatory break!"));
            }
        }

        [TestMethod]
        public void Test_Turn_Left_Twice()
        {
            // ARRANGE
            var car = new Car();

            // ACT
            car.TurnLeft();
            car.TurnLeft();
            Direction newDirection = car.Direction;

            // ASSERT
            Assert.AreEqual(Direction.South, newDirection);
        }

        [TestMethod]
        public void Test_Turn_Right_Twice()
        {
            // ARRANGE
            var car = new Car();

            // ACT
            car.TurnRight();
            car.TurnRight();
            Direction newDirection = car.Direction;

            // ASSERT
            Assert.AreEqual(Direction.South, newDirection);
        }

        [TestMethod]
        public void Test_Turn_Left_Four_Times()
        {
            // ARRANGE
            var car = new Car();

            // ACT
            for (int i = 0; i < 4; i++)
            {
                car.TurnLeft();
            }
            Direction newDirection = car.Direction;

            // ASSERT
            Assert.AreEqual(Direction.North, newDirection);
        }

        [TestMethod]
        public void Test_Turn_Right_Four_Times()
        {
            // ARRANGE
            var car = new Car();

            // ACT
            for (int i = 0; i < 4; i++)
            {
                car.TurnRight();
            }
            Direction newDirection = car.Direction;

            // ASSERT
            Assert.AreEqual(Direction.North, newDirection);
        }

        [TestMethod]
        public void Test_Move_Forward_No_Fuel()
        {
            // ARRANGE
            var car = new Car();
            for (int i = 0; i < Car.MaxFuel; i++)
            {
                car.MoveForward();
            }

            // ACT
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                car.MoveForward();
                var result = sw.ToString();

                // ASSERT
                Assert.IsTrue(result.Contains("Oops! We're out of fuel. Time to fill up the tank!"));
            }
            Assert.AreEqual(0, car.Fuel);
        }

        [TestMethod]
        public void Test_Move_Backward_No_Fuel()
        {
            // ARRANGE
            var car = new Car();
            for (int i = 0; i < Car.MaxFuel; i++)
            {
                car.MoveForward();
            }

            // ACT
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                car.MoveBackward();
                var result = sw.ToString();

                // ASSERT
                Assert.IsTrue(result.Contains("Oops! We're out of fuel. Time to fill up the tank!"));
            }
            Assert.AreEqual(0, car.Fuel);
        }

        [TestMethod]
        public void Test_Initial_Direction()
        {
            // ARRANGE
            var car = new Car();

            // ACT
            Direction initialDirection = car.Direction;

            // ASSERT
            Assert.AreEqual(Direction.North, initialDirection);
        }

        [TestMethod]
        public void Test_Rest_Resets_Fatique()
        {
            // ARRANGE
            var car = new Car();
            for (int i = 0; i < 5; i++)
            {
                car.MoveForward();
            }

            // ACT
            car.Rest();
            int driverFatique = car.DriverFatique;

            // ASSERT
            Assert.AreEqual(0, driverFatique);
        }

        [TestMethod]
        public void Test_Refuel_After_Driving()
        {
            // ARRANGE
            var car = new Car();
            for (int i = 0; i < 5; i++)
            {
                car.MoveForward();
            }

            // ACT
            car.Refuel();
            int remainingFuel = car.Fuel;

            // ASSERT
            Assert.AreEqual(Car.MaxFuel, remainingFuel);
        }

        [TestMethod]
        public void Test_Fatique_Warning()
        {
            // ARRANGE
            var car = new Car();
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // ACT
                for (int i = 0; i < Car.MaxFatique / 2 + 1; i++)
                {
                    car.MoveForward();
                }
                var result = sw.ToString();

                // ASSERT
                Assert.IsTrue(result.Contains("The driver is getting sleepy... Maybe take a break soon?"));
            }
        }

        [TestMethod]
        public void Test_Full_Cycle()
        {
            // ARRANGE
            var car = new Car();

            // ACT
            car.MoveForward();
            car.TurnLeft();
            car.MoveForward();
            car.TurnRight();
            car.MoveBackward();
            car.Refuel();
            car.Rest();

            // ASSERT
            Assert.AreEqual(Direction.North, car.Direction);
            Assert.AreEqual(Car.MaxFuel, car.Fuel);
            Assert.AreEqual(0, car.DriverFatique);
        }

        [TestMethod]
        public void Test_Driver_Fatigue_Exceeds_Max()
        {
            // ARRANGE
            var car = new Car();
            for (int i = 0; i < Car.MaxFatique; i++)
            {
                car.MoveForward();
            }

            // ACT
            car.MoveForward();
            int driverFatique = car.DriverFatique;

            // ASSERT
            Assert.AreEqual(Car.MaxFatique, driverFatique);
        }

        [TestMethod]
        public void Test_Car_Refuel_After_Empty()
        {
            // ARRANGE
            var car = new Car();
            for (int i = 0; i < Car.MaxFuel; i++)
            {
                car.MoveForward();
            }

            // ACT
            car.Refuel();
            int remainingFuel = car.Fuel;

            // ASSERT
            Assert.AreEqual(Car.MaxFuel, remainingFuel);
        }

        [TestMethod]
        public void Test_Car_Turns_Full_Circle()
        {
            // ARRANGE
            var car = new Car();

            // ACT
            car.TurnRight();
            car.TurnRight();
            car.TurnRight();
            car.TurnRight();
            Direction finalDirection = car.Direction;

            // ASSERT
            Assert.AreEqual(Direction.North, finalDirection);
        }

        [TestMethod]
        public void Test_Car_Commands()
        {
            // ARRANGE
            var car = new Car();
            var commands = new[] { "1", "2", "3", "4", "5", "6", "7" };

            // ACT and ASSERT
            foreach (var command in commands)
            {
                using (var sw = new StringWriter())
                {
                    Console.SetOut(sw);
                    ExecuteCommand(car, command);
                    string result = sw.ToString();
                    Assert.IsNotNull(result);
                }
            }
        }

        private void ExecuteCommand(Car car, string command)
        {
            switch (command)
            {
                case "1":
                    car.TurnLeft();
                    break;
                case "2":
                    car.TurnRight();
                    break;
                case "3":
                    car.MoveForward();
                    break;
                case "4":
                    car.MoveBackward();
                    break;
                case "5":
                    car.Rest();
                    break;
                case "6":
                    car.Refuel();
                    break;
                case "7":
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    break;
            }
        }
    }
}