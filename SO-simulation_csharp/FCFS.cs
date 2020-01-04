using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SO_simulation_csharp
{
    class FCFS
    {
        private List<Process> ReadyProcessesList;
        private List<List<Process>> DoneProcessesList;
        private List<List<Process>> LoadedProcesses;
        private ProcessUtilities processUtilities;

        public FCFS(string pathToDirectoryWithXMLFiles)
        {
            ReadyProcessesList = new List<Process>();
            DoneProcessesList = new List<List<Process>>();
            processUtilities = new ProcessUtilities();
            LoadedProcesses = processUtilities.LoadManyListOfProcessesFromSerializedXMLs(pathToDirectoryWithXMLFiles);

        }


        public void RunFCFS()
        {
            long cyclesNumber = 0;

            while (true)
            {

                if (LoadedProcesses.Count == 0) break;


                foreach (Process process in LoadedProcesses.First())
                {
                    //process.WaitingTime += cyclesNumber;

                    for (int i = 0; i < process.CpuBurstTime; ++i)
                    {
                        
                        cyclesNumber++;
                    }

                }

                LoadedProcesses.RemoveAt(0);

            }

            Console.WriteLine("<<<<<<<<<<<<<<<<<< " + cyclesNumber + ">>>>>>>>>>>>>>>>");
        }
    }

}
