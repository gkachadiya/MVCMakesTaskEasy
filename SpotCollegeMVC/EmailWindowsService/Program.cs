using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace EmailWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            var myService = new DailyEmailService();
            if (Environment.UserInteractive)
            {
                myService.Start();
            }
            else
            {
                var servicesToRun = new ServiceBase[] { myService };
                ServiceBase.Run(servicesToRun);
            }

        }
    }
}
