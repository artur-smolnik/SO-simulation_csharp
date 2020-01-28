using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO_simulation_csharp
{
    class SJF_NP
    {
        private List<Process> ReadyProcessesList;
        private List<List<Process>> DoneProcessesList;
        private List<List<Process>> LoadedProcesses;
        private ProcessUtilities processUtilities;
        private long cyclesNumber;

        public SJF_NP(ProcessUtilities processUtilities)
        {
            ReadyProcessesList = new List<Process>();
            DoneProcessesList = new List<List<Process>>();
            LoadedProcesses = new List<List<Process>>();
            this.processUtilities = processUtilities;

            for (int i = 0; i < processUtilities.GetListOfListsOfProcesses().Count; i++)
            {
                LoadedProcesses.Add(processUtilities.GetListOfListsOfProcesses().ElementAt(i));
            }

            cyclesNumber = 0;
        }

        public void SortLoadedProcesses()
        {
            List<List<Process>> tmpListOfLists = new List<List<Process>>();
            List<Process> sortedProcesses = new List<Process>();
          
            for (int i = 0; i < LoadedProcesses.Count; i++)
            {
                ReadyProcessesList = LoadedProcesses.ElementAt(i);
                ReadyProcessesList = ReadyProcessesList.OrderBy(process => process.CpuBurstTime).ToList();
                tmpListOfLists.Add(ReadyProcessesList);               
            }

            LoadedProcesses = tmpListOfLists;
        }

        public void RunSJF_NP()
        {
            SortLoadedProcesses();
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

            long waitingTime = 0;
            List<long> listOfWaitingTimes = new List<long>();

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
            Console.WriteLine("SJF RESULTS:");
            Console.WriteLine("Average Waiting Time > " + averageWaitingTime + " <, Average TurnaroundTime > " + averageTurnaroundTime + " <");

        }
    }
}
