using System;
using System.Collections.Generic;
using System.Text;

namespace circustrein
{
    [Serializable]

    internal class Wagon
    {
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
            _filling = 0;
            for (int i = 0; i < _wagonAnimals.Count; i++)
            {
                _filling = _filling + _wagonAnimals[i].AnimalSize;
            }
            return _filling;
        }
    }
}
