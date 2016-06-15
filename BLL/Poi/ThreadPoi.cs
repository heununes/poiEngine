using poiEngine.BLL.Poi;
using System;
using System.Threading;

namespace poiEngine.BLL.Poi
{
    class ThreadPoi
    {
        private ManualResetEvent manualResetEvent;
        private int v;
        private int _n;
        private int _fibOfN;
        private ManualResetEvent _doneEvent;

        const int PoiThreads = 10;

        public int N { get { return _n; } }
        public int FibOfN { get { return _fibOfN; } }

        // Constructor.
        public ThreadPoi(int n, ManualResetEvent doneEvent)
        {
            _n = n;
            _doneEvent = doneEvent;
        }

        public ThreadPoi(Feed[] feeds)
        {
            // One event is used for each Fibonacci object.
            ManualResetEvent[] doneEvents = new ManualResetEvent[PoiThreads];
            ThreadPoi[] fibArray = new ThreadPoi[PoiThreads];
            Random r = new Random();

            // Configure and start threads using ThreadPool.
            Console.WriteLine("launching {0} tasks...", PoiThreads);
            for (int i = 0; i < PoiThreads; i++)
            {
                doneEvents[i] = new ManualResetEvent(false);
                ThreadPoi f = new ThreadPoi(r.Next(20, 40), doneEvents[i]);
                fibArray[i] = f;
                ThreadPool.QueueUserWorkItem(f.ThreadPoolCallback, i);
            }

            // Wait for all threads in pool to calculate.
            WaitHandle.WaitAll(doneEvents);
            Console.WriteLine("All calculations are complete.");

            // Display the results.
            for (int i = 0; i < PoiThreads; i++)
            {
                ThreadPoi f = fibArray[i];
                Console.WriteLine("Fibonacci({0}) = {1}", f.N, f.FibOfN);
            }
        }

        public Feed[] addPois (Feed[] feeds)
        {
            return feeds;
        }

        // Wrapper method for use with thread pool.
        public void ThreadPoolCallback(Object threadContext)
        {
            int threadIndex = (int)threadContext;
            Console.WriteLine("thread {0} started...", threadIndex);
            _fibOfN = Calculate(_n);
            Console.WriteLine("thread {0} result calculated...", threadIndex);
            _doneEvent.Set();
        }

        // Recursive method that calculates the Nth Fibonacci number.
        public int Calculate(int n)
        {
            if (n <= 1)
            {
                return n;
            }

            return Calculate(n - 1) + Calculate(n - 2);
        }
    }
}
