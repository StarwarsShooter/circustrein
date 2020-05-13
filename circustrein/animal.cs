using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace circustrein
{
    [Serializable]

    internal class Animal : ICloneable
    {
        private string _food;
        private int _size;

        public string AnimalFood
        {
            get { return _food; }
            set => _food = value;
        }

        public int AnimalSize { get { return _size; }
            set { _size = value; }
        }
        
        public Animal(string food, int size)
        {
            this._food = food;
            this._size = size;
        }

        object ICloneable.Clone()
        {
            return ((ICloneable) typeof(Animal)).Clone();
        }
    }
    
}
