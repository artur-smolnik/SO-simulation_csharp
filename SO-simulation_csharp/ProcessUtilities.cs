using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SO_simulation_csharp
{
    class ProcessUtilities
    {

    List<Process> CreateListOfProcesses(int processesAmount, int cpuBurstMinDuration, int cpuBurstMaxDuration)
    {
            List<Process> listOfProcesses = new List<Process>();
            Random rnd = new Random();
            for(int i =0; i<processesAmount;i++)
            {
                listOfProcesses.Add(new Process())
            }
            return listOfProcesses;
    }
    


    }
}
