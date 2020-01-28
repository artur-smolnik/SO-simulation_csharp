using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SO_simulation_csharp
{
    class FCFS
    {
        private List<List<Process>> DoneProcessesList;
        private List<List<Process>> LoadedProcesses;
        private ProcessUtilities processUtilities;
        private long cyclesNumber;
        private List<List<Process>> tmpchuj;

        public FCFS(ProcessUtilities processUtilities)
        {
            DoneProcessesList = new List<List<Process>>();
            this.processUtilities = processUtilities;
            LoadedProcesses = new List<List<Process>>();
            tmpchuj = processUtilities.GetListOfListsOfProcesses();
            foreach(var v in tmpchuj)
            {
                LoadedProcesses.Add(v);
            }
            //LoadedProcesses = processUtilities.GetListOfListsOfProcesses();
            cyclesNumber = 0;
        }

        public void RunFCFS()
        {
            while (true)
            {
                if (LoadedProcesses.Count == 0) break;

                foreach (Process process in LoadedProcesses.First())
                {
                    process.WaitingTime = cyclesNumber;
                    process.TurnaroundTime = (cyclesNumber + process.CpuBurstTime);

                    cyclesNumber += process.CpuBurstTime;

                }
                cyclesNumber = 0;
                DoneProcessesList.Add(LoadedProcesses.First());
                LoadedProcesses.RemoveAt(0);
            }
        }

        public List<long> AverageWaitingTimeForEachSequenceInMiliSec()
        {

            List<long> listOfWaitingTimes = new List<long>();
            long waitingTime = 0;

            foreach (List<Process> list in DoneProcessesList)
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

            foreach (List<Process> list in DoneProcessesList)
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

        public void PrintFCFSResults()
        {
            List<long> listAverageWaiting = AverageWaitingTimeForEachSequenceInMiliSec();
            List<long> listAverageTurnaround = AverageTurnAroundTimeForEachSequenceInMiliSec();
            long averageWaitingTime = 0;
            long averageTurnaroundTime = 0;

            for(int i = 0; i < listAverageWaiting.Count; i++)
            {
                averageWaitingTime += listAverageWaiting.ElementAt(i);
                averageTurnaroundTime += listAverageTurnaround.ElementAt(i);
            }
            Console.WriteLine("FCFS PREEMPTIVE RESULTS:");
            Console.WriteLine("Average Waiting Time > " + averageWaitingTime + " <, Average TurnaroundTime > " + averageTurnaroundTime + " <");
            
        }
    }
}
