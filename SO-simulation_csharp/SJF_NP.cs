using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO_simulation_csharp
{
    class SJF_NP
    {
        /// <summary>
        /// Lista przechowujaca ciagi procesow
        /// </summary>
        private List<List<Process>> LoadedProcesses;

        /// <summary>
        /// Obiekt klasy ProcessUtilities
        /// </summary>
        private ProcessUtilities processUtilities;

        /// <summary>
        /// Zmienna wykorzystywana do mierzenia taktow zegarowych
        /// </summary>
        private long cyclesNumber;

        /// <summary>
        /// Konstruktor wczytujacy ciagi procesow z zadanej sciezki, przechowywane w odpowiednim formacie XML
        /// Inicjalizuje obiekt ProcesUtilities i ustawia cyclesNumber na 0
        /// </summary>
        /// <param name="processUtilities">Obiekt klasy ProcessUtilities</param>
        public SJF_NP(ProcessUtilities processUtilities)
        {
            LoadedProcesses = new List<List<Process>>(processUtilities.LoadManyListOfProcessesFromSerializedXMLs());
            this.processUtilities = processUtilities;
            cyclesNumber = 0;
        }

        /// <summary>
        /// Funkcja sortuje procesy w ciagach w kolejnosci od procesu zajmujacego najmniej 
        /// czasu procesora, do zajmujacych najwiecej czasu
        /// </summary>
        public void SortLoadedProcesses()
        {
            List<List<Process>> tmpListOfLists = new List<List<Process>>();
            foreach ( List<Process> loadedProcesses in LoadedProcesses)
            { 
                tmpListOfLists.Add(loadedProcesses.OrderBy(process => process.CpuBurstTime).ToList());
            }
            LoadedProcesses.Clear();
            for (int i = 0; i < tmpListOfLists.Count; i++)
            {
                LoadedProcesses.Add(tmpListOfLists.ElementAt(i));
            }
        }

        /// <summary>
        /// Funkcja odpowiada za bezposrednie przeprowadzenie szeregowania SJF
        /// </summary>
        public void RunSJF_NP()
        {
            SortLoadedProcesses();
            while (true)
            {
                //if (LoadedProcesses.Count == 0) break;
                foreach(List<Process> list in LoadedProcesses)
                {
                    foreach(Process process in list)
                    {
                        process.WaitingTime = cyclesNumber;
                        process.TurnaroundTime = (cyclesNumber + process.CpuBurstTime);
                        cyclesNumber += process.CpuBurstTime;
                    }
                    cyclesNumber = 0;
                }
                break;
            }
        }

        /// <summary>
        /// Funkcja zwraca liste srednich czasow oczekiwania dla poszczegolnych ciagow procesow
        /// </summary>
        /// <returns>Lista srednich czasow oczekiwania</returns>
        public List<long> AverageWaitingTimeForEachSequence()
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

        /// <summary>
        /// Funkcja zwraca liste srednich czasow przetwarzania dla poszczegolnych ciagow procesow
        /// </summary>
        /// <returns>Lista srednich czasow oczekiwania</returns>
        public List<long> AverageTurnaroundTimeForEachSequence()
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

        /// <summary>
        /// Funkcja wyswietla sredni czas oczekiwania i przetwarzania obliczony na podstawie
        /// wszystkich zadanych ciagow procesow
        /// </summary>
        public void PrintSJF_NPResults()
        {
            long averageWaitingTime = 0;
            long averageTurnaroundTime = 0;
            for (int i = 0; i < processUtilities.AmountOfProcessesLists; i++)
            {
                averageWaitingTime += AverageWaitingTimeForEachSequence().ElementAt(i);
                averageTurnaroundTime += AverageTurnaroundTimeForEachSequence().ElementAt(i);
            }
            averageWaitingTime /= processUtilities.AmountOfProcessesLists;
            averageTurnaroundTime /= processUtilities.AmountOfProcessesLists;

            Console.WriteLine("SJF RESULTS:");
            Console.WriteLine("Average Waiting Time > " + averageWaitingTime + " <, Average TurnaroundTime > " + averageTurnaroundTime + " <");

        }
    }
}
