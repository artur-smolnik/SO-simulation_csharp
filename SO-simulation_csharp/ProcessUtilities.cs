using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO_simulation_csharp
{
    class ProcessUtilities
    {
    
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
            var Path = Environment.CurrentDirectory;
            System.IO.FileStream file = System.IO.File.Create(Path+"processes.xml");
            serializer.Serialize(file, list);
            file.Close();
        }


    }
}
