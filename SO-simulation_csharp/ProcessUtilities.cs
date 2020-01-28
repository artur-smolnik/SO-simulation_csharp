﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SO_simulation_csharp
{
    class ProcessUtilities
    {

        private int amountOfProcessesPerList;
        private int amountOfProcessesLists;
        List<List<Process>> listOfListsOfProcesses;

        public int AmountOfProcessesPerList { get => amountOfProcessesPerList; set => amountOfProcessesPerList = value; }
        public int AmountOfProcessesLists { get => amountOfProcessesLists; set => amountOfProcessesLists = value; }
        public List<List<Process>> GetListOfListsOfProcesses() { return listOfListsOfProcesses; }


        public ProcessUtilities(string pathToDirectoryWithXMLFiles)
        {
            AmountOfProcessesPerList = 0;
            AmountOfProcessesLists = 0;
            SerializeManyListsOfProcessesAtOnce(20, 1000, 10, 100, @"C:\Users\artur\Desktop\so sim\");
            listOfListsOfProcesses = LoadManyListOfProcessesFromSerializedXMLs(pathToDirectoryWithXMLFiles);
            //Console.WriteLine("listOfListsOfProcesses= " + listOfListsOfProcesses.Count);

        }

        public List<Process> CreateListOfProcesses(int processesAmount, int cpuBurstMinDuration, int cpuBurstMaxDuration)
        {
            List<Process> listOfProcesses = new List<Process>();
            Random rnd = new Random();
            for (int i = 0; i < processesAmount; i++)
            {
                listOfProcesses.Add(new Process(rnd.Next(cpuBurstMinDuration, cpuBurstMaxDuration), 1));
                for (int y = 0; y < 1000000; y++) ; //delay for rng
            }
            return listOfProcesses;
        }

        void SerializeToXMLFile(List<Process> list, string pathToXMLFile)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Process>));
            System.IO.FileStream file = System.IO.File.Create(pathToXMLFile);
            serializer.Serialize(file, list);
            file.Close();
        }

        public List<Process> LoadProcessesFromSerializedXML(string pathToXmlFile)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Process>));
            System.IO.StreamReader file = new System.IO.StreamReader(pathToXmlFile);
            List<Process> list = (List<Process>)reader.Deserialize(file);
            file.Close();

            return list;
        }

        public void SerializeManyListsOfProcessesAtOnce(int amountOfProcessesLists, int amountOfProcessesPerList, int cpuBurstMinDuration, int cpuBurstMaxDuration, string pathToDirectoryForXMLFile)
        {

            for (int i = 0; i < amountOfProcessesLists; i++)
            {
                SerializeToXMLFile(CreateListOfProcesses(amountOfProcessesPerList, cpuBurstMinDuration, cpuBurstMaxDuration), pathToDirectoryForXMLFile + (i + 1) + ".xml");
            }
        }

        public List<List<Process>> LoadManyListOfProcessesFromSerializedXMLs(string pathToDirectoryWithXMLFiles)
        {
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
