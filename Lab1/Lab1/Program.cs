using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                DataManager dm;
                try
                {
                    dm = new DataManager(args[0], args[1]);
                    dm.Start();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Not enough args!\nUsing:\nLab1.exe in.txt out.txt");
            }

            // For Debug
            /*DataManager dm;
            dm = new DataManager("in.txt", "out.txt");
            dm.Start();*/
            
        }
    }
}
