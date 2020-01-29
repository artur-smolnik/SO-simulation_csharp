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
            /*
             Najpierw zostaje utworzony obiekt klasy ProcessUtilities,
             nastepnie poszczegolne obiekty klas algorytmow,
             nastepnie niezbedne wywoalania metodow uruchamiajacych wykonywanie algotymow i zapis wynikow do plikow
             Ilosc tworzonych ciagow procesow i ilosc procesow w ciagach regulujemy w klasie ProcessUtilities
             */
            ProcessUtilities pu = new ProcessUtilities(@"C:\Users\artur\Desktop\so sim\");
            FCFS fCFS = new FCFS(pu);
            LCFS lCFS = new LCFS(pu);
            SJF sJF = new SJF(pu);
            RoundRobinFCFS roundRobinFCFS_0p5 = new RoundRobinFCFS(pu, 0.5);
            RoundRobinFCFS roundRobinFCFS_1p = new RoundRobinFCFS(pu, 1);
            RoundRobinFCFS roundRobinFCFS_1p5 = new RoundRobinFCFS(pu, 1.5);
            RoundRobinLCFS roundRobinLCFS_0p5 = new RoundRobinLCFS(pu, 0.5);
            RoundRobinLCFS roundRobinLCFS_1p = new RoundRobinLCFS(pu, 1);
            RoundRobinLCFS roundRobinLCFS_1p5 = new RoundRobinLCFS(pu, 1.5);
            fCFS.RunFCFS();
            fCFS.PrintFCFSResults();
            lCFS.RunLCFS();
            lCFS.PrintLCFSResults();
            sJF.RunSJF_NP();
            sJF.PrintSJF_NPResults();
            roundRobinFCFS_0p5.RunRoundRobinFCFS();
            roundRobinFCFS_0p5.PrintRoundRobinFCFSResults();
            roundRobinFCFS_1p.RunRoundRobinFCFS();
            roundRobinFCFS_1p.PrintRoundRobinFCFSResults();
            roundRobinFCFS_1p5.RunRoundRobinFCFS();
            roundRobinFCFS_1p5.PrintRoundRobinFCFSResults();
            roundRobinLCFS_0p5.RunRoundRobinLCFS();
            roundRobinLCFS_0p5.PrintRoundRobinLCFSResults();
            roundRobinLCFS_1p.RunRoundRobinLCFS();
            roundRobinLCFS_1p.PrintRoundRobinLCFSResults();
            roundRobinLCFS_1p5.RunRoundRobinLCFS();
            roundRobinLCFS_1p5.PrintRoundRobinLCFSResults();
        }
    }
}
