using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO_simulation_csharp
{
    /// <summary>
    /// Klasa zajmuje sie przeprowadzeniem szeregowania LCFS 
    /// oraz obliczeniem srednich czasow oczeekiwania i przetwarzania
    /// </summary>
    class LCFS
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
        public LCFS(ProcessUtilities processUtilities)
        {
            LoadedProcesses = new List<List<Process>>(processUtilities.LoadManyListOfProcessesFromSerializedXMLs());
            this.processUtilities = processUtilities;
            cyclesNumber = 0;
            
        }

        /// <summary>
        /// Funkcja odpowiada za bezposrednie przeprowadzenie szeregowania LCFS
        /// Funkcja przeprowadza szeregowanie jednoczesnie odwracajac kolejnosc
        /// przybywania procesow, odwrotnie do FCFS
        /// </summary>
        public void RunLCFS()
        {
            foreach (List<Process> list in LoadedProcesses)
            {
                foreach (Process process in list.AsEnumerable().Reverse())
                {
                    process.WaitingTime = cyclesNumber;
                    process.TurnaroundTime = (cyclesNumber + process.CpuBurstTime);
                    cyclesNumber += process.CpuBurstTime;
                }
                cyclesNumber = 0;
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
        public void PrintLCFSResults()
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

            Console.WriteLine("LCFS RESULTS:");
            Console.WriteLine("Average Waiting Time > " + averageWaitingTime + " <, Average TurnaroundTime > " + averageTurnaroundTime + " <");

        }
    }
}
