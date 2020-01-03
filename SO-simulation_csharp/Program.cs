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
            pu.SerializeManyListsOfProcessesAtOnce(10, @"C:\Users\artur\Desktop\so sim\");
            
            


        }
    }
}
