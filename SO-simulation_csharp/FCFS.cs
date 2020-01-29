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
    /// Klasa zajmuje sie przeprowadzeniem szeregowania FCFS 
    /// oraz obliczeniem srednich czasow oczeekiwania i przetwarzania
    /// </summary>
    class FCFS
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
        /// Konstruktor wczytujacy ciagi procesow z zadanej sciezki, przechowywane w odpowiednim formacie XML
        /// Inicjalizuje obiekt ProcesUtilities i ustawia cyclesNumber na 0
        /// </summary>
        /// <param name="processUtilities">Obiekt klasy ProcessUtilities</param>
        public FCFS(ProcessUtilities processUtilities)
        {
            LoadedProcesses = new List<List<Process>>(processUtilities.LoadManyListOfProcessesFromSerializedXMLs());
            this.processUtilities = processUtilities;
            cyclesNumber = 0;
            
        }

        /// <summary>
        /// Funkcja odpowiada za bezposrednie przeprowadzenie szeregowania FCFS
        /// </summary>
        public void RunFCFS()
        {
            foreach (List<Process> list in LoadedProcesses)
            {
                foreach (Process process in list)
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
        public List<double> AverageWaitingTimeForEachSequence()
        {
            List<double> listOfWaitingTimes = new List<double>();
            double waitingTime = 0;
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
        public void PrintFCFSResults()
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

            Console.WriteLine("FCFS RESULTS:");
            Console.WriteLine("Average Waiting Time > " + averageWaitingTime + " <, Average TurnaroundTime > " + averageTurnaroundTime + " <");
            WriteResultsToFile();
        }

        /// <summary>
        /// Funkcja zapisuje wyniki dzialania algorytmu do wygenerowanego pliku txt
        /// </summary>
        public void WriteResultsToFile()
        {
            FileInfo fileInfo = new FileInfo(@"C:\Users\artur\Desktop\wyniki symulacji\wynikiFCFS.txt");
            StreamWriter streamWriter = fileInfo.CreateText();
            streamWriter.WriteLine("FCFS average waiting times for each sequence:");
            foreach(var time in AverageWaitingTimeForEachSequence())
            {
                streamWriter.Write("[" + time + "] ");

            }
            streamWriter.WriteLine();
            streamWriter.WriteLine("FCFS average turnaround times for each sequence:");
            foreach (var time in AverageTurnaroundTimeForEachSequence())
            {
                streamWriter.Write("[" + time + "] ");
            }
            streamWriter.Close();

        }
    }
}
