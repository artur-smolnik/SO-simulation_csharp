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
        private List<List<Process>> LoadedListsOfProcesses;
        private ProcessUtilities processUtilities;
        private long cyclesNumber;

        public SJF_NP(string pathToDirectoryWithXMLFiles)
        {
            ReadyProcessesList = new List<Process>();
            DoneProcessesList = new List<List<Process>>();
            processUtilities = new ProcessUtilities();
            LoadedListsOfProcesses = processUtilities.LoadManyListOfProcessesFromSerializedXMLs(pathToDirectoryWithXMLFiles);
            cyclesNumber = 0;
        }

        public void SortLoadedProcesses()
        {
            List<List<Process>> tmpListOfLists = new List<List<Process>>();
            List<Process> sortedProcesses = new List<Process>();
            Console.WriteLine("LoadedProcesses.Count:" + LoadedListsOfProcesses.Count);
            Console.WriteLine("LoadedProcesses.Last().Count" + LoadedListsOfProcesses.Last().Count);

            for (int i = 0; i < LoadedListsOfProcesses.Count; i++)
            {

                ReadyProcessesList = LoadedListsOfProcesses.ElementAt(i);
                ReadyProcessesList = ReadyProcessesList.OrderBy(process => process.CpuBurstTime).ToList();
                tmpListOfLists.Add(ReadyProcessesList);               
            }

            LoadedListsOfProcesses = tmpListOfLists;
        }
        public void RunSJF_NP()
        {

            //for (int i = 0; i < LoadedProcesses.Count; i++)
            //{
            //    //LoadedProcesses.ElementAt(i) = LoadedProcesses.ElementAt(i).OrderBy(process => process.CpuBurstTime).ToList();
            //}

            while (true)
            {

                if (LoadedListsOfProcesses.Count == 0) break;

                foreach (Process process in LoadedListsOfProcesses.First())
                {
                    process.WaitingTime = cyclesNumber;
                    process.TurnaroundTime = (cyclesNumber + process.CpuBurstTime);

                    cyclesNumber += process.CpuBurstTime;

                }
                cyclesNumber = 0;
                DoneProcessesList.Add(LoadedListsOfProcesses.First());
                LoadedListsOfProcesses.RemoveAt(0);
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

        public void PrintFCFSResults()
        {
            List<long> listAverageWaiting = AverageWaitingTimeForEachSequenceInMiliSec();
            List<long> listAverageTurnaround = AverageTurnAroundTimeForEachSequenceInMiliSec();

            for (int i = 0; i < processUtilities.AmountOfProcessesLists; i++)
            {
                //Console.WriteLine((i + 1) + ". Sequence: Average Waiting Time > " + listAverageWaiting.ElementAt(i) + " <, Average TurnaroundTime > " + listAverageTurnaround.ElementAt(i) + " <");
                Console.WriteLine("Sequence: Average Waiting Time > " + listAverageWaiting.ElementAt(i) + " <, Average TurnaroundTime > " + listAverageTurnaround.ElementAt(i) + " <");
            }
        }
    }
}
