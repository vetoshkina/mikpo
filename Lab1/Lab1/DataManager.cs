using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Lab1
{
    public class DataManager
    {

        string _inPath;
        string _outPath;

        public DataManager(string infile, string outfile)
        {
            if (File.Exists(infile))
            {
                _inPath = infile;
                _outPath = outfile;
            }
            else
                throw new Exception("Error! Input file not found!");
        }

        public void Start()
        {
            StreamReader sr = new StreamReader(_inPath);
            StreamWriter sw = new StreamWriter(_outPath);
            string read;
            int i = 0;
            Solve solve;
            List<string> buffer = new List<string>();
            while ((read = sr.ReadLine()) != null)
            {
                try
                {
                    i++;
                    solve = new Solve(read);
                    solve.Calculate();
                    Console.WriteLine(String.Format("Line {0}: Beta = {1}; Gamma = {2}", i, solve.Beta, solve.Gamma));
                    buffer.Add(String.Format("{0};{1};{2}", solve.A, solve.B, solve.C));
                    if (buffer.Count == 100)
                    {
                        foreach (string s in buffer)
                            sw.WriteLine(s);
                        buffer.Clear();
                    }
                }
                catch (DataException de)
                {
                    Console.WriteLine(String.Format("Line {0}: {1}", i, de.Message));
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine(String.Format("Line {0}: Error! Need three arguments!", i));
                }
                catch (Exception e)
                {
                    Console.WriteLine(String.Format("Line {0}: {1}", i, e.Message));
                }
                
            }
            foreach (string s in buffer)
                sw.WriteLine(s);
        }
    }
}
