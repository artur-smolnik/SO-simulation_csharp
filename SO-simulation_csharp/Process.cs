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

        double cpuBurstTime;
        double turnAroundTime;
        double waitingTime;

        /// <summary>
        /// Czas jaki procesor musi poswiecic na wykonanie zadania
        /// </summary>
        public double CpuBurstTime { get => cpuBurstTime; set => cpuBurstTime = value; }

        /// <summary>
        /// Czas jaki proces spedza na oczekiwaniu na przydzial czasu procesora
        /// </summary>
        public double WaitingTime { get => waitingTime; set => waitingTime = value; }

        /// <summary>
        /// Calkowity czas przetwarzania procesu
        /// </summary>
        public double TurnaroundTime { get => turnAroundTime; set => turnAroundTime = value; }

        /// <summary>
        /// Pusty konstruktor klasy Process
        /// </summary>
        public Process()
        {
        }

        /// <summary>
        /// Konstruktor ustawiajacy podstawowe wartosci cech procesu
        /// </summary>
        /// <param name="cpuBurstTime">Czas jaki procesor musi poswiecic na wykonanie zadania</param>
        /// <param name="waitingTime">Czas jaki proces spedza na oczekiwaniu na przydzial czasu procesora</param>
        /// <param name="turnaroundTime">Calkowity czas przetwarzania procesu</param>
        public Process(double cpuBurstTime, double waitingTime = 0, double turnaroundTime = 0)
        {
            CpuBurstTime = cpuBurstTime;
            WaitingTime = waitingTime;
            TurnaroundTime = turnaroundTime;
        }

        

    }
}
