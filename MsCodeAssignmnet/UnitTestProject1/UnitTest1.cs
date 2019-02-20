using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MsCodeAssignmnet
{
    [TestClass]
    public class TaskTests
    {
        [TestMethod]
        public void CreateObjectTypeTask()
        {
            MsTask t = new MsTask();
            Assert.IsInstanceOfType(t, typeof(MsTask));  
        }

        [TestMethod]
        public void TaskCanHaveNoDependencyWithEmptyConstrcutor()
        {
            MsTask t = new MsTask();
            Assert.IsTrue(t.Dependencies.Count == 0);
        }

        [TestMethod]
        public void TaskCanHaveNoDependencyWithParameterizedConstructor()
        {
            MsTask tChild = new MsTask();
            MsTask t = new MsTask(new List<MsTask>());
            Assert.IsTrue(t.Dependencies.Count == 0);
        }

        [TestMethod]
        public void TaskCanHaveDependencies()
        {
            MsTask tChild = new MsTask();
            MsTask t = new MsTask(new List<MsTask>{ tChild});
            Assert.IsTrue(t.Dependencies.Count > 0);
        }

        [TestMethod]
        public void TaskNotExecutedHasStateFalse_EmptyConstructor()
        {
            MsTask t = new MsTask();
            Assert.IsFalse(t.MsTaskState);
        }

        [TestMethod]
        public void TaskNotExecutedHasStateFalse_ParameterizedConstructor()
        {
            MsTask tChild = new MsTask();
            MsTask t = new MsTask(new List<MsTask> { tChild });
            Assert.IsFalse(t.MsTaskState);
        }

        [TestMethod]
        public void TaskNotExecutedHasStateFalse_WithDependencies()
        {
            MsTask tChild = new MsTask();
            MsTask t = new MsTask(new List<MsTask> { tChild });
            Assert.IsTrue(t.Dependencies.Count > 0);
            Assert.IsFalse(t.MsTaskState);
        }

        [TestMethod]
        public void TaskNotExecutedHasStateFalse_WithNoDependencies()
        {
            MsTask t = new MsTask();
            Assert.IsTrue(t.Dependencies.Count == 0);
            Assert.IsFalse(t.MsTaskState);
        }

        [TestMethod]
        public void TaskExecutedHasStateTrue_WithNoDependencies()
        {
            MsTask t = new MsTask();
            t.Execute();
            Assert.IsTrue(t.Dependencies.Count == 0);
            Assert.IsTrue(t.MsTaskState);
        }

        [TestMethod]
        public void TaskExecutedTwiceThrowsException()
        {
            MsTask t = new MsTask();
            t.Execute();
            Assert.ThrowsException<InvalidOperationException>(() => t.Execute());
        }

    }

    [TestClass]
    public class EngineTests
    {
        [TestMethod]
        public void CreateObjectTypeTaskRunEngine()
        {
            MsTaskRunEngine r = new MsTaskRunEngine(new MsTask());
            Assert.IsInstanceOfType(r, typeof(MsTaskRunEngine));
        }

        [TestMethod]
        public void TasksOfSameTypeShouldExecuteOnce()
        {
            MsTaskRunEngine r = new MsTaskRunEngine(new MsTask());
            Assert.IsInstanceOfType(r, typeof(MsTaskRunEngine));
        }

        [TestMethod]
        public void EngineRunTaskWithoutDependencies()
        {
            MsTask td = new MsTask();
            td.Info = "D";
            MsTaskRunEngine r = new MsTaskRunEngine(td);
            r.Run();
            Assert.IsTrue(td.MsTaskState);
        }

        [TestMethod]
        public void EngineRunTaskWithSingleDependency()
        {
            MsTask te = new MsTask();
            te.Info = "E";
            MsTask tc = new MsTask(new List<MsTask> { te });
            tc.Info = "C";
            MsTaskRunEngine r = new MsTaskRunEngine(tc);
            r.Run();
            Assert.IsTrue(tc.MsTaskState);
            Assert.IsTrue(te.MsTaskState);
            Assert.IsTrue(r.RunEngineLog[0].Info == "E");
            Assert.IsTrue(r.RunEngineLog[1].Info == "C");
        }

        [TestMethod]
        public void EngineRunTaskWithMultipleDependenciesOneLevelDeep()
        {
            MsTask te = new MsTask();
            te.Info = "E";
            MsTask td = new MsTask();
            td.Info = "D";
            MsTask tb = new MsTask(new List<MsTask> { td, te });
            tb.Info = "B";
            MsTaskRunEngine r = new MsTaskRunEngine(tb);
            r.Run();
            Assert.IsTrue(tb.MsTaskState);
            Assert.IsTrue(td.MsTaskState);
            Assert.IsTrue(te.MsTaskState);
            Assert.IsTrue(r.RunEngineLog[0].Info == "E");
            Assert.IsTrue(r.RunEngineLog[1].Info == "D");
            Assert.IsTrue(r.RunEngineLog[2].Info == "B");
        }

        [TestMethod]
        public void EngineRunTaskWithMultipleDependenciesMultipleLevelsDeep()
        {
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
            MsTaskRunEngine r = new MsTaskRunEngine(ta);
            r.Run();
            Assert.IsTrue(ta.MsTaskState);
            Assert.IsTrue(tb.MsTaskState);
            Assert.IsTrue(tc.MsTaskState);
            Assert.IsTrue(td.MsTaskState);
            Assert.IsTrue(te.MsTaskState);
            Assert.IsTrue(r.RunEngineLog[0].Info == "E");
            Assert.IsTrue(r.RunEngineLog[1].Info == "C");
            Assert.IsTrue(r.RunEngineLog[2].Info == "E");
            Assert.IsTrue(r.RunEngineLog[3].Info == "D");
            Assert.IsTrue(r.RunEngineLog[4].Info == "B");
            Assert.IsTrue(r.RunEngineLog[5].Info == "A");
        }
    }

}
