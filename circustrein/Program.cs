using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Linq.Expressions;
using System.Runtime.Serialization.Formatters.Binary;

namespace circustrein
{
    [Serializable]
    internal static class Program
    {
        private static string _food;

        private static int _size;

        private static int _total;

        private static int _totaalAnimals = 0;

        private static List<Animal> _animals = new List<Animal>();

        private static List<Animal> _carnivores = new List<Animal>();

        private static List<Animal> _herbivores = new List<Animal>();

        private static List<Animal> _leftovers = new List<Animal>();

        private static List<Wagon> _wagons = new List<Wagon>();

        public static void Main(string[] args)
        {
            Console.WriteLine("Hoeveel dieren wilt u vervoeren?");
            string maxed = Console.ReadLine();
            _total = Convert.ToInt32(maxed);

            Console.WriteLine("Is het een vleeseter (v) of planteneter (p)?");
            _food = Console.ReadLine();
            Console.WriteLine("Is het een klein dier (1) een middelgroot dier (3) of een groot dier (5)?");
            string grootte = Console.ReadLine();
            _size = Convert.ToInt32(grootte);

            bool check = CheckText();

            if (check)
            {
                Animal animal = new Animal(_food, _size);
                _animals.Add(animal);
                _totaalAnimals = _totaalAnimals + 1;
                while (_totaalAnimals < _total)
                {
                    AddAnimal();
                    _totaalAnimals++;
                }
                FoodCheck();
                CreateWagon();
                LeftoverFill();
                Console.WriteLine("Wagons: " + _wagons.Count.ToString());
            }
            else
            {
                
                while (_totaalAnimals < _total)
                {
                    AddAnimal();
                    _totaalAnimals++;
                }
                FoodCheck();
                CreateWagon();
                LeftoverFill();
                Console.WriteLine("Wagons: " + _wagons.Count.ToString());
            }
            
        }

        public static void FoodCheck()
        {
            for (int i = 0; i < _animals.Count; i++)
            {
                Animal[] animals = Program._animals.ToArray();
                if (animals[i].AnimalFood == "v")
                {
                    _carnivores.Add(animals[i]);
                }
                else
                {
                    _herbivores.Add(animals[i]);
                }
            }
            SortAnimal(_carnivores);
            SortAnimal(_herbivores);
        }

        public static void SortAnimal(List<Animal> food)
        {
            food = food.OrderByDescending(x => x.AnimalSize)
                .ToList();
        }

        public static void CreateWagon()
        {
            Animal[] meatAnimalses = _carnivores.ToArray();
            List<Animal> clonedHerbivores = new List<Animal>(_herbivores);
            for (int i = 0; i < meatAnimalses.Length; i++)
            {
                if (meatAnimalses[i].AnimalSize == 5)
                {
                    Wagon wagon = new Wagon(meatAnimalses[i]);
                    _wagons.Add(wagon);
                }
                else if (meatAnimalses[i].AnimalSize == 3)
                {
                    Wagon wagon = new Wagon(meatAnimalses[i]);
                    _wagons.Add(wagon);

                    for (int j = 0; j < _herbivores.Count; j++)
                    {
                        if (_herbivores[j].AnimalSize == 5)
                        {
                            wagon.AddAnimal(clonedHerbivores[j]);
                            _herbivores[j].AnimalSize = 0;
                            break;
                        }
                    }
                }
                else if (meatAnimalses[i].AnimalSize == 1)
                {
                    Wagon wagon = new Wagon(meatAnimalses[i]);
                    _wagons.Add(wagon);
                    int count = 0;

                    for (int j = 0; j < _herbivores.Count; j++)
                    {
                        if (_herbivores[j].AnimalSize == 3)
                        {
                            wagon.AddAnimal(clonedHerbivores[j]);
                            _herbivores[j].AnimalSize = 0;

                            count++;
                            if (count == 3)
                            {
                                break;
                            }
                        }
                    }
                    int check = wagon.CheckIfFilled();
                    if (check < 5)
                    {
                        for (int j = 0; j < _herbivores.Count; j++)
                        {
                            if (_herbivores[j].AnimalSize == 5)
                            {
                                wagon.AddAnimal(clonedHerbivores[j]);
                                _herbivores[j].AnimalSize = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public static void LeftoverFill()
        {
            for (int i = 0; i < _herbivores.Count; i++)
            {
                if (_herbivores[i].AnimalSize != 0)
                {
                    _leftovers.Add(_herbivores[i]);
                }
            }

            if (_leftovers.Count > 0)
            {
                var clonedLeftovers = _leftovers.DeepClone();

                Wagon wagon = new Wagon(clonedLeftovers[0]);
                _wagons.Add(wagon);
                _leftovers[0].AnimalSize = 0;
                int check = wagon.CheckIfFilled();
                if (check < 10)
                {
                    for (int i = 1; i < _leftovers.Count; i++)
                    {
                        if (_leftovers[i].AnimalSize + check <= 10)
                        {
                            wagon.AddAnimal(clonedLeftovers[i]);
                            _leftovers[i].AnimalSize = 0;
                            check = check + clonedLeftovers[i].AnimalSize;
                        }
                    }
                }

                for (int i = 0; i < _leftovers.Count; i++)
                {
                    if (_leftovers[i].AnimalSize != 0)
                    {
                        Wagon wagon2 = new Wagon(clonedLeftovers[i]);
                        _wagons.Add(wagon2);
                        _leftovers[i].AnimalSize = 0;
                        for (int j = 0; j < _leftovers.Count; j++)
                        {
                            if (_leftovers[j].AnimalSize + check <= 10)
                            {
                                wagon.AddAnimal(clonedLeftovers[i]);
                                _leftovers[j].AnimalSize = 0;
                            }
                        }
                    }
                }
            }
        }

        public static bool CheckText()
        {
            bool check;

            if (_food == "v" || _food == "p")
            {
                check = true;
            }

            else if (_size == 1 || _size == 3 || _size == 5)
            {
                check = true;
            }

            else
            {
                Console.WriteLine("Dit is geen optie");
                check = false;
            }

            return check;
        }

        public static void AddAnimal()
        {
            Console.WriteLine("Is het een vleeseter (v) of planteneter (p)?");
            _food = Console.ReadLine();
            Console.WriteLine("Is het een klein dier (1) een middelgroot dier (3) of een groot dier (5)?");
            string grootte = Console.ReadLine();
            _size = Convert.ToInt32(grootte);

            bool check = CheckText();

            if (check)
            {
                Animal animal = new Animal(_food, _size);
                _animals.Add(animal);
            }
            else
            {
                AddAnimal();
            }
        }
        public static T DeepClone<T>(this T obj)
        {
            using (MemoryStream memory_stream = new MemoryStream())
            {
                // Serialize the object into the memory stream.
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(memory_stream, obj);

                // Rewind the stream and use it to create a new object.
                memory_stream.Position = 0;
                return (T)formatter.Deserialize(memory_stream);
            }
        }
    }
}
