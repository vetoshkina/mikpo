using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace queues
{
    class Adder
    {
        private int _wTime;
        private int _cCars;
        private List<Cash> _cashes;
        private bool _isWorking;

        #region Properties

        public int WaitTime
        {
            get { return _wTime; }
            set { _wTime = value > 10 ? value : 10; }
        }

        public int CountCars
        {
            get { return _cCars; }
            set { _cCars = value > 0 ? value : 0; }
        }

        public bool IsWorking
        {
            get { return _isWorking; }
            set { _isWorking = value; }
        }

        #endregion

        public List<Cash> Cashes;

        public Adder (int waitTime, int countCars, List<Cash> cashes)
        {
            WaitTime = waitTime;
            CountCars = countCars;
            Cashes = cashes;
        }

        public void Start()
        {
            IsWorking = true;
            while (IsWorking)
            {
                int q = 0;
                for (int i = 0; i < CountCars;i++)
                {
                    Cashes[q].Queue++;
                    q = (q + 1) % Cashes.Count;
                }
                    Thread.Sleep(WaitTime);
            }
        }
    }
}
