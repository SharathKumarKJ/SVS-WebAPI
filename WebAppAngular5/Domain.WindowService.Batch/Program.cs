using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppAngular5.WindowService;

namespace Domain.WindowService.Batch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Job Service has been started");
            JobExecution.Start().GetAwaiter();
            Console.WriteLine("Job Service has been stopped");
            Console.ReadLine();
        }
    }
}
