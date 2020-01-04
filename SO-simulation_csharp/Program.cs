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
            pu.SerializeManyListsOfProcessesAtOnce(100, 100, 1000, @"C:\Users\artur\Desktop\so sim\");


            List<List<Process>> lists = pu.LoadManyListOfProcessesFromSerializedXMLs(@"C:\Users\artur\Desktop\so sim");
            int i = 1;
            foreach (var v in lists)
            {
                Console.Write(i + " ciag: ");
                foreach (var x in v)
                {
                    Console.Write(x.CpuBurstTime + " ");
                }
                Console.WriteLine();

                i++;
            }


        }
    }
}
