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
            pu.SerializeManyListsOfProcessesAtOnce(100, 50, 10, 1000000, @"C:\Users\artur\Desktop\so sim\");
            pu.LoadManyListOfProcessesFromSerializedXMLs(@"C:\Users\artur\Desktop\so sim\");
            FCFS fCFS = new FCFS(@"C:\Users\artur\Desktop\so sim\");
            fCFS.RunFCFS();
            fCFS.PrintFCFSResults();


        }
    }
}
