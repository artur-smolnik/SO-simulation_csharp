using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SO_simulation_csharp
{
    public class Process
    {
        long cpuBurstTime;
        long turnAroundTime;
        long waitingTime;

       

        public Process()
        {
        }

        public Process(long cpuBurstTime, long waitingTime = 0)
        {
            CpuBurstTime = cpuBurstTime;
            WaitingTime = waitingTime;
            TurnaroundTime = waitingTime;
        }

        public long CpuBurstTime { get => cpuBurstTime; set => cpuBurstTime = value; }
        public long TurnaroundTime { get => turnAroundTime; set => turnAroundTime = value; }
        public long WaitingTime { get => waitingTime; set => waitingTime = value; }

        




        /*
         using System.Diagnostics;
        // ...
        Stopwatch sw = new Stopwatch();
        sw.Start();
        // ...
        sw.Stop();
        Console.WriteLine("Elapsed={0}",sw.Elapsed);
         */
    }
}
