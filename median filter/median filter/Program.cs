using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;

namespace median_filter
{
    
    class Program
    {

        public static IEnumerable<KeyValuePair<int, int[,]>> produceBlocks(Bitmap img, int count)
        {
            int w = img.Width, h = img.Height, cur = 0, index = 0;
            int[,] block = new int[w, count];
            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                    block[i, cur] = img.GetPixel(i, j).ToArgb();
                cur++;
                if (cur % count == 0)
                {
                    yield return new KeyValuePair<int, int[,]>(index, block);
                    block = new int[w, count];
                    index += cur;
                    cur = 0;
                }
            }
            if (cur % count != 0)
                yield return new KeyValuePair<int, int[,]>(index, block);
        }

        public static Bitmap getBitmap (int width, int height, BlockingCollection<KeyValuePair<int, Color[,]>> source)
        {
            Bitmap result = new Bitmap(width, height);
            foreach (var p in source)
            {
                for (int j = 0; j < p.Value.GetLength(1); j++)
                    for (int i = 0; i < width; i++)
                    {
                        if (j + p.Key < height)
                            result.SetPixel(i, j + p.Key, p.Value[i, j]);
                        else
                            return result;
                    }
            }
            return result;
        }

        static void Main(string[] args)
        {
            if (args.Count() != 2)
            {
                Console.WriteLine("Not enough arguments!");
                return;
            }

            if(!File.Exists(args[0]))
            {
                Console.WriteLine("Input file missing!");
                return;
            }
            
            Bitmap image = null;
            try 
            {
                image = new Bitmap(args[0]);
            }
            catch
            { 
                Console.WriteLine("Input file error!");
                return;
            }

            BlockingCollection<KeyValuePair<int, Color[,]>> filteredBlocks = new BlockingCollection<KeyValuePair<int, Color[,]>>();
            //Stopwatch stopWatch = new Stopwatch();
            //stopWatch.Start();
            int blocksHeight = 30;
            if (image.Height < blocksHeight)
            {
                Console.WriteLine("Image is too small!");
                return;
            }
            int blocksCount = Convert.ToInt32(Math.Ceiling(image.Height / (double)blocksHeight));
            try
            {
                Parallel.ForEach(produceBlocks(image, blocksCount), block =>
                {
                    int w = block.Value.GetLength(0), h = block.Value.GetLength(1);
                    Color[,] result = new Color[w, h];
                    for (int j = 0; j < h; j++)
                        for (int i = 0; i < w; i++)
                        {
                            List<int> filterList = new List<int>();
                            filterList.Add(block.Value[i - 1 > 0 ? i - 1 : 0, j - 1 > 0 ? j - 1 : 0]);
                            filterList.Add(block.Value[i, j - 1 > 0 ? j - 1 : 0]);
                            filterList.Add(block.Value[i + 1 < w ? i + 1 : w - 1, j - 1 > 0 ? j - 1 : 0]);

                            filterList.Add(block.Value[i - 1 > 0 ? i - 1 : 0, j]);
                            filterList.Add(block.Value[i, j]);
                            filterList.Add(block.Value[i + 1 < w ? i + 1 : w - 1, j]);

                            filterList.Add(block.Value[i - 1 > 0 ? i - 1 : 0, j + 1 < h ? j + 1 : h - 1]);
                            filterList.Add(block.Value[i, j + 1 < h ? j + 1 : h - 1]);
                            filterList.Add(block.Value[i + 1 < w ? i + 1 : h - 1, j + 1 < h ? j + 1 : h - 1]);

                            filterList.Sort();
                            result[i, j] = Color.FromArgb(filterList[4]);
                        }
                    filteredBlocks.Add(new KeyValuePair<int, Color[,]>(block.Key, result));
                });
                filteredBlocks.CompleteAdding();
            }
            catch
            { 
                Console.WriteLine("Median filter error!");
                return;
            }

            try
            {
                Bitmap outimg = getBitmap(image.Width, image.Height, filteredBlocks);
                outimg.Save(args[1], System.Drawing.Imaging.ImageFormat.Bmp);
            }
            catch
            {
                Console.WriteLine("Can't write output image!");
                return;
            }
            
            //stopWatch.Stop();
            //TimeSpan ts = stopWatch.Elapsed;
            //string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            //    ts.Hours, ts.Minutes, ts.Seconds,
            //    ts.Milliseconds / 10);
            //Console.WriteLine("RunTime " + elapsedTime);
        }
    }
}
