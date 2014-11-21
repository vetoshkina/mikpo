using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace queues
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() != 5)
            {
                Console.WriteLine("Not enough arguments!");
                return;
            }
            int countcashes = 0;
            int countcars = 0;
            int alltime = 0;
            int mintime = 0;
            int maxtime = 0;
            try
            {
                countcashes = int.Parse(args[0]);
                countcars = int.Parse(args[1]);
                alltime = int.Parse(args[2]) * 1000;
                mintime = int.Parse(args[3]) * 1000;
                maxtime = int.Parse(args[4]) * 1000;
            }
            catch
            {
                Console.WriteLine("Wrong arguments!");
                return;
            }

            Manager manager = new Manager(countcars, countcashes, alltime, mintime, maxtime);
            Thread mThread = new Thread(new ThreadStart(manager.Start));
            mThread.Start();
            
            //Console.ReadKey();
        }
    }
}
