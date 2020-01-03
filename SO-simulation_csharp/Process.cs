using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SO_simulation_csharp
{
    class Process
    {
        public TimeSpan CpuBurstTime { get => CpuBurstTime; set => CpuBurstTime = value; }
        public DateTime ArrivalTime { get => ArrivalTime; set => ArrivalTime = value; }
        public DateTime CompletionTime { get => CompletionTime; set => CompletionTime = value; }
        public TimeSpan TurnAroundTime { get => TurnAroundTime; set => TurnAroundTime = value; }
        public TimeSpan WaitingTime { get => WaitingTime; set => WaitingTime = value; }

        
        public Process(TimeSpan cpuBurstTime, DateTime arrivalTime, DateTime completionTime, TimeSpan turnAroundTime, TimeSpan waitingTime)
        {
            CpuBurstTime = cpuBurstTime;
            ArrivalTime = arrivalTime;
            CompletionTime = completionTime;
            TurnAroundTime = turnAroundTime;
            WaitingTime = waitingTime;
            Stopwatch sw = new Stopwatch();
            
        }


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
