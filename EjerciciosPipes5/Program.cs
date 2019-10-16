using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;

namespace EjerciciosPipes5
{
    class Program
    {
        static void Main(string[] args)
        {
            
            for(int i = 0; i < 2; i++)
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                using (AnonymousPipeServerStream apcs = new AnonymousPipeServerStream(PipeDirection.Out, HandleInheritability.Inheritable))
                {
                    using (StreamWriter sw = new StreamWriter(apcs))
                    {
                        Process p = new Process();
                        psi.FileName = "..\\..\\..\\Hijo\\bin\\Debug\\Hijo.exe";
                        psi.UseShellExecute = false;
                        psi.Arguments = apcs.GetClientHandleAsString();
                        p.StartInfo = psi;

                        p.Start();
                        sw.WriteLine(i);
                        
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
