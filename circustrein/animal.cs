using System;
using System.Collections.Generic;
using System.Text;

namespace circustrein
{
    class animal
    {
        private string food;
        private int size;

        public string AnimalFood
        {
            get { return food; }
            set => food = value;
        }

        public int AnimalSize { get { return size; }
            set { size = value; }
        }
        
        public animal(string food, int size)
        {
            this.food = food;
            this.size = size;
        }
    }
}
