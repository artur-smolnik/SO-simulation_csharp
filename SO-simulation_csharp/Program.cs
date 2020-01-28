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
            ProcessUtilities pu = new ProcessUtilities(@"C:\Users\artur\Desktop\so sim\");
            //pu.SerializeManyListsOfProcessesAtOnce(2, 10, 10, 1000, @"C:\Users\artur\Desktop\so sim\");
            FCFS fCFS = new FCFS(pu);
           
            SJF_NP sJF_NP = new SJF_NP(pu);

            fCFS.RunFCFS();
            fCFS.PrintFCFSResults();

            //sJF_NP.RunSJF_NP();            
            //sJF_NP.PrintSJF_NPResults();
            LCFS lCFS = new LCFS(pu);
            lCFS.RunLCFS();
            lCFS.PrintLCFSResults();
        }
    }
}
