using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO_simulation_csharp
{
    class ProcessUtilities
    {

        private string Path;

        public ProcessUtilities(string pathToXMLDirectory)
        {
            Path = pathToXMLDirectory;
        }

        public ProcessUtilities()
        {
            Path = Environment.CurrentDirectory + "processes.xml";
        }

        public List<Process> CreateListOfProcesses(int processesAmount, int cpuBurstMinDuration, int cpuBurstMaxDuration)
        {
            List<Process> listOfProcesses = new List<Process>();
            Random rnd = new Random();
            for(int i = 0; i<processesAmount;i++)
            {
                listOfProcesses.Add(new Process(rnd.Next(cpuBurstMinDuration, cpuBurstMaxDuration), 1));
            }
            return listOfProcesses;
        }

        public void SerializeToXMLFile(List<Process> list)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Process>));
            System.IO.FileStream file = System.IO.File.Create(Path);
            serializer.Serialize(file, list);
            file.Close();
        }

        void SerializeToXMLFile(List<Process> list, string pathToXMLFile)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Process>));
            System.IO.FileStream file = System.IO.File.Create(pathToXMLFile);
            serializer.Serialize(file, list);
            file.Close();
        }

        public List<Process> LoadProcessesFromSerializedXML(string path)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Process>));
            System.IO.StreamReader file = new System.IO.StreamReader(Path);
            List<Process> list = (List<Process>)reader.Deserialize(file);
            file.Close();
            return list;
        }

        public void SerializeManyListsOfProcessesAtOnce(int amountOfProcessesLists, string pathToDirectoryForXMLFile)
        {
            for(int i = 0; i < amountOfProcessesLists; i++)
            {
                SerializeToXMLFile(CreateListOfProcesses(10, 100, 1000), pathToDirectoryForXMLFile+i+".xml");
            }
        }

    }
}
