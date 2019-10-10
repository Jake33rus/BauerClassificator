using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace NaiveBayer
{
<<<<<<< Updated upstream
    //class MainClass
    //{
    //    public static void Main(string[] args)
    //    {
    //        var worker = new Worker(@"/Users/evgeny/Desktop/final/digital/NaiveBayer/categories.csv");
    //        Console.WriteLine("Actual " + worker.ResultRequestName("в лифте не работает кнопка я застрял помогите э"));
    //        Console.ReadKey();
    //    }
    //}
=======
    class MainClass
    {
        public static void Main(string[] args)
        {
            var worker = new Worker(@"/Users/evgeny/Desktop/final/digital/NaiveBayer/categories.csv");
            Console.WriteLine("Actual " + worker.ResultRequestName("Застрял в лифте не могу дозвониться до оператора"));
            Console.ReadKey();
        }
    }
>>>>>>> Stashed changes
}
