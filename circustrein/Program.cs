using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace circustrein
{
    class Program
    {
        static string eter;

        static int formaat;

        private static int totaal;

        private static int totaalAnimals = 0;

        static List<animal> animals = new List<animal>();

        static List<animal> meat = new List<animal>();

        static List<animal> plant = new List<animal>();

        static List<animal> leftover = new List<animal>();

        static List<Wagon> wagons = new List<Wagon>();

        public static void Main(string[] args)
        {
            Console.WriteLine("Hoeveel dieren wilt u vervoeren?");
            string maxed = Console.ReadLine();
            totaal = Convert.ToInt32(maxed);

            Console.WriteLine("Is het een vleeseter (v) of planteneter (p)?");
            eter = Console.ReadLine();
            Console.WriteLine("Is het een klein dier (1) een middelgroot dier (3) of een groot dier (5)?");
            string grootte = Console.ReadLine();
            formaat = Convert.ToInt32(grootte);

            bool check = CheckText();

            if (check)
            {
                animal animal = new animal(eter, formaat);
                animals.Add(animal);
                totaalAnimals = totaalAnimals + 1;
                while (totaalAnimals < totaal)
                {
                    AddAnimal();
                    totaalAnimals++;
                }
                FoodCheck();
                CreateWagon();
                LeftoverFill();
                Console.WriteLine(wagons.Count.ToString());
            }
            else
            {
                
                while (totaalAnimals < totaal)
                {
                    AddAnimal();
                    totaalAnimals++;
                }
                FoodCheck();
                CreateWagon();
                LeftoverFill();
            }
            
        }

        public static void FoodCheck()
        {
            for (int i = 0; i < animals.Count; i++)
            {
                animal[] animals = Program.animals.ToArray();
                if (animals[i].AnimalFood == "v")
                {
                    meat.Add(animals[i]);
                }
                else
                {
                    plant.Add(animals[i]);
                }
            }
            SortAnimal(meat);
            SortAnimal(plant);
        }

        public static void SortAnimal(List<animal> Food)
        {
            Food = Food.OrderByDescending(x => x.AnimalSize)
                .ToList();
        }

        public static void CreateWagon()
        {
            animal[] meatAnimalses = meat.ToArray();
            for (int i = 0; i < meatAnimalses.Length; i++)
            {
                if (meatAnimalses[i].AnimalSize == 5)
                {
                    Wagon wagon = new Wagon(meatAnimalses[i]);
                    wagons.Add(wagon);
                }
                else if (meatAnimalses[i].AnimalSize == 3)
                {
                    Wagon wagon = new Wagon(meatAnimalses[i]);
                    wagons.Add(wagon);

                    for (int j = 0; j < plant.Count; j++)
                    {
                        if (plant[j].AnimalSize == 5)
                        {
                            var animalFill = plant[j];
                            wagon.AddAnimal(animalFill);
                            plant[j].AnimalSize = 0;
                            break;
                        }
                    }
                }
                else if (meatAnimalses[i].AnimalSize == 1)
                {
                    Wagon wagon = new Wagon(meatAnimalses[i]);
                    wagons.Add(wagon);
                    int count = 0;

                    for (int j = 0; j < plant[j].AnimalSize; j++)
                    {
                        if (plant[j].AnimalSize == 3)
                        {
                            var animalFill = plant[j];
                            wagon.AddAnimal(animalFill);
                            plant[j].AnimalSize = 0;

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
                        for (int j = 0; j < plant[j].AnimalSize; j++)
                        {
                            if (plant[j].AnimalSize == 5)
                            {
                                var animalFill = plant[j];
                                wagon.AddAnimal(animalFill);
                                plant[j].AnimalSize = 0;
                                break;
                            }
                        }
                    }
                }
            }
        }

        public static void LeftoverFill()
        {
            for (int i = 0; i < plant[i].AnimalSize; i++)
            {
                if (plant[i].AnimalSize != 0)
                {
                    leftover.Add(plant[i]);
                }
            }

            if (leftover.Count > 0)
            {
                for (int i = 0; i < leftover[i].AnimalSize; i++)
                {
                    Wagon wagon = new Wagon(leftover[0]);
                    wagons.Add(wagon);
                    int check = wagon.CheckIfFilled();
                    if (check < 10)
                    {
                        if (leftover[i].AnimalSize + check < 10)
                        {
                            var filler = leftover[i];
                            wagon.AddAnimal(filler);
                            leftover[i].AnimalSize = 0;
                        }
                    }
                }
            }
        }

        public static bool CheckText()
        {
            bool check;

            if (eter == "v" || eter == "p")
            {
                //Console.WriteLine("Dit is geen optie");
                check = true;
            }

            else if (formaat == 1 || formaat == 3 || formaat == 5)
            {
                //Console.WriteLine("Dit is geen optie");
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
            eter = Console.ReadLine();
            Console.WriteLine("Is het een klein dier (1) een middelgroot dier (3) of een groot dier (5)?");
            string grootte = Console.ReadLine();
            formaat = Convert.ToInt32(grootte);

            bool check = CheckText();

            if (check)
            {
                animal animal = new animal(eter, formaat);
                animals.Add(animal);
            }
            else
            {
                AddAnimal();
            }
        }
    }
}
