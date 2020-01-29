using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SO_simulation_csharp
{
    class ProcessUtilities
    {

        private double amountOfProcessesPerList;
        private double amountOfProcessesLists;

        /// <summary>
        /// Properta zawiera liczbe procesow obecnych w pojedynczym ciagu
        /// </summary>
        public double AmountOfProcessesPerList { get => amountOfProcessesPerList; set => amountOfProcessesPerList = value; }

        /// <summary>
        /// Properta zawiera liczbe ciagow procesow
        /// </summary>
        public double AmountOfProcessesLists { get => amountOfProcessesLists; set => amountOfProcessesLists = value; }

        /// <summary>
        /// Konstruktor inicjalizujacy obiekt klasy ProcessUtilities
        /// </summary>
        /// <param name="pathToDirectoryWithXMLFiles">sciezka do archiwum zawierajacego pliki XML z procesami</param>
        public ProcessUtilities(string pathToDirectoryWithXMLFiles)
        {
            AmountOfProcessesPerList = 0;
            AmountOfProcessesLists = 0;
            SerializeManyListsOfProcessesAtOnce(100, 100, 1, 20, @"C:\Users\artur\Desktop\so sim\");
        }

        /// <summary>
        /// Funkcja tworzy jedna liste procesow o zadanych parametrach
        /// </summary>
        /// <param name="processesAmount">Ilosc procesow w ciagu</param>
        /// <param name="cpuBurstMinDuration">Minimalna wartosc Cpu Burst Time</param>
        /// <param name="cpuBurstMaxDuration">Maksymalna wartosc Cpu Burst Time</param>
        /// <returns>Lista procesow</returns>
        public List<Process> CreateListOfProcesses(int processesAmount, int cpuBurstMinDuration, int cpuBurstMaxDuration)
        {
            List<Process> listOfProcesses = new List<Process>();
            Random rnd = new Random();
            for (int i = 0; i < processesAmount; i++)
            {
                listOfProcesses.Add(new Process(rnd.Next(cpuBurstMinDuration, cpuBurstMaxDuration), 0, 0));
                for (int y = 0; y < 1000000; y++) ; //delay for rng
            }
            return listOfProcesses;
        }

        /// <summary>
        /// Funkcja zapisuje podana w parametrze liste procesow do pliku XML
        /// </summary>
        /// <param name="list">Lista procesow do zapisania do pliku XML</param>
        /// <param name="pathToXMLFile">Sciezka, gdzie plik XML ma zostac utworzony</param>
        void SerializeToXMLFile(List<Process> list, string pathToXMLFile)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Process>));
            System.IO.FileStream file = System.IO.File.Create(pathToXMLFile);
            serializer.Serialize(file, list);
            file.Close();
        }

        /// <summary>
        /// Funkcja wczytuje liste procesow z pliku XML i zapisuje ja listy, ktora jest potem zwrocona
        /// </summary>
        /// <param name="pathToXmlFile">Sciezka do pliku XML z zapisanymi procesami</param>
        /// <returns>Lista wczytanych procesow</returns>
        public List<Process> LoadProcessesFromSerializedXML(string pathToXmlFile)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Process>));
            System.IO.StreamReader file = new System.IO.StreamReader(pathToXmlFile);
            List<Process> list = (List<Process>)reader.Deserialize(file);
            file.Close();

            return list;
        }

        /// <summary>
        /// Funkcja tworzy i jednoczesnie zapisuje do plikow XML wiele list procesow
        /// </summary>
        /// <param name="amountOfProcessesLists">Liczba ciagow procesow</param>
        /// <param name="amountOfProcessesPerList">Liczba procesow w pojedynczym ciagu</param>
        /// <param name="cpuBurstMinDuration">Minimalna wartosc dla CBT</param>
        /// <param name="cpuBurstMaxDuration">Maksymalna wartosc dla CBT</param>
        /// <param name="pathToDirectoryForXMLFile">Sciezka do katologu, gdzie beda zapisane ciagi procesow</param>
        public void SerializeManyListsOfProcessesAtOnce(int amountOfProcessesLists, int amountOfProcessesPerList, int cpuBurstMinDuration, int cpuBurstMaxDuration, string pathToDirectoryForXMLFile)
        {

            for (int i = 0; i < amountOfProcessesLists; i++)
            {
                SerializeToXMLFile(CreateListOfProcesses(amountOfProcessesPerList, cpuBurstMinDuration, cpuBurstMaxDuration), pathToDirectoryForXMLFile + (i + 1) + ".xml");
            }
        }

        /// <summary>
        /// Funkcja wczytuje wiele ciagow procesow z wielu plikow XML i zapisuje je do listy list
        /// </summary>
        /// <returns>Lista list wczytanych procesow</returns>
        public List<List<Process>> LoadManyListOfProcessesFromSerializedXMLs()
        {
            string pathToDirectoryWithXMLFiles = @"C:\Users\artur\Desktop\so sim\";
            List<List<Process>> listOfListsOfProcesses = new List<List<Process>>();

            string[] folderContent = Directory.GetFiles(pathToDirectoryWithXMLFiles);

            foreach (var filepath in folderContent)
            {
                listOfListsOfProcesses.Add(LoadProcessesFromSerializedXML(filepath));
            }
            AmountOfProcessesLists = folderContent.Count();
            AmountOfProcessesPerList = listOfListsOfProcesses.Last().Count;
            return listOfListsOfProcesses;
        }
    }
}
