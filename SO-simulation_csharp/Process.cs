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
        int cpuBurstTime;
        int turnAroundTime;
        int waitingTime;

       

        public Process()
        {
        }

        public Process(int cpuBurstTime, int waitingTime = 0)
        {
            CpuBurstTime = cpuBurstTime;
            TurnAroundTime = waitingTime;
            WaitingTime = waitingTime;
        }

        public int CpuBurstTime { get => cpuBurstTime; set => cpuBurstTime = value; }
        public int TurnAroundTime { get => turnAroundTime; set => turnAroundTime = value; }
        public int WaitingTime { get => waitingTime; set => waitingTime = value; }

        //public int CpuBurstTime { get => CpuBurstTime; set => CpuBurstTime = value; }
        //public int TurnAroundTime { get => TurnAroundTime; set => TurnAroundTime = value; }
        //public int WaitingTime { get => WaitingTime; set => WaitingTime = value; }








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
