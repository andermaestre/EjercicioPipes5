using System;
using System.IO;
using System.IO.Pipes;

namespace Hijo
{
    class Program
    {
        static void Main(string[] args)
        {
            int cont;
            using (AnonymousPipeClientStream pipe = new AnonymousPipeClientStream(PipeDirection.In, args[0]))
            {

                using (StreamReader sr = new StreamReader(pipe))
                {
                    cont = int.Parse(sr.ReadLine());
                }
            }

            if (cont == 0)     //Hijo 1
            {
                String aps;
                Console.WriteLine("Escribeme algo.");
                using (NamedPipeClientStream pipa = new NamedPipeClientStream(".", "pepepipe", PipeDirection.Out))
                {

                    pipa.Connect();

                    using (StreamWriter sw = new StreamWriter(pipa))
                    {
                        sw.AutoFlush = true;
                        do
                        {
                            
                            aps = Console.ReadLine();
                            sw.WriteLine(aps);
                        } while (!aps.ToUpper().ToString().Equals("FIN"));
                    }

                }
            }
            else              //Hijo 2
            {
                using (NamedPipeServerStream pipa = new NamedPipeServerStream("pepepipe", PipeDirection.In))
                {
                    pipa.WaitForConnection();

                    using (StreamReader sr = new StreamReader(pipa))
                    {

                        String a;
                        do
                        {
                            a = sr.ReadLine();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(a);
                            Console.ForegroundColor = ConsoleColor.White;
                        } while (!a.ToUpper().ToString().Equals("FIN"));
                    }
                }
                
            }
        }
    }
}


