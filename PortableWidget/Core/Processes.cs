using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PortableWidget.Core
{
    class Processes
    {
        public string GetProcessNameById(int id)
        {
            string ProcessName = "";
            Process localProcess = Process.GetProcessById(id);
            ProcessName = localProcess.ProcessName;
            return ProcessName;
        }

        public long GetRamUsageById(int id)
        {
            long RamUsage = 0;
            Process localProcess = Process.GetProcessById(id);
            RamUsage = localProcess.WorkingSet64;
            return RamUsage;
        }
    }


}
