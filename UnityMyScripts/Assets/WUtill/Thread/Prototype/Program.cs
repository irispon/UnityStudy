using System;
using System.Diagnostics;
using System.Threading;
public class Program 
{
        static void Main(string[] args)
        {
            Thread t1 = new Thread((e) =>
            {
                SetIdealProcessor(3);


            });

            t1.Start();
        }

        static void SetIdealProcessor(int cpuNumber)
        {
            if (cpuNumber >= Environment.ProcessorCount)
            {
                cpuNumber = 0;
            }

            foreach (ProcessThread pthread in Process.GetCurrentProcess().Threads)
            {
                if (pthread.Id == Thread.CurrentThread.ManagedThreadId)
                {
                    pthread.IdealProcessor = cpuNumber;
                    // 또는 이렇게. (Environment.ProcessorCount == 4인 경우, 0x0F flag 사용)
                    // pthread.ProcessorAffinity = new IntPtr(0x0F & (1 << cpuNumber));
                    // dkadkfktn
                    break;
                }
            }
        }
    
}

