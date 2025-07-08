using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using CustomTestRunner.Models;
using CustomTestRunner.Attributes;


namespace CustomTestRunner.Execution
{
    public class TestExecutor
    {
        public List<TestResult> ExecuteTests(IEnumerable<Type> testClasses)
        {
            var results = new List<TestResult>();

            foreach (var type in testClasses)
            {
                object instance;
                try { instance = Activator.CreateInstance(type); }
                catch { continue; }

                var setupMethods = type.GetMethods().Where(m => m.GetCustomAttribute(typeof(SetupAttribute)) != null && m.GetParameters().Length == 0);
                var teardownMethods = type.GetMethods().Where(m => m.GetCustomAttribute(typeof(TeardownAttribute)) != null && m.GetParameters().Length == 0);

                foreach (var method in type.GetMethods().Where(m => m.GetCustomAttribute(typeof(MyTestAttribute)) != null && m.GetParameters().Length == 0))
                {
                    var result = new TestResult { TestName = $"{type.Name}.{method.Name}" };
                    var stopwatch = Stopwatch.StartNew();

                    try
                    {
                        foreach (var setup in setupMethods)
                            setup.Invoke(instance, null);

                        method.Invoke(instance, null);

                        foreach (var teardown in teardownMethods)
                            teardown.Invoke(instance, null);

                        result.Passed = true;
                    }
                    catch (Exception ex)
                    {
                        var inner = ex.InnerException ?? ex;
                        result.Passed = false;
                        result.ErrorMessage = inner.Message;
                        result.ExceptionType = inner.GetType().Name;
                    }

                    stopwatch.Stop();
                    result.DurationMilliseconds = stopwatch.ElapsedMilliseconds;

                    results.Add(result);
                }
            }

            return results;
        }
    }
}

