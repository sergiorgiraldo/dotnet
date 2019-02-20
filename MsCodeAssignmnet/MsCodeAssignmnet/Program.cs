using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsCodeAssignmnet
{
    class Program
    {
        static void Main(string[] args)
        {
            //program for quick tests
            MsTask te = new MsTask();
            te.Info = "E";
            MsTask td = new MsTask();
            td.Info = "D";
            MsTask tc = new MsTask(new List<MsTask> { te });
            tc.Info = "C";
            MsTask tb = new MsTask(new List<MsTask> { td, te });
            tb.Info = "B";
            MsTask ta = new MsTask(new List<MsTask> { tb, tc });
            ta.Info = "A";

            var r = new MsTaskRunEngine(ta);
            r.BuildStack(r.TaskToRun);
            foreach (var t in r.stackTasks)
            {
                Console.WriteLine(t.Info);
            }
            Console.Read();
        }
    }
}
