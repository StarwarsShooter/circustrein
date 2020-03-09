using System;

namespace circustrein
{
    class Program
    {
        string eter;
        int formaat;
        public void Main(string[] args)
        {
            Console.WriteLine("Is het een vleeseter of planteneter?");
            eter = Console.ReadLine();
            Console.WriteLine("Is het een klein dier (1) een middelgroot dier (3) of een groot dier (5)");
            string grootte = Console.ReadLine();
            formaat = Convert.ToInt32(grootte);

            animal animal = new animal(eter, formaat);
        }        
    }
}
