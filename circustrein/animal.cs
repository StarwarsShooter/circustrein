using System;
using System.Collections.Generic;
using System.Text;

namespace circustrein
{
    class animal
    {
        private string food;
        private int size;

        public string AnimalFood { get { return food; } }
        public int AnimalSize { get { return size; } }
        
        public animal(string food, int size)
        {
            this.food = food;
            this.size = size;
        }
    }
}
