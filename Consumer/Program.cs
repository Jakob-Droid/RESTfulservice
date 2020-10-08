using System;
using System.Linq;
using ModelLib.Model;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {

            Worker worker = new Worker();

            worker.PostItemAsync(new Item(2, "Bread", "Middle", 21));

            worker.start();
            
            Console.ReadLine();
        }
    }
}
