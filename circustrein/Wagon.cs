using System;
using System.Collections.Generic;
using System.Text;

namespace circustrein
{
    [Serializable]

    internal class Wagon
    {
        private int _max = 10;

        private int _filling = 0;

        private readonly List<Animal> _wagonAnimals = new List<Animal>();
        public List<Animal> WagonList
        {
            get { return _wagonAnimals; }
        }

        public Wagon(Animal animal)
        {
            _wagonAnimals.Add(animal);
        }

        public void AddAnimal(Animal plantAnimal)
        {
            _wagonAnimals.Add(plantAnimal);
        }

        public int CheckIfFilled()
        {
            Animal[] wagonArray = _wagonAnimals.ToArray();
            _filling = 0;
            for (int i = 0; i < wagonArray.Length; i++)
            {
                _filling = _filling + wagonArray[i].AnimalSize;
            }
            return _filling;
        }
    }
}
