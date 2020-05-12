using System;
using System.Collections.Generic;
using System.Text;

namespace circustrein
{
    class Wagon
    {
        private int max = 10;

        private int filling = 0;

        private List<animal> wagonAnimals = new List<animal>();
        public List<animal> wagonList
        {
            get { return wagonAnimals; }
        }

        public Wagon(animal animal)
        {
            wagonAnimals.Add(animal);
        }

        public void AddAnimal(animal plantAnimal)
        {
            wagonAnimals.Add(plantAnimal);
        }

        public int CheckIfFilled()
        {
            animal[] wagonArray = wagonAnimals.ToArray();
            for (int i = 0; i < wagonArray.Length; i++)
            {
                filling = filling + wagonArray[i].AnimalSize;
            }
            return filling;
        }
    }
}
