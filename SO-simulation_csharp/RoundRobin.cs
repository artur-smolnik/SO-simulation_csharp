using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO_simulation_csharp
{
    class RoundRobin
    {
        private List<List<Process>> LoadedProcesses;
        private ProcessUtilities processUtilities;
        private long cyclesNumber;
        private long quantum;

        public RoundRobin(ProcessUtilities processUtilities)
        {
            LoadedProcesses = new List<List<Process>>();
            this.processUtilities = processUtilities;
            quantum = 10;
            cyclesNumber = 0;

            for (int i = 0; i < processUtilities.GetListOfListsOfProcesses().Count; i++)
            {
                LoadedProcesses.Add(processUtilities.GetListOfListsOfProcesses().ElementAt(i));
            }
            foreach(List<Process> list in LoadedProcesses)
            {
                foreach(Process process in list)
                {
                    process.WaitingTime -= process.CpuBurstTime;
                }
            }
        }

        public void RunRoundRobinFCFS()
        {
            int switchList = 0;

            while (true)
            {
                foreach (List<Process> list in LoadedProcesses)
                {
                    while (switchList != processUtilities.AmountOfProcessesPerList)
                    {
                        foreach (Process process in list)
                        {
                            if (process.CpuBurstTime == 0) continue;

                            if (process.CpuBurstTime - quantum > 0)
                            {
                                process.CpuBurstTime -= quantum;
                                cyclesNumber += quantum;
                            }
                            else
                            {
                                cyclesNumber += process.CpuBurstTime;
                                process.CpuBurstTime = 0;
                                process.TurnaroundTime = cyclesNumber;
                                process.WaitingTime += cyclesNumber;
                                switchList++;
                            }
                        }
                    }
                    switchList = 0;
                    cyclesNumber = 0;
                }
                break;
            }
        }

        
        public List<long> AverageWaitingTimeForEachSequenceInMiliSec()
        {
            long waitingTime = 0;
            List<long> listOfWaitingTimes = new List<long>();

            foreach (List<Process> list in LoadedProcesses)
            {
                foreach (Process process in list)
                {
                    waitingTime += process.WaitingTime;
                }
                listOfWaitingTimes.Add(waitingTime / processUtilities.AmountOfProcessesPerList);
                waitingTime = 0;
            }
            return listOfWaitingTimes;
        }

        public List<long> AverageTurnAroundTimeForEachSequenceInMiliSec()
        {
            List<long> listOfTurnaroundTime = new List<long>();
            long turnaroundTime = 0;
            foreach (List<Process> list in LoadedProcesses)
            {
                foreach (Process process in list)
                {
                    turnaroundTime += process.TurnaroundTime;
                }
                listOfTurnaroundTime.Add(turnaroundTime / processUtilities.AmountOfProcessesPerList);
                turnaroundTime = 0;
            }
            return listOfTurnaroundTime;
        }

        public void PrintSJF_NPResults()
        {
            List<long> listAverageWaiting = AverageWaitingTimeForEachSequenceInMiliSec();
            List<long> listAverageTurnaround = AverageTurnAroundTimeForEachSequenceInMiliSec();

            long averageWaitingTime = 0;
            long averageTurnaroundTime = 0;
            for (int i = 0; i < processUtilities.AmountOfProcessesLists; i++)
            {
                averageWaitingTime += listAverageWaiting.ElementAt(i);
                averageTurnaroundTime += listAverageTurnaround.ElementAt(i);
            }
            Console.WriteLine("RoundRobin RESULTS:");
            Console.WriteLine("Average Waiting Time > " + averageWaitingTime + " <, Average TurnaroundTime > " + averageTurnaroundTime + " <");

        }

    }
}
