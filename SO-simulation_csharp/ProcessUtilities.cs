using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO_simulation_csharp
{
    class ProcessUtilities
    {

        private var Path = Environment.CurrentDirectory + "processes.xml";

        private List<Process> CreateListOfProcesses(int processesAmount, int cpuBurstMinDuration, int cpuBurstMaxDuration)
        {
           List<Process> listOfProcesses = new List<Process>();
            Random rnd = new Random();
            for(int i =0; i<processesAmount;i++)
            {
                listOfProcesses.Add(new Process(rnd.Next(cpuBurstMinDuration, cpuBurstMaxDuration)));
            }
            return listOfProcesses;
        }

        void SerializeToXMLFile(List<Process> list)
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<Process>));
            
            System.IO.FileStream file = System.IO.File.Create(Path);
            serializer.Serialize(file, list);
            file.Close();
        }

        public List<Process> LoadProcessesFromSerializedXML(string path)
        {
            var Path = Environment.CurrentDirectory + "processes.xml";
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Process>));
            System.IO.StreamReader file = new System.IO.StreamReader(Path);
            List<Process> list = (List<Process>)reader.Deserialize(file);
            file.Close();
            return list;
        }

    }
}
