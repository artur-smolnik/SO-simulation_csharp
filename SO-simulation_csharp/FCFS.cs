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
        private List<Process> DoneProcessesList;
        
        public FCFS(List<Process> list)
        {
            ReadyProcessesList = list;
            DoneProcessesList = new List<Process>();
        }

        public void LoadProcessesFromSerializedXML(string path)
        {
            
        }
    }

}
