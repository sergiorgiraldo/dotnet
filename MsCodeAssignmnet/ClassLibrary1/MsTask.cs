using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MsCodeAssignmnet
{
    public class MsTaskRunEngine
    {
        public MsTask TaskToRun { get; private set; }
        public Dictionary<int, MsTask> RunEngineLog { get; private set; }
        public Stack<MsTask> stackTasks { get; private set; }

        public MsTaskRunEngine(MsTask taskToRun)
        {
            TaskToRun = taskToRun;
            RunEngineLog = new Dictionary<int, MsTask>();
            stackTasks = new Stack<MsTask>();
        }

        public void Run()
        {
            var order = 0;
            BuildStack(TaskToRun);
            foreach(var t in stackTasks)
            {
                if (TaskNotExecuted(t)) { t.Execute(); }                
                RunEngineLog.Add(order++, t);
            }
        }

        private bool TaskNotExecuted(MsTask t)
        {
            var result = true;
            for (int i = 0; i < RunEngineLog.Count; i++)
            {
                if (RunEngineLog[i].Info == t.Info && RunEngineLog[i].MsTaskState)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public void BuildStack(MsTask t)
        {
            stackTasks.Push(t);
            foreach (var tChild in t.Dependencies)
            {
                BuildStack(tChild);
            }
        }
    }


    public class MsTask
    {
        private List<MsTask> dependencies = new List<MsTask>();

        public string Info { get; set; }

        public MsTask(List<MsTask> msTasks)
        {
            Dependencies = msTasks;
        }

        public MsTask()
        {
            MsTaskState = false;
        }

        public bool MsTaskState { get; private set; }

        public int MsTaskExecutions { get; private set; }

        public void Execute()
        {
            if (MsTaskState)
            {
                throw new InvalidOperationException("Task already ran");
            }
            else
            {
                MsTaskState = true;
            }
        }

        public List<MsTask> Dependencies {
            get
            {
                return dependencies;
            }
            private set
            {
                dependencies.AddRange(value);
            }
        }
    }
}
