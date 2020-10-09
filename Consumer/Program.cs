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

            worker.Start();
            

            
            Console.ReadLine();
        }
    }
}
