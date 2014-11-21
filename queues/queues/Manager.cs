using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace queues
{
    class Manager
    {
        private int _cCashes;
        private int _cCars;
        private int _aTime;
        private int _minTime;
        private int _maxTime;

        #region Properties

        public int CashesCount
        {
            get { return _cCashes; }
            set { _cCashes = value > 0 ? value : 0; }
        }

        public int CarsCount
        {
            get { return _cCars; }
            set { _cCars = value > 0 ? value : 0; }
        }

        public int WorkTime
        {
            get { return _aTime; }
            set { _aTime = value > 10 ? value : 10; }
        }

        public int MinTime
        {
            get { return _minTime; }
            set { _minTime = value > 10 ? value : 10; }
        }

        public int MaxTime
        {
            get { return _maxTime; }
            set { _maxTime = value > 10 ? value : 10; }
        }

        #endregion

        public Manager(int carscount, int cashcount, int alltime, int mintime, int maxtime)
        {
            CarsCount = carscount;
            CashesCount = cashcount;
            WorkTime = alltime;
            if (mintime <= maxtime)
            {
                MinTime = mintime;
                MaxTime = maxtime;
            }
            else
            {
                MaxTime = mintime;
                MinTime = maxtime;
            }
        }

        public void Start()
        {
            int carperiod = 1000;
            List<Cash> Cashes = new List<Cash>();
            Random r = new Random();

            for (int i = 0; i < CashesCount; i++)
                Cashes.Add(new Cash(MinTime, MaxTime, r));
            Adder adder = new Adder(carperiod, CarsCount, Cashes);

            List<Thread> cThreads = new List<Thread>();
            foreach (var c in Cashes)
            {
                cThreads.Add(new Thread(new ThreadStart(c.Start)));
                cThreads[cThreads.Count - 1].Start();
            }

            Thread aThread = new Thread(new ThreadStart(adder.Start));
            aThread.Start();

            Console.WriteLine("Modleing...");
            Thread.Sleep(WorkTime);

            adder.IsWorking = false;
            foreach (var c in Cashes)
                c.IsWorking = false;

            bool isCashesStopped = false;
            while (!isCashesStopped)
            {
                isCashesStopped = true;
                foreach (var t in cThreads)
                    isCashesStopped = isCashesStopped && !t.IsAlive;
            }

            for (int i = 0; i < Cashes.Count; i++)
                Console.WriteLine("Cash {0}: {1} cars served. {2} cars in queue left.", i + 1, Cashes[i].CountServed, Cashes[i].Queue);
        }
    }
}
