using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPractice
{
    class Program
    {
        public static Random random = new Random();
        delegate bool MyDel();
        static void Main(string[] args)
        {
            Garage garage = new Garage();
            Func<Car, bool> f = (c) =>
            {
                if (c.Brand == Brand.TOYOTA && c.ManufactureDate.Year == 2019) return true;
                return false;
            };
            Print(garage, f);

            Func<Car, bool> f2 = (c) =>
            {
                if (c.Price > 7000 && c.Price < 12000) return true;
                return false;
            };
            Print(garage, f2);

            Func<Car, bool> f3 = (c) =>
            {
                if (c.Price > 7000 && c.Price < 12000) return true;
                return false;
            };
            Print(garage, f3);
            Console.WriteLine(garage.GetMinimumPriceCar());
            Console.Read();
        }

        public static void Print(Garage g, Func<Car, bool> func)
        {
            foreach (var c in g.GetCars(func))
            {
                Console.WriteLine(c);
            }
        }
    }
    class Garage
    {
        public List<Car> Cars = new List<Car>();
        public Garage()
        {
            for (int i = 0; i < 50; i++)
                Cars.Add(Car.Generate());
        }

        public void AddCar(Car car)
        {
            Cars.Add(car);
        }

        public void RemoveCar(Car car)
        {
            Cars.Remove(car);
        }
        public Car GetMinimumPriceCar()
        {
            return Cars.OrderBy(car => car.Price).First() ;
        }

        public List<Car> GetCars(Func<Car, bool> func)
        {
            List<Car> list = new List<Car>();
            foreach(var c in Cars)
            {
                if (func(c))
                {
                    list.Add(c);
                }
            }
            return list;
        }
        public Car GetFirstOrDefault(Func<Car, bool> func)
        {
            foreach (var c in Cars)
            {
                if (func(c))
                {
                    return c;
                }
            }
            return Cars[0] ?? null;
        }
    }
    class Car
    {
        public Brand Brand { get; set; }
        public string Model { get; set; }
        public DateTime ManufactureDate { get;set; }
        public decimal Price { get; set; }
        public bool IsElectricFueled { get; set; }
        
        public static Car Generate ()
        {
            Car car = new Car();
            car.Brand = (Brand)Program.random.Next(1, 6);
            car.Model = GenerateRandomString();
            car.ManufactureDate = DateTime.Now;
            car.Price = Program.random.Next(1000, 10001);
            car.IsElectricFueled = Program.random.Next(0, 2) == 1 ? true : false;
            return car;
        }

        public static string GenerateRandomString()
        {
            var str = "";
            for (int i = 0; i < Program.random.Next(10, 20); i++)
            {// 141 173
                str += (char)Program.random.Next(97, 123);
            }
            return str;
        }

        public override string ToString() {
            return $" Brand: {Brand}, \n Model: {Model} \n ManufactureDate: {ManufactureDate} \n Price: {Price}, IsElectric: {IsElectricFueled} \n ============================= \n \n";
        }
    }

    public enum Brand
    {
        BMW,
        TOYOTA,
        HONDA,
        MERCEDES,
        SUBARU
    }
}
