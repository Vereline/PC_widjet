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
        // this methods will crash on super computer becouse of (int)id, id is uint,
        // but for as it is ok.
        public long GetRamUsageById(int id)
        {
            long RamUsage = 0;
            Process localProcess = Process.GetProcessById(id);
            RamUsage = localProcess.WorkingSet64;
            return RamUsage;
        }
    }


}
