using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace SO_simulation_csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //C:\Users\artur\Desktop\so sim
            ProcessUtilities pu = new ProcessUtilities();
            pu.SerializeManyListsOfProcessesAtOnce(2, 100, 10, 1000, @"C:\Users\artur\Desktop\so sim\");
            pu.LoadManyListOfProcessesFromSerializedXMLs(@"C:\Users\artur\Desktop\so sim\");
            FCFS fCFS = new FCFS(@"C:\Users\artur\Desktop\so sim\");
            fCFS.RunFCFS();
            fCFS.PrintFCFSResults();
            Console.WriteLine();
            Console.WriteLine("---------------------------");
            SJF_NP sJF_NP = new SJF_NP(@"C:\Users\artur\Desktop\so sim\");
            sJF_NP.RunSJF_NP();
            sJF_NP.SortLoadedProcesses();
            sJF_NP.PrintSJF_NPResults();
        }
    }
}
