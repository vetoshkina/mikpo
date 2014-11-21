using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace queues
{
    class Cash
    {

        private int _cServed;
        private int _minServeTime;
        private int _maxServeTime;
        private int _queue;

        #region Properties
        public int CountServed
        {
            get { return _cServed; }
            set { _cServed = value > 0 ? value : 0; }
        }

        
        

        public int MinServeTime
        {
            get { return _minServeTime; }
            set { _minServeTime = value > 10 ? value : 10; }
        }

        public int MaxServeTime
        {
            get { return _maxServeTime; }
            set { _maxServeTime = value > 10 && value > MinServeTime ? value : MinServeTime; }
        }

        public int Queue
        {
            get { return _queue; }
            set { _queue = value > 0 ? value : 0; }
        }

        #endregion

        public Random rTime;
        public bool IsWorking;

        public Cash(int minServeTime, int maxServeTime, Random r)
        {
            MinServeTime = minServeTime;
            MaxServeTime = maxServeTime;
            Queue = 0;
            rTime = r;
        }

        public void Start()
        {
            IsWorking = true;
            while (IsWorking)
            {
                while (Queue == 0)
                    if (!IsWorking)
                        return;

                int sleeptime = rTime.Next(MinServeTime, MaxServeTime);
                Thread.Sleep(sleeptime);
                CountServed++;
                Queue--;
            }
        }

    }
}
