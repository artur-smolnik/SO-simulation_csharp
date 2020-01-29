using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace SO_simulation_csharp
{
    /// <summary>
    /// Klasa zajmuje sie przeprowadzeniem szeregowania RoundRobinLCFS 
    /// oraz obliczeniem srednich czasow oczekiwania i przetwarzania
    /// </summary>
    class RoundRobinLCFS
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
        private double cyclesNumber;

        /// <summary>
        /// Kwant czasu(ilosc jednostek/taktow zegarowych) poswiecany cyklicznie kazdemu procesowi
        /// </summary>
        private double quantum;

        /// <summary>
        /// Konstruktor wczytujacy ciagi procesow z zadanej sciezki do zmiennej LoadedProcesses, 
        /// przechowywane w odpowiednim formacie XML
        /// Inicjalizuje obiekt ProcesUtilities i ustawia cyclesNumber na 0, kwant czas ustawiony na 10
        /// </summary>
        /// <param name="processUtilities">Obiekt klasy ProcessUtilities</param>
        public RoundRobinLCFS(ProcessUtilities processUtilities, double quantum)
        {
            LoadedProcesses = new List<List<Process>>(processUtilities.LoadManyListOfProcessesFromSerializedXMLs());
            this.processUtilities = processUtilities;
            cyclesNumber = 0;
            this.quantum = quantum;
            foreach (List<Process> list in LoadedProcesses)
            {
                foreach (Process process in list)
                {
                    process.WaitingTime -= process.CpuBurstTime;
                }
            }
        }

        /// <summary>
        /// Funkcja odpowiada za bezposrednie przeprowadzenie szeregowania RoundRobinLCFS
        /// Funkcja przeprowadza szeregowanie jednoczesnie odwracajac kolejnosc
        /// przybywania procesow, odwrotnie do RoundRobinFCFS
        /// </summary>
        public void RunRoundRobinLCFS()
        {
            int switchList = 0;
            foreach (List<Process> list in LoadedProcesses)
            {
                while (switchList != processUtilities.AmountOfProcessesPerList)
                {
                    foreach (Process process in list.AsEnumerable().Reverse())
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
        }

        /// <summary>
        /// Funkcja zwraca liste srednich czasow oczekiwania dla poszczegolnych ciagow procesow
        /// </summary>
        /// <returns>Lista srednich czasow oczekiwania</returns>
        public List<double> AverageWaitingTimeForEachSequence()
        {
            double waitingTime = 0;
            List<double> listOfWaitingTimes = new List<double>();

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
        public List<double> AverageTurnaroundTimeForEachSequence()
        {
            List<double> listOfTurnaroundTime = new List<double>();
            double turnaroundTime = 0;
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
        public void PrintRoundRobinLCFSResults()
        {
            double averageWaitingTime = 0;
            double averageTurnaroundTime = 0;
            for (int i = 0; i < processUtilities.AmountOfProcessesLists; i++)
            {
                averageWaitingTime += AverageWaitingTimeForEachSequence().ElementAt(i);
                averageTurnaroundTime += AverageTurnaroundTimeForEachSequence().ElementAt(i);
            }
            averageWaitingTime /= processUtilities.AmountOfProcessesLists;
            averageTurnaroundTime /= processUtilities.AmountOfProcessesLists;

            Console.WriteLine("RoundRobinLCFS with quantum = "+quantum+"RESULTS:");
            Console.WriteLine("Average Waiting Time > " + averageWaitingTime + " <, Average TurnaroundTime > " + averageTurnaroundTime + " <");
            WriteResultsToFile();
        }

        /// <summary>
        /// Funkcja zapisuje wyniki dzialania algorytmu do wygenerowanego pliku txt
        /// </summary>
        public void WriteResultsToFile()
        {
            FileInfo fileInfo = new FileInfo(@"C:\Users\artur\Desktop\wyniki symulacji\wyniki Round Robin LCFS with quantum " + quantum + ".txt");
            StreamWriter streamWriter = fileInfo.CreateText();
            streamWriter.WriteLine("RoundRobinLCFS average waiting times for each sequence with quantum " + quantum + " :");
            foreach (var time in AverageWaitingTimeForEachSequence())
            {
                streamWriter.Write("[" + time + "] ");

            }
            streamWriter.WriteLine();
            streamWriter.WriteLine("RoundRobinLCFS average turnaround times for each sequence with quantum " + quantum + " :");
            foreach (var time in AverageTurnaroundTimeForEachSequence())
            {
                streamWriter.Write("[" + time + "] ");
            }
            streamWriter.Close();

        }
    }
}
